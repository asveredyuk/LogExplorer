using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSystem
{
    public class JobParseException : Exception
    {
        private const string DOES_NOT_EXIST_OR_INVALID = "Field does not exist or invalid";
        public string FieldName { get; private set; }

        public JobParseException(string fieldName, string msg):base(msg + ", " + fieldName)
        {
            FieldName = fieldName;
        }

    }
}
