using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogEntity;

namespace ClientApp.LogExplorer.Model
{
    public class LogTraceWithLabels : LogTrace
    {
        public List<string[]> ItemsLabels = new List<string[]>();

    }
}
