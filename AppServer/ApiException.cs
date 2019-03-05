using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServer
{
    class ApiException: Exception
    {
        public readonly int Code;

        public ApiException(int code, string message) : base(message)
        {
            this.Code = code;
        }

        public ApiException(int code, string message , Exception inner) : base(message, inner)
        {
            Code = code;
        }
    }
}
