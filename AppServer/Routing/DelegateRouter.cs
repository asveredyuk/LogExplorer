﻿using System;
using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;

namespace AppServer.Routing
{
    public class DelegateRouter : IRouter
    {
        private readonly Action<HttpListenerRequest, HttpListenerResponse> handler;
        private readonly Action<HttpListenerRequest, HttpListenerResponse, JObject> parametrizedHandler;

        public DelegateRouter(Action<HttpListenerRequest, HttpListenerResponse> handler)
        {
            this.handler = handler;
        }

        public DelegateRouter(Action<HttpListenerRequest, HttpListenerResponse, JObject> parametrizedHandler)
        {
            this.parametrizedHandler = parametrizedHandler;
        }

        //public static implicit operator DelegateRouter(Action<HttpListenerRequest, HttpListenerResponse> handler)
        //{
        //    return new DelegateRouter(handler);
        //}



        public void Handle(HttpListenerRequest req, HttpListenerResponse resp, Queue<string> path, JObject args)
        {
            if(handler != null)
                handler(req, resp);
            else
            {
                string param = string.Join("/", path);
                if(param != "")
                    args["rest_of_url"] = param;    
                parametrizedHandler(req, resp, args);
            }
        }
    }
}
