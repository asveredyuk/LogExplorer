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
    public static class HttpListenerResponseExt
    {
        public static void WriteString(this HttpListenerResponse resp, string str)
        {
            if(resp.OutputStream == null)
                throw new NullReferenceException("No output stream detected");
            if(!resp.OutputStream.CanWrite)
                throw new IOException("Output stream cannot write");
            using (var sw = new StreamWriter(resp.OutputStream))
            {
                sw.Write(str);
                sw.Close();
            }
        }

        public static void WriteJson(this HttpListenerResponse resp, object obj, Formatting formatting = Formatting.Indented)
        {
            string json = JsonConvert.SerializeObject(obj, formatting);
            resp.WriteString(json);
        }

        public static void CloseWithCode(this HttpListenerResponse resp, int code)
        {
            resp.StatusCode = 404;
            resp.Close();
        }
    }
}
