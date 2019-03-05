using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSystem
{
    /// <summary>
    /// Wrapper class to describe job state (to work with client)
    /// </summary>
    public class JobInfo
    {
        public Guid Id { get; set; }
        public String State { get; set; }
        /// <summary>
        /// Data of the job itself
        /// </summary>
        //public dynamic JobData { get; set; }
        public JobProgress Progress { get; set; }

        public JobResult Result { get; set; }

        //todo: add job result?
    }
}
