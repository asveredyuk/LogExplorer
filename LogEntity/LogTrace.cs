using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogEntity
{
    public class LogTrace
    {
        /// <summary>
        /// Unique identificator of log trace (grouping field value)
        /// </summary>
        public string _id;
        /// <summary>
        /// Position in overall log
        /// </summary>
        public long _pos;
        /// <summary>
        /// Cached values
        /// </summary>
        public dynamic Cache;
        /// <summary>
        /// Log records of trace
        /// </summary>
        public List<Dictionary<string, dynamic>> Items;
    }
}
