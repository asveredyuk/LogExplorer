using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JobSystem;
using Newtonsoft.Json;

namespace JobDaemon
{
    class JobExecutor
    {
        private Thread executingThread;
        private const int EXECUTOR_INTERVAL = 1000;

        private void Main()
        {
            //check if active dir is not empty
            var pendingDir = new DirectoryInfo(Program.PENDING_JOBS_PATH);
            while (true)
            {
                //check if there are some pending jobs    
                pendingDir.Refresh();
                var jobFiles = pendingDir.GetFiles("*" + Program.JOB_EXT);
                if (jobFiles.Length > 0)
                {
                    //there is a pending jobs
                    var jobFile = jobFiles.OrderBy(t => t.CreationTime).First();
                    string jobFname = jobFile.FullName;
                    //TODO: handle errors
                    Job job = JsonConvert.DeserializeObject<Job>(File.ReadAllText(jobFname));

                    jobFname = MoveJobToActive(jobFname);
                    Logger.LogStartedJob(job.Id.ToString(), job.Type.ToString());
                    (int executeRsult, long elapsedMs) = ExecuteJob(jobFname, job);
                    TryUpdateTimeTaken(jobFname, job, elapsedMs);

                    switch (executeRsult)
                    {
                        case 0:
                            MoveJobToCompleted(jobFname);
                            Logger.LogCompletedJob(job.Id.ToString(), job.Type.ToString());
                            break;
                        case 1:
                            //todo: cancelled
                            break;
                        case 2:
                        case 3:
                        default:
                            MoveJobToFailed(jobFname);
                            Logger.LogFailedJob(job.Id.ToString(), job.Type.ToString());
                            break;
                    }
                }


                try
                {
                    Thread.Sleep(EXECUTOR_INTERVAL);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(nameof(NewJobWaiter) + " stopped");
                    return;
                }
            }
        }
        /// <summary>
        /// Starts appropriate executor and waits till it exits (or is cancelled?)
        /// </summary>
        /// <param name="jobFname"></param>
        /// <param name="job"></param>
        /// <returns>0 - completed successfully, 1 - cancelled, 2 - failed, result written, 3 - failed, unknown</returns>
        private (int, long) ExecuteJob(string jobFname, Job job)
        {
            const string IMPORTER_PATH =
                @"C:\Users\Alex\source\repos\CSVLogImporter\CSVLogImporter\bin\Debug\CSVLogImporter.exe";
            const string MAPMAKER_PATH =
                @"C:\dev\LogExplorer\ProcessMapMaker\bin\Debug\ProcessMapMaker.exe";
            Stopwatch sw = Stopwatch.StartNew();

            (int, long) ReturnInfo(int exitCode)
            {
                sw.Stop();
                if (exitCode == 0)
                {
                    return (0, sw.ElapsedMilliseconds);
                }
                else
                {
                    if (exitCode == 1)
                    {
                        return (2, sw.ElapsedMilliseconds);
                    }
                    else
                    {
                        return (3, sw.ElapsedMilliseconds);
                    }
                }
            }

            Process StartProc(string exe, string args)
            {
                Process process = new Process();

                // Stop the process from opening a new window
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                // Setup executable and parameters
                process.StartInfo.FileName = exe;
                process.StartInfo.Arguments = args;

                // Go
                process.Start();
                //TODO: problem may occur
                string output = process.StandardOutput.ReadToEnd();
                return process;
            }

            switch (job.Type)
            {
                case JobType.LogImport:
                    {
                        var proc = StartProc(IMPORTER_PATH, jobFname);//Process.Start(IMPORTER_PATH, jobFname);
                        proc.WaitForExit();
                        return ReturnInfo(proc.ExitCode);
                    }
                case JobType.MakeMap:
                    {
                        var proc = StartProc(MAPMAKER_PATH, jobFname);//Process.Start(MAPMAKER_PATH, jobFname);
                        proc.WaitForExit();
                        return ReturnInfo(proc.ExitCode);
                    }
                case JobType.CacheLabel:
                    {
                        var proc = StartProc(MAPMAKER_PATH, jobFname);//Process.Start(MAPMAKER_PATH, jobFname);
                        proc.WaitForExit();
                        return ReturnInfo(proc.ExitCode);
                    }

                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private static void TryUpdateTimeTaken(string jobFname, Job job, long ms)
        {
            string resultName = jobFname + ".result";
            try
            {
                if (File.Exists(resultName))
                {
                    var res = JobResult.ReadFromFile(resultName);
                    res.ElapsedMs = ms;
                    res.WriteToFile(resultName);
                }
                else
                {
                    var res = JobResult.MakeForJob(job);
                    res.ReturnCode = -1;//unknown
                    res.ElapsedMs = ms;
                    res.WriteToFile(resultName);
                }
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to update result file");
                return;
            }
        }
        public static long FindPrimeNumber(int n)
        {
            int count = 0;
            long a = 2;
            while (count < n)
            {
                long b = 2;
                int prime = 1;// to check if found a prime
                while (b * b <= a)
                {
                    if (a % b == 0)
                    {
                        prime = 0;
                        break;
                    }
                    b++;
                }
                if (prime > 0)
                {
                    count++;
                }
                a++;
            }
            return (--a);
        }
        private string MoveJobToActive(string jobFname)
        {
            try
            {
                var newName = Program.ACTIVE_JOBS_PATH + Path.GetFileName(jobFname);
                File.Move(jobFname, newName);
                return newName;
                //TODO: move the "result" to the same cat
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to move the job " + jobFname);
                //TODO:?
            }

            return null;
        }
        private void MoveJobToCompleted(string jobFname)
        {
            try
            {
                File.Move(jobFname, Program.COMPLETED_JOBS_PATH + Path.GetFileName(jobFname));
                var resultFname = jobFname + ".result";
                if (File.Exists(resultFname))
                {
                    File.Move(resultFname, Program.COMPLETED_JOBS_PATH + Path.GetFileName(resultFname));
                }
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to move the job " + jobFname);
                //TODO:?
            }
        }

        private void MoveJobToFailed(string jobFname)
        {
            try
            {
                File.Move(jobFname, Program.FAILED_JOBS_PATH + Path.GetFileName(jobFname));
                var resultFname = jobFname + ".result";
                if (File.Exists(resultFname))
                {
                    File.Move(resultFname, Program.FAILED_JOBS_PATH + Path.GetFileName(resultFname));
                }
            }
            catch (Exception e)
            {
                Logger.LogDebug("Failed to move the job " + jobFname);
                //TODO:?
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

        public static JobExecutor StartNew()
        {
            var executor = new JobExecutor();
            executor.Start();
            return executor;
        }
    }


}
