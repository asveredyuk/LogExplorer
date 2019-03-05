using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSystem
{
    public class JobProgress
    {
        public Guid Id { get; set; }
        public int CurrentStagePercentage { get; set; }
        public string CurrentStage { get; set; }
        public int OverallProgress { get; set; }
        //message?
    }
}