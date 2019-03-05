using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobSystem;
using Newtonsoft.Json;

namespace JobDaemon
{
    class StatsDisplayer
    {
        private Thread executingThread;
        private const int WAITER_INTERVAL = 5000;

        private void Main()
        {
            while (true)
            {
                try
                {
                    Thread.Sleep(WAITER_INTERVAL);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(nameof(StatsDisplayer) + " stopped");
                    return;
                }

                var activeJobs = Directory.GetFiles(Program.ACTIVE_JOBS_PATH, "*" + Program.JOB_EXT);
                
                if (activeJobs.Length > 0)
                {
                    if (activeJobs.Length > 1)
                    {
                        //TODO: handling in case of failure
                        Logger.LogDebug("Something strange! More than one active job");
                    }

                    var job = JsonConvert.DeserializeObject<Job>(File.ReadAllText(activeJobs[0]));
                    Logger.LogStatsActiveJob(job);
                    var activeJobProgressPath = activeJobs[0] + ".progress";
                    if (File.Exists(activeJobProgressPath)) //TODO: redo, move ext to program class
                    {
                        try
                        {
                            JobProgress progress =
                                JsonConvert.DeserializeObject<JobProgress>(File.ReadAllText(activeJobProgressPath));
                            Logger.LogStatsActiveProgress(progress);
                        }
                        catch (Exception e)
                        {
                            //failed to read info about progress
                            Logger.LogDebug("Failed to read progress info");
                        }
                    }
                    //nothing to display
                    continue;
                }

                var pendingCount = Directory.GetFiles(Program.PENDING_JOBS_PATH, "*" + Program.JOB_EXT).Length;
                if (pendingCount > 0)
                {
                    Logger.LogStatsPendingJobs(pendingCount);
                }
                var pendingJobs = Directory.GetFiles(Program.PENDING_JOBS_PATH, "*" + Program.JOB_EXT);
            }
        }
        public void Stop()
        {
            executingThread.Interrupt();
        }

        public void Start()
        {
            executingThread = new Thread(Main);
            executingThread.Start();
        }

        public static StatsDisplayer StartNew()
        {
            var displayer = new StatsDisplayer();
            displayer.Start();
            return displayer;
        }
    }
}
