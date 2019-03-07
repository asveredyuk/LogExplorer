using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServer.Ext;
using AppServer.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppServer.Routes
{
    class HelloWorldRoute : Router
    {
        public HelloWorldRoute()
        {
            Get("/te", new DelegateRouter(Getq));
            Get("/:id", new DelegateRouter(Test));
        }

        private void Getq(HttpListenerRequest req, HttpListenerResponse resp)
        {
            resp.WriteString("helllo!");
        }

        public void Test(HttpListenerRequest req, HttpListenerResponse resp, JObject arg)
        {
            Console.WriteLine(JsonConvert.SerializeObject(arg, Formatting.Indented));
            resp.Close();
        }
    }
}
