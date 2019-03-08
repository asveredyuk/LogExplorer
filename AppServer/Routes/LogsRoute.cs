﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServer.Ext;
using AppServer.Routing;
using DatabaseBoundary;
using LogEntity;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppServer.Routes
{
    class LogsRoute : Router
    {
        public LogsRoute()
        {
            Get("/list", new DelegateRouter(GetLogsList));
            Get("/:logname/info", new DelegateRouter(GetLogInfo));
            Post("/:logname/at_pos", new DelegateRouter(GetItemsAtPos));
            Use("/:logname/labels", new LogLabelsRoute());
        }
        //TODO: add ability to delete the log
        ///get list of all available logs
        public void GetLogsList(HttpListenerRequest req, HttpListenerResponse resp)
        {
            //TODO: reuse client
            //var db = GetDb();
            //var colNames = db.ListCollections().ToEnumerable().Select(t => t["name"].AsString).ToList();
            resp.WriteJson(DatabaseClient.Self.GetLogNames());
        }
        //get info about collection
        public void GetLogInfo(HttpListenerRequest req, HttpListenerResponse resp, JObject args)
        {
            if (!args.ContainsKey("logname"))
            {
                throw new ApiException(400, "log name is not defined");
            }

            string name = args["logname"].Value<string>();
            var db = DatabaseClient.Self.GetLogDatabase(name);
            var info = db.Info;
            resp.WriteJson(info);
        }

        public class GetItemsAtPosArgs
        {
            [JsonRequired]
            public long Pos { get; set; }
            [JsonRequired]
            public long Count { get; set; }
        }
        public void GetItemsAtPos(HttpListenerRequest req, HttpListenerResponse resp, JObject jobj)
        {
            if (!jobj.ContainsKey("logname"))
            {
                throw new ApiException(400, "log name is not defined");
            }
            string name = jobj["logname"].Value<string>();
            var db = DatabaseClient.Self.GetLogDatabase(name);
            var args = req.ReadJson<GetItemsAtPosArgs>();
            var res = db.GetTracesAtPos(args.Pos, args.Count);
            resp.WriteJson(res);
        }
        
    }
}
