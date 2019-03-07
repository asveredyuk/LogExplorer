using System.Collections.Generic;
using System.Linq;
using System.Net;
using AppServer.Ext;
using Newtonsoft.Json.Linq;

namespace AppServer.Routing
{
    public class Router : IRouter
    {
        private Dictionary<RoutingPath, IRouter> routers = new Dictionary<RoutingPath, IRouter>();

        public void Use(string path, IRouter router, string method = "*")
        {
            if (!path.Contains(":") && path.Count(ch => ch == '/') == 1 || path == "/:")
            {
                this.routers[(path, method)] = router;
                return;
            }
            //check if path is multiPath
            var pathItems = new Queue<string>(path.Substring(1).Split('/'));
            var lastRouter = this;
            while (pathItems.Count > 0)
            {
                var p = pathItems.Dequeue();
                if (p.StartsWith(":") && p.Length > 1)
                {
                    var argRouter = new ArgumentRouter(p.TrimStart(':'));
                    lastRouter.Use("/:", argRouter, method);
                    lastRouter = argRouter;
                    if (pathItems.Count == 0)
                    {
                        //this was the last item
                        lastRouter.Use("/", router,method);
                    }
                    continue;
                }

                if (pathItems.Count == 0)
                {
                    //this was the last item
                    lastRouter.Use("/" + p, router, method);
                }
                else
                {
                    var simpleRouter = new Router();
                    lastRouter.Use("/" + p, simpleRouter, method);
                    lastRouter = simpleRouter;
                }

            }
            
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
        public virtual void Handle(HttpListenerRequest req, HttpListenerResponse resp, Queue<string> path, JObject args)
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
                router.Handle(req, resp, path, args);
            }
            else
            {
                //try find method-specific method
                if (routers.TryGetValue((subpath, req.HttpMethod), out router))
                {
                    router.Handle(req, resp, path, args);
                }
                else
                {
                    //404, not found
                    //try to find argumented method, if there are args, only method-specific allowed
                    if (subpath != "/" && routers.TryGetValue(("/:", req.HttpMethod), out router))
                    {
                        //push back to front of queue
                        router.Handle(req,resp, new Queue<string>(new string[] { subpath.TrimStart('/') }.Concat(path)), args);
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
