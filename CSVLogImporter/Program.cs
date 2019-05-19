using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JobSystem;
using JobSystem.Jobs;
using LogEntity;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace CSVLogImporter
{
    class Program
    {
        public static JobProgressWrapper Progress { get; private set; }
        public static ImportJob Job { get; private set; }
        static int Main(string[] args)
        {
            //0 - success
            //1 - see in result file
            //2 - failed to read job

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
                Job = JsonConvert.DeserializeObject<ImportJob>(json);
                
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
            //initialize the job
            try
            {
                Job.Init();
                if (!Job.TmpFolder.EndsWith("\\") && !Job.TmpFolder.EndsWith("/"))
                    Job.TmpFolder += "\\";
            }
            catch (Exception e)
            {
                Console.WriteLine("Problem detected with job, " + e);
                JobResult.WriteForJob(jobPath, Job, 1, e.Message);
                return 1;
            }

            if (!File.Exists(Job.Input.CsvFileName))
            {
                Console.WriteLine("Input file not found");
                JobResult.WriteForJob(jobPath, Job, 1, "Input file not found");
                return 1;
            }

            if (Directory.Exists(Job.TmpFolder))
            {
                //delete all subs
                foreach (var fs in new DirectoryInfo(Job.TmpFolder).GetFileSystemInfos())
                {
                    fs.Delete();
                }
            }
            else
            {
                Directory.CreateDirectory(Job.TmpFolder);

            }
            var selfProgr = new JobProgress()
            {
                CurrentStage = "init",
                CurrentStagePercentage = 0,
                OverallProgress = 0,
                Id = Job.Id
            };
            using (Progress = new JobProgressWrapper(selfProgr, args[0] + ".progress"))
            {
                Progress.Progress.TotalStagesCount = 3;
                Progress.CommitProgress();

                //Console.ReadLine();
                //todo: try to truncate all the data that is the same for all records in the line
                try
                {
                    Progress.Progress.CurrentStageNomber = 1;
                    CsvImporter.Import(Job);
                    Progress.Progress.CurrentStageNomber = 2;
                    MetaCompiler.Compile(Job.TmpFolder);
                    Progress.Progress.CurrentStageNomber = 3;
                    MongoExporter.ExportToMongoDB(Job);
                }
                catch (ImportException e)
                {
                    JobResult.WriteForJob(jobPath, Job, e.Code, e.Message);
                    return 1;
                }
                catch (Exception e)
                {
                    JobResult.WriteForJob(jobPath, Job, 777, $"Crashed with exception {e}");
                    return 1;
                }

                //Console.WriteLine("Completed import");
                //Console.ReadLine();
                //TODO:clean up tmp folder?

            }
            JobResult.WriteForJob(jobPath, Job, 0, "OK");

            //Stopwatch swTotal = Stopwatch.StartNew();

            //string log_path =
            //    "C:\\Users\\Alex\\Downloads\\BPI2016_Clicks_Logged_In.csv\\BPI2016_Clicks_Logged_In_old.csv";
            ////"C:\\Users\\Alex\\Downloads\\BPI2016_Clicks_Logged_In.csv\\0.csv";
            //string tmp_dir = "X:\\tmp\\";


            ////if (dict.Count != 0)
            ////{
            ////    Console.WriteLine("Importing remaining objects");
            ////    ImportToDb(dict);
            ////    dict.Clear();
            ////    GC.Collect();
            ////}
            ////test();
            ////Console.WriteLine();
            ////Console.WriteLine("Objects : " + counter);
            ////Console.WriteLine("Grouped : " + grouped_count);
            //swTotal.Stop();

            //Console.WriteLine("Total : " + swTotal.ElapsedMilliseconds + " ms");
            // Console.ReadLine();
            return 0;
        }

        static void test()
        {
            var client = new MongoClient();
            var db = client.GetDatabase("test");
            var collection = db.GetCollection<Dictionary<string, string>>("traces");
            Dictionary<string, string> dt = new Dictionary<string, string>();
            dt["a"] = "b";
            dt["test"] = "hello";
            collection.InsertOne(dt);
        }

        static void ImportToDb(Dictionary<string, List<dynamic>> dict)
        {
            Console.WriteLine("ImportingToDb");
            Stopwatch sw = Stopwatch.StartNew();
            var client = new MongoClient();
            var db = client.GetDatabase("test");
            var collection = db.GetCollection<LogTrace>("traces");

            Console.WriteLine("Inserting");
            long done = 0;

            List<UpdateOneModel<LogTrace>> models = new List<UpdateOneModel<LogTrace>>();

            void Commit()
            {
                collection.BulkWrite(models);
                models.Clear();
            }

            const int COMMIT_EACH = 100;
            int count = 0;
            //collection.UpdateMany(dict.Select(t=> new LogTrace(){_id = t.Key, Items = t.Value}));
            foreach (var kv in dict)
            {
                FilterDefinition<LogTrace> find = Builders<LogTrace>.Filter.Eq("_id", kv.Key);
                UpdateDefinition<LogTrace> upd = Builders<LogTrace>.Update.PushEach("items", kv.Value);
                models.Add(new UpdateOneModel<LogTrace>(find, upd) { IsUpsert = true });
                count++;
                if (count % COMMIT_EACH == 0)
                    Commit();
                //collection.UpdateOne(find, upd, new UpdateOptions() { IsUpsert = true });
                Util.ProgressEach(++done, dict.Count, 100);
            }
            if (models.Count != 0)
                Commit();
            //collection.InsertMany();
            sw.Stop();
            Console.WriteLine("Import done in " + sw.ElapsedMilliseconds + " ms");
        }
    }
}
