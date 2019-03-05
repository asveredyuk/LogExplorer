using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServer.Ext;
using AppServer.Routing;

namespace AppServer.Routes
{
    class HelloWorldRoute : Router
    {
        public HelloWorldRoute()
        {
            Get("/", new DelegateRouter(Getq));
        }

        private void Getq(HttpListenerRequest req, HttpListenerResponse resp)
        {
            
        }
    }
}
