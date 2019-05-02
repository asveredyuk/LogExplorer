using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogEntity
{
    public class LabeledLogTrace : LogTrace
    {
        public Dictionary<string, int[]> Filters;
    }
}
