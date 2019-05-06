using System.IO;
using System.Linq;
using System.Net;
using AppServer.Ext;
using AppServer.Routing;
using Newtonsoft.Json;
using ServerConfig;

namespace AppServer.Routes
{
    class FilesRoute:Router
    {

        public FilesRoute()
        {
            Get("/", new DelegateRouter(GetList));
            Post("/get_headers", new DelegateRouter(GetHeaders));
        }


        public void GetList(HttpListenerRequest req, HttpListenerResponse resp)
        {
            //returns array of names
            var all = GetFiles();
            var names = all.OrderByDescending(t=>t.CreationTime).Select(t => t.Name);
            resp.WriteJson(names);
        }

        public class GetHeadersReq
        {
            [JsonRequired]
            public string FileName { get; set; }
        }
        public void GetHeaders(HttpListenerRequest req, HttpListenerResponse resp)
        {
            var info = req.ReadJson<GetHeadersReq>();
            var all = GetFiles();
            var file = all.FirstOrDefault(t => t.Name == info.FileName);
            if (file == null)
            {
                throw new ApiException(404, "Specified file not found");
            }

            using (var sr = new StreamReader(file.OpenRead()))
            {
                var header = sr.ReadLine();
                var commas = header.Count(t => t == ',');
                var semicol = header.Count(t => t == ';');
                var splitter = commas > semicol ? ',' : ';';
                resp.WriteJson(header.Split(splitter));
            }
        }

        public static FileInfo[] GetFiles()
        {
            var di = new DirectoryInfo(Config.Self.FilesDir);
            if (!di.Exists)
            {
                di.Create();
                return new FileInfo[0];
            }

            return di.GetFiles();
        }
    }
}
