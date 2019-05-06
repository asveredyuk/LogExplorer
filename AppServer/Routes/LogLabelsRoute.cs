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
            var data = db.GetLabels().ToList();
            foreach (var logLabel in data)
            {
                logLabel.HasCache = db.LabelHasCache(logLabel._id);
            }
            resp.WriteJson(data, Formatting.Indented);
        }

        public void AddLabel(HttpListenerRequest req, HttpListenerResponse resp, JObject args)
        {
            string logname = args["logname"].Value<string>();
            var label = req.ReadJson<LogLabel>();
            var db = GetDb(logname);
            if (db.GetLabels().Any(t => t._id == label._id))
            {
                throw new ApiException(400, "Label with such id already exists");
            }
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
            if (id != label._id)
            {
                throw new ApiException(400, "Id in url and id in object are different");
            }

            if (!db.GetLabels().Any(t => t._id == id))
            {
                throw new ApiException(404, "No label with given id found");
            }
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
            if (!db.GetLabels().Any(t => t._id == id))
            {
                throw new ApiException(404, "No label with given id found");
            }
            db.DeleteLabel(guid.ToString());
            resp.Close();
        }
    }
}
