using System;
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

namespace AppServer.Routes
{
    class LogsRoute : Router
    {
        public LogsRoute()
        {
            Get("/list", new DelegateRouter(GetLogsList));
            Get("/info", new DelegateRouter(GetLogInfo));
            Post("/at_pos", new DelegateRouter(GetItemsAtPos));
        }
        //TODO: add ability to delete the log
        ///get list of all available logs
        public void GetLogsList(HttpListenerRequest req, HttpListenerResponse resp)
        {
            //TODO: reuse client
            //var db = GetDb();
            //var colNames = db.ListCollections().ToEnumerable().Select(t => t["name"].AsString).ToList();
            resp.WriteJson(Database.Self.GetLogNames());
        }
        //get info about collection
        public void GetLogInfo(HttpListenerRequest req, HttpListenerResponse resp, string param)
        {

            //todo: may be log infos should be stored in collection to store info
            
            string name = param;
            var db = Database.Self.GetLogDatabase(name);
            var col = db.GetCollection<BsonDocument>(Database.LOG_DATA_COL_NAME);
            //TODO: handle if does not exists
            
            var info = new LogInfo()
            {
                Name = name,
                ItemsCount = col.Count(new BsonDocument())
            };

            resp.WriteJson(info);
        }

        public class GetItemsAtPosArgs
        {
            [JsonRequired]
            public long Pos { get; set; }
            [JsonRequired]
            public long Count { get; set; }
        }
        public void GetItemsAtPos(HttpListenerRequest req, HttpListenerResponse resp, string param)
        {
            
            string name = param;
            var db = Database.Self.GetLogDatabase(name);
            var col = db.GetCollection<LogTrace>(Database.LOG_DATA_COL_NAME);
            var args = req.ReadJson<GetItemsAtPosArgs>();
            var fb = Builders<LogTrace>.Filter;
            var filterGte = fb.Gte(t => t._pos, args.Pos);
            var filterLt = fb.Lt(t => t._pos, args.Pos + args.Count);
            var filter = fb.And(filterGte, filterLt);
            //for performance, change this to bson redirect
            var res = col.FindSync(filter).ToEnumerable();
            resp.WriteJson(res);
        }
        
    }
}
