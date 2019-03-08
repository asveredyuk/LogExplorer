using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServer.Routes;
using AppServer.Routing;
using Newtonsoft.Json.Linq;

namespace AppServer
{
    class App : Router
    {
        private App()
        {

        }

        public static App MakeApp()
        {
            var app = new App();
            app.Use("/jobs", new JobRoute());
            app.Use("/files", new FilesRoute());
            app.Use("/logs", new LogsRoute());
         //   app.Use("/hello", new HelloWorldRoute());
            Console.WriteLine("Routes tree info:");
            app.Print("");
            return app;  
        }

        public void Handle(HttpListenerRequest req, HttpListenerResponse resp)
        {
            string absolutePath = req.Url.AbsolutePath;
            string[] arr = absolutePath.Split('/');
            var qu = new Queue<string>(arr.Where(t=>!string.IsNullOrEmpty(t)));
            Handle(req, resp, qu, new JObject());

        }

        


    }
}
