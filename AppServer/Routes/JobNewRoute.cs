﻿using System;
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
            [JsonRequired]
            public char CsvDelimiter { get; set; }
            //another data here
        }
        public void NewImportJob(HttpListenerRequest req, HttpListenerResponse resp)
        {
            //TODO: this is wrong!! internal data detected! client should not work with this
            ImportArgs args = req.ReadJson<ImportArgs>();
            var fnameWithoutExtension = Path.GetFileNameWithoutExtension(args.FileName);
            var dbName = new string(fnameWithoutExtension.Where(char.IsLetterOrDigit).ToArray());
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
                    Database = Config.Self.MongoDbPrefix + dbName,
                    Table = LogDatabase.DATA_COL_NAME
                },
                Id = Guid.NewGuid(),
                Input = new ImportJob.CsvInputInfo()
                {
                    CsvFileName = Config.Self.FilesDir + args.FileName,
                    CsvDelimiter = args.CsvDelimiter
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
