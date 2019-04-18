using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JobSystem;
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp
{
    public partial class ApiBoundary
    {
        public static async Task<ProcessMap> GetMap(string logname, string id)
        {
            WebRequest wq = WebRequest.CreateHttp(SERVER_PATH + "/maps/" + logname + "/" + id);
            var res = await wq.GetResponseAsync();
            using (var sr = new StreamReader(res.GetResponseStream()))
            {
                var json = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<ProcessMap>(json);
            }
        }

        public class MapListItem
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public static async Task<MapListItem[]> GetMapList(string logname)
        {
            WebRequest wq = WebRequest.CreateHttp(SERVER_PATH + "/maps/list/" + logname);
            var res = await wq.GetResponseAsync();
            using (var sr = new StreamReader(res.GetResponseStream()))
            {
                var json = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<MapListItem[]>(json);
            }
        }
    }
}
