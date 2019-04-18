using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DatabaseBoundary;
using JobSystem;
using JobSystem.Jobs;
using LogEntity;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using ProcessMapMaker.Properties;

namespace ProcessMapMaker
{
    class Program
    {
        /// <summary>
        /// Splitter for internal dictionary keys
        /// </summary>
        private const char DICT_SPLITTER = ' ';
        public static ProcessMapJob Job { get; private set; }
        private static LogDatabase Database { get; set; }

        static int Main(string[] args)
        {


            if (args.Length == 0)
            {
                Console.WriteLine("No job specified");
                return 2;
            }

            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Job file not found");
                return 2;
            }

            string jobPath = args[0];
            //load the job
            try
            {
                string json = File.ReadAllText(jobPath);
                Job = JsonConvert.DeserializeObject<ProcessMapJob>(json);

            }
            catch (JobParseException e)
            {
                Console.WriteLine("Failed to initialize the job");
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load the job");
                return 2;
            }

            //init database
            if (!DatabaseClient.Self.GetLogNames().Contains(Job.LogName))
            {
                //no such log
                string msg = $"No such log \"{Job.LogName}\"";
                Console.WriteLine(msg);
                JobResult.WriteForJob(jobPath, Job, 3, msg);
                return 3;
            }

            Database = DatabaseClient.Self.GetLogDatabase(Job.LogName);

            var labels = GetLabels(Job.Labels).ToList();
            if (labels.Count != Job.Labels.Length)
            {
                string msg = $"Not all labels were found!";
                Console.WriteLine(msg);
                JobResult.WriteForJob(jobPath, Job, 4, msg);
                return 4;
            }
            //check if need to bake the labels
            var db = Database.Db;
            foreach (var logLabel in labels)
            {
                if (!Database.LabelHasCache(logLabel._id))
                {
                    var sw = Stopwatch.StartNew();
                    Console.WriteLine($"Baking {logLabel._id} label");
                    BakeLabel(logLabel);
                    sw.Stop();
                    Console.WriteLine($"Done in {sw.ElapsedMilliseconds}ms");
                }
            }

            (var crtMapCode, var map) = CreateMap(labels);
            if (crtMapCode != 0)
                return crtMapCode;

            foreach (var kv in map.OrderByDescending(t => t.Value))
            {
                Console.WriteLine($"{kv.Key} - {kv.Value}");
            }

