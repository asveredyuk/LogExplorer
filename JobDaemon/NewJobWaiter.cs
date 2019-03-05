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
    class NewJobWaiter
    {
        private Thread executingThread;
        private const int WAITER_INTERVAL = 1000;
        void Main()
        {
            while (true)
            {
                //check if there are some new jobs    
                foreach (var jobfile in Directory.GetFiles(Program.NEW_JOBS_PATH, "*" + Program.JOB_EXT))
                {
                    TryHandleNewJob(jobfile);
                }

                try
                {
                    Thread.Sleep(WAITER_INTERVAL);
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(nameof(NewJobWaiter) + " stopped");
                    return;
                }
            }
        }
        void TryHandleNewJob(string fname)
        {
            try
            {
                string fileText = File.ReadAllText(fname);
                Job job = JsonConvert.DeserializeObject<Job>(fileText);
                //if (Guid.Parse(Path.GetFileNameWithoutExtension(fname)) != job.Id)
                //{
                //    throw new Exception("File name does not match job id");
                //}
                string resFname = Program.PENDING_JOBS_PATH + job.Id.ToString() + Program.JOB_EXT;
                if (File.Exists(resFname))
                {
                    throw new Exception("Job already exists");
                }
                File.Move(fname, resFname);
                Logger.LogNewJobFound(job.Id.ToString(), job.Type.ToString());

            }
            catch (Exception e)
            {
                Logger.LogInvalidNewJob(e.Message, Path.GetFileName(fname));
                //TODO:redo
                try
                {
                    File.Move(fname, Program.INVALID_JOBS_PATH + fname);
                }
                catch (Exception e2)
                {
                    File.Delete(fname);
                }

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

        public static NewJobWaiter StartNew()
        {
            var waiter = new NewJobWaiter();
            waiter.Start();
            return waiter;
        }
    }
}
