using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogEntity;

namespace ClientApp.LogExplorer.Model
{
    public class LazyLog
    {
        private Dictionary<long, LogTraceWithLabels> traces;

        public LazyLog()
        {
            traces = new Dictionary<long, LogTraceWithLabels>();
        }

        public void PushData(LogTraceWithLabels[] data)
        {
            traces.Clear();
            foreach (var logTrace in data)
            {
                traces[logTrace._pos] = logTrace;
            }
        }

        public bool CoversWindow(long start, long end)
        {
            //check only start and end
            return this[start] != null && this[end] != null;
        }

        public IEnumerable<LogTraceWithLabels> Enumerate()
        {
            return traces.Select(t => t.Value);
        }

        public IEnumerable<LogTraceWithLabels> EnumerateInRage(long begin, long end)
        {
            for (long i = begin; i < end; i++)
            {
                yield return this[i];
            }
        }
        public LogTraceWithLabels this[long pos]
        {
            get
            {
                LogTraceWithLabels tr;
                if (traces.TryGetValue(pos, out tr))
                {
                    return tr;
                }

                return null;
            }
        }
    }
}
