using System.Collections.Generic;
using System.Linq;
using System.Net;
using AppServer.Ext;

namespace AppServer.Routing
{
    public class Router : IRouter
    {
        private Dictionary<RoutingPath, IRouter> routers = new Dictionary<RoutingPath, IRouter>();

        public void Use(string path, IRouter router, string method = "*")
        {
            this.routers[(path, method)] = router;
        }

        public void Get(string path, IRouter router)
        {
            Use(path, router, "GET");
        }

        public void Post(string path, IRouter router)
        {
            Use(path, router, "POST");
        }

        public void Patch(string path, IRouter router)
        {
            Use(path, router, "PATCH");
        }

        public void Delete(string path, IRouter router)
        {
            Use(path, router, "DELETE");
        }
        public virtual void Handle(HttpListenerRequest req, HttpListenerResponse resp, Queue<string> path)
        {
            //string q = req.Url.AbsolutePath.Trim('/');

            var subpath = "/";
            if (path.Count != 0)
            {
                subpath = path.Dequeue();
                if (!subpath.StartsWith("/"))
                    subpath = "/" + subpath;
            }
            IRouter router;
            //try find universal method
            
            if (routers.TryGetValue((subpath, "*"), out router))
            {
                router.Handle(req, resp, path);
            }
            else
            {
                //try find method-specific method
                if (routers.TryGetValue((subpath, req.HttpMethod), out router))
                {
                    router.Handle(req, resp, path);
                }
                else
                {
                    //404, not found
                    //try to find argumented method, if there are args, only method-specific allowed
                    if (subpath != "/" && routers.TryGetValue(("/:", req.HttpMethod), out router))
                    {
                        //push back to front of queue
                        router.Handle(req,resp, new Queue<string>(new string[] { subpath.TrimStart('/') }.Concat(path)));
                    }
                    else
                    {
                        resp.CloseWithCode(404);
                    }
                }
            }
        }
        //handle with a dict and another interesting

        //public abstract void Handle(HttpListenerRequest req, HttpListenerResponse resp);
    }
}
