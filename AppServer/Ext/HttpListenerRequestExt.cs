using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppServer.Ext
{
    public static class HttpListenerRequestExt
    {
        public static string ReadString(this HttpListenerRequest req)
        {
            if(req.InputStream == null)
                throw new NullReferenceException("Request input stream is null");
            if(!req.InputStream.CanRead)
                throw new IOException("Request input stream cannot read");
            using (var sr = new StreamReader(req.InputStream))
            {
                string str = sr.ReadToEnd();
                sr.Close();
                return str;
            }
        }
        public static T ReadJson<T>(this HttpListenerRequest req)
        {
            try
            {
                string json = req.ReadString();
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception e)
            {
                throw new ApiException(400, e.Message);
            }
        }
    }
}
