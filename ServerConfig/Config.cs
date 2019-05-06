using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseBoundary;
using Newtonsoft.Json;

namespace ServerConfig
{
    public class Config
    {
        public static string FILE_PATH = "config.json";
        private static Config self;

        public static Config Self
        {
            get
            {
                if (self == null)
                {
                    if (File.Exists(FILE_PATH))
                    {
                        self = JsonConvert.DeserializeObject<Config>(File.ReadAllText(FILE_PATH));
                    }
                    else
                    {
                        self = new Config();
                    }
                }

                return self;
            }
        }
        public DatabaseConnectionInfo MongoDBConnectionInfo { get; set; }

        public string JobRepoPath { get; set; } = @"X:\jobs\";
        public string ImporterTempFolder { get; set; } = @"X:\temp\";
        public string FilesDir { get; set; } = @"C:\Users\Alex\Downloads\BPI2016_Clicks_Logged_In.csv\"; //@"X:\files\";
        //public string MongoDbName { get; set; } = "test";
        public string MongoDbPrefix { get; set; } = "_log_";

        public string CsvLogImporterPath { get; set; } = @"C:\dev\LogExplorer\CSVLogImporter\bin\Debug\CSVLogImporter.exe";
        public string ProcessMapMakerPath { get; set; } = @"C:\dev\LogExplorer\ProcessMapMaker\bin\Debug\ProcessMapMaker.exe";
    }
}
