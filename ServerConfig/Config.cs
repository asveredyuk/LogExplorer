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

        /// <summary>
        /// Path to the directory which will store jobs
        /// </summary>
        public string JobRepoPath { get; set; } = @"X:\jobs\";
        /// <summary>
        /// Path to the directory where temporary files are created during import process
        /// </summary>
        public string ImporterTempFolder { get; set; } = @"X:\temp\";
        /// <summary>
        /// Path to the directory with files.
        /// Upload your files here and then import
        /// </summary>
        public string FilesDir { get; set; } = @"C:\Users\Alex\Downloads\BPI2016_Clicks_Logged_In.csv\"; //@"X:\files\";

        public string MongoDbPrefix { get; set; } = "_log_";

        /// <summary>
        /// Path to importer executable
        /// </summary>
        public string CsvLogImporterPath { get; set; } = @"C:\dev\LogExplorer\CSVLogImporter\bin\Debug\CSVLogImporter.exe";
        /// <summary>
        /// Path to map maker executable
        /// </summary>
        public string ProcessMapMakerPath { get; set; } = @"C:\dev\LogExplorer\ProcessMapMaker\bin\Debug\ProcessMapMaker.exe";
        /// <summary>
        /// Memory limit for csv importer. 
        /// Do not make it less than 1 gb (1024mb)
        /// </summary>
        public int ImporterMaxRamMegabytes { get; set; } = 1024;

        /// <summary>
        /// Maximum number of threads when making the process maps. 
        /// More threads (not more than cores), faster the process will be.
        /// But the memory usage will raise
        /// </summary>
        public int MapMakerMaxThread { get; set; } = 4;
        /// <summary>
        /// Number of traces in one query to database.
        /// Shorter number makes too much queries and the process is longer
        /// Larger number makes less queries, but requires more memory
        /// </summary>
        public int MapMakerTracesInOneStep { get; set; } = 5000;

    }
}
