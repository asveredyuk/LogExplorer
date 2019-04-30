using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AppServer.Ext;
using AppServer.Routing;
using DatabaseBoundary;
using Newtonsoft.Json.Linq;

namespace AppServer.Routes
{
    class MapRoute : Router
    {
        public MapRoute()
        {
            Get("/:logname/list", new DelegateRouter(GetMapsList));
            Get("/:logname/:id", new DelegateRouter(GetMap));
        }

        public void GetMap(HttpListenerRequest req, HttpListenerResponse resp, JObject args)
        {
            if (!args.ContainsKey("id") || !args.ContainsKey("logname"))
            {
                throw new ApiException(400, "Id is not specified");
            }

            var id = args["id"].Value<string>();
            var logname = args["logname"].Value<string>();
            var map = DatabaseClient.Self.GetLogDatabase(logname)?.GetMap(id);
            if (map == null)
            {
                throw new ApiException(404, "Not found");
            }
            resp.WriteJson(map);

        }

        class MapListItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
        public void GetMapsList(HttpListenerRequest req, HttpListenerResponse resp, JObject args)
        {
            if (!args.ContainsKey("logname"))
            {
                throw new ApiException(400, "Id is not specified");
            }
            var logname = args["logname"].Value<string>();
            var maps = DatabaseClient.Self.GetLogDatabase(logname)?.GetMaps();
            if (maps == null)
            {
                throw new ApiException(404, "Log not found");
            }

            var infos = maps.Select(t => new MapListItem()
            {
                Id = t._id,
                Name = t.Name
            }).ToArray();

            resp.WriteJson(infos);

        }
    }
}
