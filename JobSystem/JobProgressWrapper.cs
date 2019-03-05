using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JobSystem
{
    public class JobProgressWrapper : IDisposable
    {
        public JobProgress Progress { get; private set; }
        public string Fname { get; private set; }

        /// <summary>
        /// Time last changes written
        /// </summary>
        public DateTime LastWritten = DateTime.Now.AddDays(-1);
        /// <summary>
        /// Minimum interval of rewrites
        /// </summary>
        public const int WRITE_INTERVAL_MS = 1000;

        public JobProgressWrapper(JobProgress progress, string fname)
        {
            Progress = progress;
            Fname = fname;
        }
        /// <summary>
        /// Called when progress object changed and need to save
        /// </summary>
        public void CommitProgress()
        {
            lock (this)
            {
                if ((DateTime.Now - LastWritten).TotalMilliseconds > 1000)
                {
                    try
                    {
                        File.WriteAllText(Fname, JsonConvert.SerializeObject(Progress, Formatting.Indented));
                        LastWritten = DateTime.Now;
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine("DEBUG: " + "failed to write progress file");
                    }
                }
            }
        }

        public void Dispose()
        {
            //There is no need to store progress info more
            if(File.Exists(Fname))
                File.Delete(Fname);
        }
    }
}
