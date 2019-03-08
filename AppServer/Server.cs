using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServer.Ext;

namespace AppServer
{
    class Server
    {
        private const string PORT = "8080";
        private const string LISTENER_PREFIX = "http://+:" + PORT + "/";//"http://localhost:80/";

        private HttpListener listener;
        private Task handler;
        private App app;
        public Server()
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
                app.Handle(req, resp);
                DebugLogger.LogRequest(req,resp);
            }
            catch (ApiException e)
            {
                try
                {
                    //handler has thrown exception
                    var obj = new
                    {
                        error = new
                        {
                            code = e.Code,
                            message = e.Message,
                            innerException = e.InnerException
                        }
                    };
                    resp.StatusCode = e.Code;
                    resp.WriteJson(obj);
                    DebugLogger.LogRequest(req,resp);
                }
                catch (Exception )
                {
                    //do nothing
                }
            }
            catch (Exception e)
            {
                try
                {

                    if (resp.OutputStream.CanWrite)
                    {
                        //responce was not closed
                        //set 500 - internal server error
                        resp.StatusCode = (int)HttpStatusCode.InternalServerError;
                        resp.Close();
                        DebugLogger.LogRequest(req, resp);
                    }
                }
                catch (Exception)
                {
                    //do nothing
                    //throw;
                }
                Console.Out.WriteLine("Exception in handler thread, " + e.Message);
                Console.Out.WriteLine(e.StackTrace);
                //throw;
            }
        }
        
    }
}
