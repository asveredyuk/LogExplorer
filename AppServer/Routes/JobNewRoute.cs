using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServer.Ext;
using AppServer.Routing;
using JobSystem.Jobs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppServer.Routes
{
    class JobNewRoute : Router
    {
        public JobNewRoute()
        {
            Post("/import", new DelegateRouter(NewImportJob));
        }

        public class ImportArgs
        {
            [JsonRequired]
            public string FileName { get; set; }
            [JsonRequired]
            public string GroupingField { get; set; }
            [JsonRequired]
            public string TimeField { get; set; }
            [JsonRequired]
            public string GroupingFieldType { get; set; }
            [JsonRequired]
            public string TimeFieldType { get; set; }
            //another data here
        }
        public void NewImportJob(HttpListenerRequest req, HttpListenerResponse resp)
        {
            //TODO: this is wrong!! internal data detected! client should not work with this
            ImportArgs args = req.ReadJson<ImportArgs>();
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
                    Database = "test",
                    Table = "traces"
                },
                Id = Guid.NewGuid(),
                Input = new ImportJob.CsvInputInfo()
                {
                    CsvFileName = Config.Self.FilesDir + args.FileName
                },
                Schema = schema,
                TmpFolder = "X:\\tmp"
            };
            //if (JobRoute.FindJob(ij.Id) != null)
            //{
            //    throw new ApiException(409, "Job with such id already exists");
            //}

            try
            {
                var json = JsonConvert.SerializeObject(ij, Formatting.Indented);
                var path = JobRoute.NEW_JOBS_PATH + ij.Id.ToString() + ".job";
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
