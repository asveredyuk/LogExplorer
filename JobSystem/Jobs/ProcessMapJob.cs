using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseBoundary;

namespace JobSystem.Jobs
{
    public class ProcessMapJob : Job
    {
        public DatabaseConnectionInfo ConnectionInfo { get; set; }

        public string LogName { get; set; }


        public string MapName { get; set; }

        public Guid MapId { get; set; }

        /// <summary>
        /// GUIDs of labels
        /// </summary>
        public string[] Labels { get; set; }

        public int MaxThreads { get; set; } = 4;

        public int TracesInOneStep { get; set; } = 5000;
        
        public ProcessMapJob()
        {
            Type = JobType.MakeMap;
        }
    }
}
