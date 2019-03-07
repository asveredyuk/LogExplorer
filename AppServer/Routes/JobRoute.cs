using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AppServer.Ext;
using AppServer.Routing;
using JobSystem;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppServer.Routes
{
    class JobRoute : Router
    {
        public const string JOBS_PATH = @"X:\jobs\";
        public const string NEW_JOBS_PATH = JOBS_PATH + @"new\";

        public JobRoute()
        {
            Get("/", new DelegateRouter(GetJobs));
            Delete("/:id", new DelegateRouter(RemoveJob));
            Post("/new", new JobNewRoute());
        }

        public void GetJobs(HttpListenerRequest req, HttpListenerResponse resp)
        {
            var files = GetAllJobs();
            var ordered = files.OrderByDescending(t => t.CreationTime);
            var jobs = new List<JobInfo>();
            foreach (var fileInfo in ordered)
            {
                var jobJson = File.ReadAllText(fileInfo.FullName);
                var job = JsonConvert.DeserializeObject<Job>(jobJson);
                //dynamic d = JsonConvert.DeserializeObject<ExpandoObject>(jobJson);
                var state = fileInfo.Directory.Name;
                //d.state = state;
                var jobInfo = new JobInfo()
                {
                    Id = job.Id,
                    State = state
                };
                if (state == "active")
                {
                    var progressFileName = fileInfo.FullName + ".progress";
                    if (File.Exists(progressFileName))
                    {
                        try
                        {
                            var json = File.ReadAllText(progressFileName);
                            JobProgress progress = JsonConvert.DeserializeObject<JobProgress>(json);
                            jobInfo.Progress = progress;

                        }
                        catch (Exception e)
                        {
                            //failed to read progress info, do not care
                        }
                    }
                }

                if (state == "completed" || state == "failed")
                {
                    var resultFileName = fileInfo.FullName + ".result";
                    if (File.Exists(resultFileName))
                    {
                        try
                        {
                            var json = File.ReadAllText(resultFileName);
                            JobResult result = JsonConvert.DeserializeObject<JobResult>(json);
                            jobInfo.Result = result;
                        }
                        catch (Exception e)
                        {
                            //failed to read result info
                        }
                    }
                }
                jobs.Add(jobInfo);
            }
            resp.WriteJson(jobs);
        }

        public void RemoveJob(HttpListenerRequest req, HttpListenerResponse resp, JObject args)
        {
            if (!args.ContainsKey("id"))
            {
                throw new ApiException(400, "Guid is not specified");
            }
            Guid guid;
            if (!Guid.TryParse(args["id"].Value<string>(), out guid))
            {
                throw new ApiException(400, "Guid is not specified or invalid");
            }

            var all = GetAllJobs();
            var job = all.FirstOrDefault(t => Path.GetFileNameWithoutExtension(t.FullName) == guid.ToString());
            if (job == null)
            {
                throw new ApiException(404, "No job with given guid found");

            }

            if (job.Directory.Name == "active")
            {
                throw new ApiException(406, "Active jobs cannot be deleted");
            }

            DeleteJob(job.FullName);
            resp.Close();

        }

        public static FileInfo[] GetAllJobs()
        {
            var di = new DirectoryInfo(JOBS_PATH);

            return di.GetFiles("*.job", SearchOption.AllDirectories);
            
        }

        public static FileInfo FindJob(Guid guid)
        {
            var all = GetAllJobs();
            return all.FirstOrDefault(t => t.FullName.Contains(guid.ToString()));
        }

        private void DeleteJob(string fname)
        {
            File.Delete(fname);
            if (File.Exists(fname + ".result"))
            {
                File.Delete(fname + ".result");
            }
        }

    }
}
