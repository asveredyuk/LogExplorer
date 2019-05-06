using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobSystem;
using JobSystem.Jobs;
using Newtonsoft.Json;

namespace JobDaemon
{
    class Program
    {
        public static string JOBS_PATH = @"X:\jobs\";
        public static string NEW_JOBS_PATH;
        public static string PENDING_JOBS_PATH;
        public static string ACTIVE_JOBS_PATH;
        public static string COMPLETED_JOBS_PATH;
        public static string FAILED_JOBS_PATH;
        public static string CANCELLED_JOBS_PATH;
        public static string INVALID_JOBS_PATH;
        public const string JOB_EXT = ".job";
        public const string PROGRESS_EXT = ".job.progress";
        private static NewJobWaiter newJobWaiter;
        private static JobExecutor jobExecutor;
        private static StatsDisplayer statsDisplayer;

        static void InitPath()
        {
            NEW_JOBS_PATH = JOBS_PATH + @"new\";
            PENDING_JOBS_PATH = JOBS_PATH + @"pending\";
            ACTIVE_JOBS_PATH = JOBS_PATH + @"active\";
            COMPLETED_JOBS_PATH = JOBS_PATH + @"completed\";
            FAILED_JOBS_PATH = JOBS_PATH + @"failed\";
            CANCELLED_JOBS_PATH = JOBS_PATH + @"cancelled\";
            INVALID_JOBS_PATH = JOBS_PATH + @"invalid\";
        }
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                //this first item is a path to config file
                ServerConfig.Config.FILE_PATH = args[0];
            }

            JOBS_PATH = ServerConfig.Config.Self.JobRepoPath;
            InitPath();
            //ImportJob j = new ImportJob()
            //{
            //    GroupingField = "SesstionId",
            //    InputFileName = "in.csv",
            //    TmpFolder = "tmp"
            //};
            //File.WriteAllText("X:\\test.job", JsonConvert.SerializeObject(j, Formatting.Indented));

            Logger.LogHello();
            //Logger.LogNewJobFound(Guid.NewGuid().ToString(),JobType.LogImport.ToString());
            //Logger.LogInvalidNewJob("Invalid json", Guid.NewGuid().ToString());
            CheckDirectoriesExists();
            //check if there is not executed active job
            if (Directory.GetFiles(ACTIVE_JOBS_PATH).Length > 0)
            {
                Logger.LogDebug("Non completed active job detected, removing everything from active folder");
                foreach (var fi in new DirectoryInfo(ACTIVE_JOBS_PATH).GetFileSystemInfos())
                {
                    fi.Delete();
                }
            }


            newJobWaiter = NewJobWaiter.StartNew();
            jobExecutor = JobExecutor.StartNew();
            statsDisplayer = StatsDisplayer.StartNew();


            Console.CancelKeyPress += delegate(object sender, ConsoleCancelEventArgs eventArgs)
            {
                eventArgs.Cancel = true;
                Console.WriteLine("Stopping, wait");
                newJobWaiter.Stop();
                jobExecutor.Stop();
                statsDisplayer.Stop();
            };
            //Console.ReadLine();
        }

        


        
        static void CheckDirectoriesExists()
        {
            var dirs = new string[] {JOBS_PATH, NEW_JOBS_PATH, PENDING_JOBS_PATH, ACTIVE_JOBS_PATH, COMPLETED_JOBS_PATH, FAILED_JOBS_PATH, CANCELLED_JOBS_PATH, INVALID_JOBS_PATH};
            foreach (var dir in dirs)
            {
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
        }

        
    }
}
