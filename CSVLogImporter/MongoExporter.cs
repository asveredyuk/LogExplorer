using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSystem.Jobs;
using LogEntity;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace CSVLogImporter
{
    class MongoExporter
    {
        static string FilterField(string header)
        {
            char FilterChar(char ch)
            {
                if (char.IsLetterOrDigit(ch))
                    return ch;
                return '_';
            }
            var res = "";
            if (!char.IsLetter(header[0]))
            {
                //field cannot start with no-letter
                res += "_";
            }
            res += new string(header.Select(FilterChar).ToArray());
            return res;
        }
        public static int ExportToMongoDB(ImportJob job)
        {
            Program.Progress.Progress.CurrentStage = "Importing to MongoDB";
            Program.Progress.Progress.CurrentStagePercentage = 0;
            Program.Progress.CommitProgress();


            Console.WriteLine("Importing to mongo");

            if (!File.Exists(job.TmpFolder + "\\compiled.meta"))
            {
                Console.WriteLine("No compiled meta file");
                return 1;
            }
            
            var schema = ReadCsvSchema(job.TmpFolder);

            //TODO: make customizable
            var client = new MongoClient();
            var db = client.GetDatabase(job.Database.Database);
            var collection = db.GetCollection<LogTrace>(job.Database.Table);



            long totalTraces = long.Parse(File.ReadAllText(job.TmpFolder+ "\\" + "compiled.count"));
            long addedCount = 0;
            var sr = new StreamReader(job.TmpFolder + "\\compiled.meta");

            const int BUF_SIZE = 5000; //TODO: make it depends on memory, not just magic
            List<LogTrace> buf = new List<LogTrace>();
            Task insertTask = null;
            Stopwatch overallStopwatch = new Stopwatch();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                    continue;
                string[] split1 = line.Split(GlobalConsts.META_SPLITTER);
                string id = split1[0];
                string[] poses = split1[1].Split(GlobalConsts.META_COMPILED_MULTI);

                string[] lines = ReadLinesFromData(job.TmpFolder, poses).ToArray();

                List<Dictionary<string, string>> items = ConvertFromLines(lines, schema);
                
                var trace = MakeTrace(id, items, job, addedCount);
                buf.Add(trace);

                if (buf.Count > BUF_SIZE)
                {
                    if (insertTask != null && !insertTask.IsCompleted)
                    {
                        var localSw = Stopwatch.StartNew();
                        overallStopwatch.Start();
                        insertTask.Wait();
                        localSw.Stop();
                        overallStopwatch.Stop();
                        //Console.WriteLine("Waited " + localSw.ElapsedMilliseconds + " ms");
                        //TODO:solve this?
                        if (insertTask.IsFaulted)
                            throw insertTask.Exception;
                    }
                    insertTask = collection.InsertManyAsync(buf, new InsertManyOptions(){BypassDocumentValidation = true, IsOrdered = false});
                    buf = new List<LogTrace>();
                }
                addedCount++;
                Util.ProgressEachFraction(addedCount, totalTraces, 100);
                if (addedCount % 1000 == 0)
                {
                    Program.Progress.Progress.CurrentStagePercentage = (int)(addedCount * 100 / totalTraces);
                    Program.Progress.CommitProgress();
                }
                //import here

            }

            Console.WriteLine("Overall wait " + overallStopwatch.ElapsedMilliseconds + " ms");
            return 0;
        }

        static List<Dictionary<string,string>> ConvertFromLines(string[] lines, string[] schema)
        {
            List<Dictionary<string, string>> res = new List<Dictionary<string, string>>();
            foreach (var line in lines)
            {
                string[] vals = line.Split(GlobalConsts.DATA_SPLITTER);
                var dic = new Dictionary<string,string>();
                for (int i = 0; i < schema.Length; i++)
                {
                    dic[schema[i]] = vals[i];
                }
                res.Add(dic);
            }

            return res;
        }
        static string[] ReadCsvSchema(string dataDir)
        {
            //todo : remove this and make everything via job
            return JsonConvert.DeserializeObject<string[]>(File.ReadAllText(dataDir + "\\" + "schema"));
        }

        static Func<string, dynamic> GetTypeConverterForField(ImportJob job, string fieldName)
        {
            ImportJob.SchemaItemInfo outp;
            if (job.Schema.TryGetValue(fieldName, out outp))
            {
                switch (outp.Type.ToLower())
                {
                    case "int":
                        return t => int.Parse(t);
                    case "long":
                        return t => long.Parse(t);
                    case "double":
                        return t => double.Parse(t);
                    case "decimal":
                        return t => decimal.Parse(t);
                    case "datetime":
                        return t => DateTime.Parse(t);
                    case "string":
                        return t => t;
                    case "bool":
                        return t => t.Length > 1 ? bool.Parse(t) : t=="1";
                        default:
                            throw new Exception("Cannot find hanlder for type " + outp.Type);
                }
            }
            return t => t;

        }
        static IEnumerable<string> ReadLinesFromData(string dataDir, string[] poses)
        {
            foreach (var pose in poses)
            {
                string[] spl = pose.Split(GlobalConsts.META_COMPILED_PREFIX);
                string dataName = $"part{spl[0]}.data";
                long pos = long.Parse(spl[1]);
                using (StreamReader sr = new StreamReader(dataDir + "\\" + dataName))
                {
                    sr.BaseStream.Seek(pos, SeekOrigin.Begin);
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        if(string.IsNullOrWhiteSpace(line))
                            break;
                        yield return line;
                    }
                }
            }
        }

        static LogTrace MakeTrace(string id, List<Dictionary<string, string>> trace, ImportJob job, long tracePos)
        {
            //order traces by time field
            List<Dictionary<string,dynamic>> newTrace = new List<Dictionary<string, dynamic>>();
            foreach (var dict in trace)
            {
                var newDict = new Dictionary<string,dynamic>();
                foreach (var kv in dict)
                {
                    newDict[kv.Key] = GetTypeConverterForField(job, kv.Key)(kv.Value);
                }
                newTrace.Add(newDict);
            }
            //order here
            var orderedTrace = newTrace.OrderBy(t => t[job.TimeField.name]).ToList();

            return new LogTrace()
            {
                _id = id,
                _pos = tracePos,
                Items = orderedTrace,
                Cache = new
                {
                    begin = orderedTrace[0][job.TimeField.name],
                    end = orderedTrace[orderedTrace.Count-1][job.TimeField.name]
                }
            };
        }
    }
}
