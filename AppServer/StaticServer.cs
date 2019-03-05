using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServer.Ext;

namespace AppServer
{
    class StaticServer
    {
        private const string PORT = "80";
        private const string LISTENER_PREFIX = "http://+:" + PORT + "/";//"http://localhost:80/";
        private const string STATIC_DIR = @"www/";
        private HttpListener listener;
        private Task handler;
        private App app;
        public StaticServer()
        {
            app = App.MakeApp();
            listener = new HttpListener();
            listener.Prefixes.Add(LISTENER_PREFIX);

        }

        public void Start()
        {
            listener.Start();
            if (handler == null)
            {
                handler = Handler();
            }
        }

        public void Stop()
        {
            listener.Stop();
        }
        private async Task Handler()
        {
            while (true)
            {
                //if server is not listening anymore - exit
                if (!listener.IsListening)
                {
                    handler = null;
                    break;
                }
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest req = context.Request;
                HttpListenerResponse resp = context.Response;
                //start task to handle this request
                Task.Run(() => GoHandleRequest(req, resp));

            }
        }

        private void GoHandleRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            

            try
            {
                var path = STATIC_DIR + req.Url.AbsolutePath;
                if (!File.Exists(path))
                {
                    resp.CloseWithCode(404);
                }
                resp.WriteString(File.ReadAllText(path));
            }
            catch (Exception e)
            {
                //do nothing :(

            }
        }
    }
}
