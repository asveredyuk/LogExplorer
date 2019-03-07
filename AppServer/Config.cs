using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppServer
{
    class Config
    {
        private const string FILE_PATH = "config.json";
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

        public string FilesDir { get; set; } = @"C:\Users\Alex\Downloads\BPI2016_Clicks_Logged_In.csv\"; //@"X:\files\";
        //public string MongoDbName { get; set; } = "test";
        public string MongoDbPrefix { get; set; } = "_log_";
    }
}
