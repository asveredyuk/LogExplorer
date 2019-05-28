using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServer.Ext;
using AppServer.Routing;
using DatabaseBoundary;
using JobSystem.Jobs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServerConfig;

namespace AppServer.Routes
{
    class JobNewRoute : Router
    {
        public string JOBS_PATH = @"X:\jobs\";
        public string NEW_JOBS_PATH;

        void InitPath()
        {
            NEW_JOBS_PATH = JOBS_PATH + @"new\";

        }
        public JobNewRoute()
        {
            JOBS_PATH = ServerConfig.Config.Self.JobRepoPath;
            InitPath();
            Post("/import", new DelegateRouter(NewImportJob));
            Post("/processmap", new DelegateRouter(NewProcessMapJob));
            Post("/cachelabel", new DelegateRouter(NewCacheJob));
        }

        public void NewCacheJob(HttpListenerRequest req, HttpListenerResponse resp)
        {
            var job = req.ReadJson<CacheLabelJob>();
            job.ConnectionInfo = Config.Self.MongoDBConnectionInfo;
            try
            {
                var json = JsonConvert.SerializeObject(job, Formatting.Indented);
                var path = NEW_JOBS_PATH + job.Id.ToString() + ".job";
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                throw new ApiException(500, "Failed to write job file", e);
            }
            resp.Close();
        }

        public void NewProcessMapJob(HttpListenerRequest req, HttpListenerResponse resp)
        {
            var job = req.ReadJson<ProcessMapJob>();
            job.ConnectionInfo = Config.Self.MongoDBConnectionInfo;
            try
            {
                var json = JsonConvert.SerializeObject(job, Formatting.Indented);
                var path = NEW_JOBS_PATH + job.Id.ToString() + ".job";
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                throw new ApiException(500, "Failed to write job file", e);
            }
            resp.Close();
        }
        public class ImportArgs
        {
            [JsonRequired]
            public Guid JobID { get; set; }
            [JsonRequired]
            public string FileName { get; set; }
            [JsonRequired]
            public string LogName { get; set; }
            [JsonRequired]
            public string GroupingField { get; set; }
            [JsonRequired]
            public string TimeField { get; set; }
            [JsonRequired]
            public string GroupingFieldType { get; set; }
            [JsonRequired]
            public string TimeFieldType { get; set; }
            [JsonRequired]
            public char CsvDelimiter { get; set; }
            //another data here
        }
        public void NewImportJob(HttpListenerRequest req, HttpListenerResponse resp)
        {
            //TODO: this is wrong!! internal data detected! client should not work with this
            ImportArgs args = req.ReadJson<ImportArgs>();
            var fnameWithoutExtension = Path.GetFileNameWithoutExtension(args.FileName);
            var dbName = args.LogName;
            var schema = new Dictionary<string, ImportJob.SchemaItemInfo>();
            schema[args.GroupingField] = new ImportJob.SchemaItemInfo()
            {
                GroupingField = true,
                Type = args.GroupingFieldType
            };
            schema[args.TimeField] = new ImportJob.SchemaItemInfo()
            {
                TimeField = true,
                Type = args.TimeFieldType
            };
            ImportJob ij = new ImportJob()
            {
                Database = new ImportJob.DatabaseInfo()
                {
                    ConnectionInfo = Config.Self.MongoDBConnectionInfo,
                    Database = Config.Self.MongoDbPrefix + dbName,
                    Table = LogDatabase.DATA_COL_NAME
                },
                Id = args.JobID,
                Input = new ImportJob.CsvInputInfo()
                {
                    CsvFileName = Config.Self.FilesDir + args.FileName,
                    CsvDelimiter = args.CsvDelimiter
                },
                Schema = schema,
                TmpFolder = Config.Self.ImporterTempFolder,
                MaxMemoryUseMegabytes = Config.Self.ImporterMaxRamMegabytes
            };
            //if (JobRoute.FindJob(ij.Id) != null)
            //{
            //    throw new ApiException(409, "Job with such id already exists");
            //}

            try
            {
                var json = JsonConvert.SerializeObject(ij, Formatting.Indented);
                var path = NEW_JOBS_PATH + ij.Id.ToString() + ".job";
                File.WriteAllText(path, json);
            }
            catch (Exception e)
            {
                throw new ApiException(500, "Failed to write job file", e);
            }
            resp.Close();
        }        
    }
}
