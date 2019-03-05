using System.Collections.Generic;
using System.Net;

namespace AppServer.Routing
{
    public interface IRouter
    {
        void Handle(HttpListenerRequest req, HttpListenerResponse resp, Queue<string> path);
    }
}
