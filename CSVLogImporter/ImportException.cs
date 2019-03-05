using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVLogImporter
{
    class ImportException : Exception
    {
        public int Code { get; set; }
        public ImportException(string message, int code) : base(message)
        {
            Code = code;
        }
    }
}
