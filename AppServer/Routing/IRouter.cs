using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace AppServer.Routing
{
    public interface IRouter
    {
        void Handle(HttpListenerRequest req, HttpListenerResponse resp, Queue<string> path, JObject args);
    }
}
