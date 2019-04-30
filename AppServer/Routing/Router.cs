using System;
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

        private List<(string route, string method, IRouter router)> dirtyRouters = new List<(string path, string method, IRouter router)>();

        public void Print(string prev)
        {
            foreach (var pair in routers)
            {
                string name = $"{pair.Key.method} {pair.Key.route}";
                string str = $"|->{name}|";
                string holder = $"|  {new String(' ', name.Length)}";
                Console.WriteLine(prev + str);
                pair.Value.Print(prev + holder);
            }

            Console.WriteLine(prev + new string('-', Console.WindowWidth - prev.Length - 1));
        }

        protected void Compile()
        {
            foreach (var dr in dirtyRouters)
            {
                var drr = dr.router as Router;
                if (drr != null)
                {
                    drr.Compile();
                }
                AddRouter(dr.route, dr.method, dr.router);
            }
        }
        //TODO: make truncate after the whole tree is built
        void AddRouter(string route, string method, IRouter router)
        {
            if (router is Router)
            {
                var newRouter = router as Router;
                var pairsWithSamePath = routers.Where(t => t.Key.route == route).ToList();
                if (method == "*")
                {
                    //our router is universal
                    //we need to reassign all sub-routes of other routers
                    //and remove same routers
                    foreach (var pair in pairsWithSamePath)
                    {
                        var r = pair.Value as Router;
                        if (r == null)
                            throw new Exception("Conflict"); //dumb exception
                        foreach (var kv in r.routers)
                        {
                            newRouter.routers[kv.Key] = kv.Value;
                        }

                        routers.Remove(pair.Key);
                    }
                    //there is no any other router with same path
                    //we need to add this router, so no return
                }
                else
                {
                    //try to find universal router
                    foreach (var pair in pairsWithSamePath)
                    {
                        if (pair.Key.method == "*")
                        {
                            //there is universal router with same path
                            //we do not need to add current router
                            //just reassign subrouters
                            var r = pair.Value as Router;
                            if (r == null)
                                throw new Exception("Conflict"); //dumb exception
                            foreach (var kv in newRouter.routers)
                            {
                                r.routers[kv.Key] = kv.Value;
                            }
                            return;
                        }

                        if (pair.Key.method == method)
                        {
                            var r = pair.Value as Router;
                            if (r == null)
                                throw new Exception("Conflict"); //dumb exception
                            foreach (var kv in newRouter.routers)
                            {
                                r.routers[kv.Key] = kv.Value;
                            }
                            return;
                        }
                    }
                }
            }
            this.routers[(route, method)] = router;

        }
        public void Use(string path, IRouter router, string method = "*")
        {
            if (!path.Contains(":") && path.Count(ch => ch == '/') == 1 || path == "/:")
            {
                dirtyRouters.Add((path, method, router));
                //AddRouter(path,method,router);
                // this.routers[(path, method)] = router;
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
                        lastRouter.Use("/", router, method);
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
                    //try to find universal argumented method, if there are arg

                    if (subpath != "/" && routers.TryGetValue(("/:", "*"), out router))
                    {
                        //push back to front of queue
                        router.Handle(req, resp, new Queue<string>(new string[] { subpath.TrimStart('/') }.Concat(path)), args);
                    }
                    else
                    {
                        //try to find concrete argumented http method 
                        if (subpath != "/" && routers.TryGetValue(("/:", req.HttpMethod), out router))
                        {
                            //push back to front of queue
                            router.Handle(req, resp, new Queue<string>(new string[] { subpath.TrimStart('/') }.Concat(path)), args);
                        }
                        else
                        {
                            resp.CloseWithCode(404);
                        }
                    }
                }
            }
        }
        //handle with a dict and another interesting

        //public abstract void Handle(HttpListenerRequest req, HttpListenerResponse resp);
    }
}
