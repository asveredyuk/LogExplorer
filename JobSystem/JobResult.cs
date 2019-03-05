using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JobSystem
{
    public class JobResult
    {
        public Guid Id { get; set; }
        public int ReturnCode { get; set; }
        public string Message { get; set; }
        //This field is filled with JobDaemon! ms
        public long ElapsedMs { get; set; }

        public static void WriteForJob(string jobPath, Job job, int code, string message)
        {
            var res = new JobResult()
            {
                Id = job.Id,
                ReturnCode = code,
                Message = message
            };
            res.WriteToFile(jobPath + ".result");
        }

        public static JobResult MakeForJob(Job job)
        {
            return new JobResult()
            {
                Id = job.Id
            };
        }

        public static JobResult ReadFromFile(string path)
        {
            return JsonConvert.DeserializeObject<JobResult>(File.ReadAllText(path));
        }

        public void WriteToFile(string path)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, json);
        }

    }
}
