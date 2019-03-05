using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSystem
{
    public class Job
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public JobType Type { get; set; }
    }
}
