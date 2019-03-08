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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AppServer.Routes
{
    class LogLabelsRoute : Router
    {
        public LogLabelsRoute()
        {
            Get("/", new DelegateRouter(GetLabels));
            Post("/", new DelegateRouter(AddLabel));
            Patch("/:id", new DelegateRouter(UpdateLabel));
            Delete("/:id", new DelegateRouter(DeleteLabel));
        }

        private LogDatabase GetDb(string name)
        {
            return DatabaseClient.Self.GetLogDatabase(name);
        }
        public void GetLabels(HttpListenerRequest req, HttpListenerResponse resp, JObject args)
        {
            string logname = args["logname"].Value<string>();
            var db = GetDb(logname);
            var data = db.GetLabels();
            resp.WriteJson(data, Formatting.Indented);
        }

        public void AddLabel(HttpListenerRequest req, HttpListenerResponse resp, JObject args)
        {
            string logname = args["logname"].Value<string>();
            var label = req.ReadJson<LogLabel>();
            var db = GetDb(logname);
            db.AddLabel(label);
            resp.Close();
        }

        public void UpdateLabel(HttpListenerRequest req, HttpListenerResponse resp, JObject args)
        {
            string logname = args["logname"].Value<string>();
            string id = args["id"].Value<string>();
            Guid guid;
            if (!Guid.TryParse(id, out guid))
            {
                throw new ApiException(400, $"id \"{id}\" is invalid");
            }

            var db = GetDb(logname);
            var label = req.ReadJson<LogLabel>();
            db.UpdateLabel(label);
            resp.Close();
        }

        public void DeleteLabel(HttpListenerRequest req, HttpListenerResponse resp, JObject args)
        {
            string logname = args["logname"].Value<string>();
            string id = args["id"].Value<string>();
            Guid guid;
            if (!Guid.TryParse(id, out guid))
            {
                throw new ApiException(400, $"id \"{id}\" is invalid");
            }

            var db = GetDb(logname);
            db.DeleteLabel(guid.ToString());
            resp.Close();
        }
    }
}
