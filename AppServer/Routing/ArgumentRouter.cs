using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AppServer.Routing
{
    class ArgumentRouter : Router
    {
        public readonly string ArgumentName;

        public ArgumentRouter(string argumentName)
        {
            ArgumentName = argumentName;
        }

        public override void Handle(HttpListenerRequest req, HttpListenerResponse resp, Queue<string> path, JObject args)
        {
            var argVal = path.Dequeue(); //argument was enqueued back by previous handler
            args[ArgumentName] = argVal;
            base.Handle(req, resp, path, args);
        }
    }
}
