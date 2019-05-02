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
        private Dictionary<long, LabelledLogTraceExt> traces;

        public LazyLog()
        {
            traces = new Dictionary<long, LabelledLogTraceExt>();
        }

        public void PushData(LabelledLogTraceExt[] data)
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

        public IEnumerable<LabelledLogTraceExt> Enumerate()
        {
            return traces.Select(t => t.Value);
        }

        public IEnumerable<LabelledLogTraceExt> EnumerateInRage(long begin, long end)
        {
            for (long i = begin; i < end; i++)
            {
                yield return this[i];
            }
        }
        public LabelledLogTraceExt this[long pos]
        {
            get
            {
                LabelledLogTraceExt tr;
                if (traces.TryGetValue(pos, out tr))
                {
                    return tr;
                }

                return null;
            }
        }
    }
}
