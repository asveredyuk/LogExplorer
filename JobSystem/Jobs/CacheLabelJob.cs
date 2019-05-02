using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JobSystem.Jobs
{
    public class CacheLabelJob : Job
    {
        [JsonRequired]
        public string LogName { get; set; }

        [JsonRequired]
        public string LabelId { get; set; }
        public CacheLabelJob()
        {
            Type = JobType.CacheLabel;
        }
    }
}
