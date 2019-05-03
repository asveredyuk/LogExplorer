using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogEntity;

namespace ClientApp.LogExplorer.Model
{
    public static class LogLabelExt
    {
        public static bool IsStartOrEnd(this LogLabel label)
        {
            return label._id == LogLabel.START_ID || label._id == LogLabel.END_ID;
        }
    }
}
