using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JobSystem.Jobs
{
    public class ImportJob : Job
    {
        public class CsvInputInfo
        {
            [JsonRequired]
            public string CsvFileName { get; set; }
            //[JsonRequired]
            public char CsvDelimiter { get; set; } = ',';
        }

        public class SchemaItemInfo
        {
            /// <summary>
            /// Type of field, required
            /// </summary>
            [JsonRequired]
            public string Type { get; set; }
            /// <summary>
            /// Field that is same for all records in on trace
            /// </summary>
            public bool GroupingField { get; set; }
            /// <summary>
            /// Field that orders records within one trace
            /// </summary>
            public bool TimeField { get; set; }
            /// <summary>
            /// Ignore this field, do not import to database
            /// </summary>
            public bool Ignore { get; set; }
        }

        public class DatabaseInfo
        {
            [JsonRequired]
            public string Database { get; set; }   
            [JsonRequired]
            public string Table { get; set; }
        }

        /// <summary>
        /// Info about input file
        /// </summary>
        [JsonRequired]
        public CsvInputInfo Input { get; set; }
        /// <summary>
        /// Info about database where to import
        /// </summary>
        [JsonRequired]
        public DatabaseInfo Database { get; set; }

        /// <summary>
        /// Info about data schema
        /// </summary>
        [JsonRequired]
        public Dictionary<string, SchemaItemInfo> Schema { get; set; }

        //public string InputFileName;

        //public string GroupingField;

        //public string TmpFolder;
        //public string InputFileName { get; set; }
        //public string GroupingField { get; set; }
        //public string OrderingField { get; set; }
        public string TmpFolder { get; set; }

        /// <summary>
        /// Field that is same for all records in on trace
        /// </summary>
        [JsonIgnore]
        public (string name, SchemaItemInfo info) GroupingField
        {
            get { return (groupingField, Schema[groupingField]); }
        }
        /// <summary>
        /// Field that orders records within one trace
        /// </summary>
        [JsonIgnore]
        public (string name, SchemaItemInfo info) TimeField
        {
            get { return (timeField, Schema[timeField]); }
        }


        private string groupingField;
        private string timeField;
        //public string DbName { get; set; }
        //public string TableName { get; set; }
        public ImportJob()
        {
            Type = JobType.LogImport;
        }
        /// <summary>
        /// Should be run after deserialization to initialize the object for further work
        /// </summary>
        public void Init()
        {
            var schemaGrouping = Schema.Where(t => t.Value.GroupingField).ToArray();
            int groupingCount = schemaGrouping.Count();
            if (groupingCount == 0)
            {
                throw new JobParseException(nameof(Schema), "No field with " + nameof(SchemaItemInfo.GroupingField) + " detected");
            }
            else
            {
                if (groupingCount > 1)
                {
                    throw new JobParseException(nameof(Schema), "Two or more fields with " + nameof(SchemaItemInfo.GroupingField) + " detected");
                }
                //ok
                groupingField = schemaGrouping.First().Key;
            }

            var schemaTime = Schema.Where(t => t.Value.TimeField).ToArray();
            int timeCount = schemaTime.Count();

            if (timeCount == 0)
            {
                throw new JobParseException(nameof(Schema), "No field with " + nameof(SchemaItemInfo.TimeField) + " detected");
            }
            else
            {
                if (timeCount > 1)
                {
                    throw new JobParseException(nameof(Schema), "Two or more fields with " + nameof(SchemaItemInfo.TimeField) + " detected");
                }
                //ok
                timeField = schemaTime.First().Key;
            }
            //ok
        }

        
    }
}
