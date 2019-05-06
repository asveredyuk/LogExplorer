using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServer.Ext;
using AppServer.Routing;
using Newtonsoft.Json;

namespace AppServer.Routes
{
    public class PingRouter : Router
    {
        public PingRouter()
        {
            Use("/", new DelegateRouter(Ping));
        }

        public void Ping(HttpListenerRequest req, HttpListenerResponse resp)
        {
            var obj = new
            {
                Ping = "Pong"
            };
            resp.WriteJson(obj, Formatting.Indented);
        }
    }
}