            Console.WriteLine("Saving map");
            var saveMapCode = SaveMap(map, labels);
            Console.WriteLine("OK");
            Console.ReadLine();
            return saveMapCode;
            //return 0;





        }
        /// <summary>
        /// Converts dictionary into process map and saves it to database
        /// </summary>
        /// <param name="map"></param>
        /// <param name="labels"></param>
        /// <returns></returns>
        private static int SaveMap(Dictionary<string, long> map, List<LogLabel> labels)
        {
            ProcessMapRelation MakeRelation(KeyValuePair<string, long> kv)
            {
                string[] kbs = kv.Key.Split(DICT_SPLITTER);
                return new ProcessMapRelation()
                {
                    count = kv.Value,
                    labelFrom = kbs[0],
                    labelTo = kbs[1]
                };
            }

            var relations = map.OrderByDescending(t => t.Value).Select(MakeRelation).ToArray();
            var processMap = new ProcessMap()
            {
                _id = Job.MapId.ToString(),
                Labels = labels.ToArray(),
                Name = Job.MapName,
                Relations = relations
            };
            Database.AddMap(processMap);
            return 0;
        }
        /// <summary>
        /// Crate process map with given labels
        /// </summary>
        /// <param name="labels"></param>
        /// <returns></returns>
        private static (int code, Dictionary<string, long> map) CreateMap(List<LogLabel> labels)
        {
            //get settings
            long step = Job.TracesInOneStep;
            int maxTasks = Job.MaxThreads;

            //stopwatch to track the performance
            var sw = Stopwatch.StartNew();

            //reference to the traces collection
            var data = Database.GetTracesCollection();
            long totalCount = data.CountDocuments(new BsonDocument());
            //number of tasks to cover all the collection with steps
            long totalTasks = totalCount / step + 1;

            //mongodb aggregation framework stages
            var stages = new List<BsonDocument>();
            //removes all the fields except id
            stages.Add(MakeProjectFilter());
            //add filter stages
            foreach (var logLabel in labels)
            {
                stages.AddRange(MakeFilterStages(logLabel._id));
            }
            //generates tasks for each step
            IEnumerable<Task<Dictionary<string, long>>> TaskGenerator()
            {
                for (int i = 0; i < totalTasks; i++)
                {

                    stages.Insert(0, MakePosFilter(i * step, (i + 1) * step));
                    var pipeline = PipelineDefinition<LogTrace, BsonDocument>.Create(stages);
                    stages.RemoveAt(0);
                    yield return TaskFunc(pipeline, data);
                }
            }
            //task pool
            List<Task<Dictionary<string, long>>> tasks = new List<Task<Dictionary<string, long>>>();
            //number of finished tasks
            long tasksDone = 0;
            //the aggregated map
            var map = new Dictionary<string, long>();
            //aggregates given dictionary into map
            void MergeIn(Dictionary<string, long> dict)
            {
                foreach (var kv in dict)
                {
                    long val;
                    if (!map.TryGetValue(kv.Key, out val))
                    {
                        val = 0;
                    }

                    map[kv.Key] = val + kv.Value;
                }
            }

            foreach (var task in TaskGenerator())
            {
                tasks.Add(task);
                if (tasks.Count >= maxTasks)
                {
                    //we had reached the limit of concurrent tasks
                    //wait until any of them finishes
                    int taskIndex = Task.WaitAny(tasks.ToArray());
                    MergeIn(tasks[taskIndex].Result);
                    tasks.RemoveAt(taskIndex);

                    tasksDone++;
                    //progressBar
                    Util.Progress(tasksDone, totalTasks);
                }
            }

            //handle the rest of the tasks
            Task.WaitAll(tasks.ToArray());
            foreach (var task in tasks)
            {
                MergeIn(task.Result);
            }

            Util.Progress(100, 100);
            Console.WriteLine();
            
            sw.Stop();
            Console.WriteLine($"Done in {sw.ElapsedMilliseconds}ms");


            return (0, map);

        }
        /// <summary>
        /// Function for tasks, makes map for given trace part
        /// </summary>
        /// <param name="pipeline"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        static async Task<Dictionary<string, long>> TaskFunc(PipelineDefinition<LogTrace, BsonDocument> pipeline, IMongoCollection<LogTrace> data)
        {
            var bsonRes = (await data.AggregateAsync(pipeline)).ToList();
            var traces = bsonRes.Select(t => ConvertTrace(t).OrderBy(q => q.index));

            Dictionary<string, long> submap = new Dictionary<string, long>();
            foreach (var trace in traces)
            {
                MakeRelations(trace, submap);
                //MergeIn(dict);
            }

            return submap;
        }
        /// <summary>
        /// Makes relations for given trace
        /// </summary>
        /// <param name="trace"></param>
        /// <param name="rels"></param>
        /// <returns></returns>
        static Dictionary<string, long> MakeRelations(IEnumerable<(int index, string label)> trace, Dictionary<string, long> rels = null)
        {
            var relations = rels;
            if (relations == null)
            {
                relations = new Dictionary<string, long>();
            }

            var tr = trace.ToList();
            for (int i = 0; i < tr.Count - 1; i++)
            {
                //TODO: this is not good, one log record may have more than one label
                //TODO: the n'th process map implementation
                var key = tr[i].label + DICT_SPLITTER + tr[i + 1].label;
                long count;
                if (!relations.TryGetValue(key, out count))
                {
                    count = 0;
                }
                relations[key] = count + 1;
            }
            return relations;
        }
        /// <summary>
        /// Converts bson document into trace
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        static IEnumerable<(int index, string label)> ConvertTrace(BsonDocument doc)
        {
            foreach (var elem in doc)
            {
                if (!elem.Value.IsBsonArray)
                {
                    //this is not array
                    continue;
                }

                var arr = elem.Value.AsBsonArray;
                foreach (var val in arr)
                {
                    var index = (int)val.AsDouble;
                    yield return (index, elem.Name);
                }
            }
        }
        /// <summary>
        /// Generates position filter for aggregation framework
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        static BsonDocument MakePosFilter(long from, long to)
        {
            var stage1 = new BsonDocument
            {
                {
                    "$match" , new BsonDocument
                    {
                        {
                            "_pos", new BsonDocument
                            {
                                {"$gte",from },
                                {"$lt",to }
                            }

                        }
                    }
                }
            };
            return stage1;
        }
        /// <summary>
        /// Makes filter for aggregation framework to remove all the fields except id 
        /// </summary>
        /// <returns></returns>
        static BsonDocument MakeProjectFilter()
        {
            var stage2 = new BsonDocument
            {
                {
                    "$project", new BsonDocument
                    {
                        {"_id",true }
                    }
                }
            };
            return stage2;
        }
        /// <summary>
        /// Makes stages to imlement label filter for aggregation framework
        /// </summary>
        /// <param name="labelid"></param>
        /// <returns></returns>
        static IEnumerable<BsonDocument> MakeFilterStages(string labelid)
        {
            var stage1 = new BsonDocument
            {
                {
                    "$lookup", new BsonDocument
                    {
                        { "from",Database.MakeCacheNameForLabel(labelid)},
                        {"localField","_id" },
                        {"foreignField", "_id"},
                        {"as",labelid }
                    }
                }
            };
            var stage2 = new BsonDocument
            {
                {
                    "$addFields", new BsonDocument
                    {
                        {
                            labelid, new BsonDocument
                            {
                                {
                                    "$arrayElemAt",new BsonArray
                                    {
                                        "$" + labelid + ".value",
                                        0
                                    }
                                }
                            }
                        }
                    }
                }
            };
            yield return stage1;
            yield return stage2;
        }

        
        

        static IEnumerable<LogLabel> GetLabels(string[] guids)
        {
            return Database.GetLabels().Where(t => guids.Contains(t._id));
        }

        static void BakeLabel(LogLabel label)
        {
            string colName = Database.MakeCacheNameForLabel(label._id);
            var dataCol = Database.GetTracesCollection();
            var map = Resources.map;
            var reduce = Resources.reduce;
            var finalize = Resources.finalize;
            map = map.Replace("$FILTER$", label.JSFilter);
            //TODO: check here if js is valid
            var opts = new MapReduceOptions<LogTrace, BsonDocument>()
            {
                Finalize = finalize,
                OutputOptions = MapReduceOutputOptions.Replace(colName)
            };
            dataCol.MapReduce(map, reduce, opts);
        }

    }
}
