using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using JobSystem;

namespace JobDaemon
{
    static class Logger
    {
        private const ConsoleColor C_DEFAULT = ConsoleColor.White;
        private const ConsoleColor C_INVALID = ConsoleColor.DarkRed;
        private const ConsoleColor C_NEWJOB = ConsoleColor.DarkYellow;
        private const ConsoleColor C_STARTEDJOB = ConsoleColor.DarkGreen;
        private const ConsoleColor C_COMPLETEDJOB = ConsoleColor.DarkGreen;
        private const ConsoleColor C_FAILEDJOB = ConsoleColor.DarkRed;
        private const ConsoleColor C_CANCELEDJOB = ConsoleColor.DarkGreen;
        private const ConsoleColor C_STATS = ConsoleColor.DarkCyan;
        private const ConsoleColor C_DEBUG = ConsoleColor.Gray;
        static void MakeColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        static void ResetColor()
        {
            Console.ForegroundColor = C_DEFAULT;
        }
        public static void LogHello()
        {
            ResetColor();
            Console.WriteLine("JobDaemon v" + Assembly.GetAssembly(typeof(Logger)).GetName().Version);
            Console.WriteLine("Started at " + DateTime.Now.ToString());
        }

        public static void LogInvalidNewJob(string message, string fname)
        {
            Log("Job file \"" + fname + "\" is invalid, " + message,C_INVALID);
        }

        public static void LogNewJobFound(string guid, string type)
        {
            Log($"Got new job {guid}, {type}",C_NEWJOB);
        }

        public static void LogStartedJob(string guid, string type)
        {
            Log($"Started job {guid}, {type}",C_STARTEDJOB);
        }

        public static void LogCompletedJob(string guid, string type)
        {
            //TODO: add some result info?
            Log($"Completed job {guid}, {type}",C_COMPLETEDJOB);
        }

        public static void LogFailedJob(string guid, string type)
        {
            //TODO: add some info about failure
            Log($"Failed job {guid}, {type}",C_FAILEDJOB);
        }

        public static void LogCancelledJob(string guid, string type)
        {
            Log($"Cancelled job {guid}, {type}",C_CANCELEDJOB);
        }

        public static void LogStatsActiveJob(Job job)
        {
            Log($"Active job {job.Id}, {job.Type.ToString()}", C_STATS);
        }

        public static void LogStatsActiveProgress(JobProgress progress)
        {
            Log($"Current stage : {progress.CurrentStage}", C_STATS);
            Log($"Current stage progress: {progress.CurrentStagePercentage}%", C_STATS);
            Log($"Overall progress : {progress.OverallProgress}", C_STATS);
        }

        public static void LogStatsPendingJobs(int num)
        {
            Log($"{num} jobs pending", C_STATS);
        }
        public static void LogDebug(string msg)
        {
            Log("DEBUG : " + msg, C_DEBUG);
        }
        private static object Lock = new object();
        static void Log(string text, ConsoleColor color = C_DEFAULT)
        {
            lock (Lock)
            {
                MakeColor(color);
                Console.WriteLine($"{DateTime.Now.ToString()} : {text}");
                ResetColor();
            }
        }
    }
}
