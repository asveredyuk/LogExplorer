using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JobSystem;
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp
{

    class ApiBoundary
    {
        private const string SERVER_PATH = "http://localhost:8080/";
        public static async Task<JobInfo[]> GetJobs()
        {
            WebRequest wq = WebRequest.CreateHttp(SERVER_PATH + "/jobs");
            var res = await wq.GetResponseAsync();
            using (var sr = new StreamReader(res.GetResponseStream()))
            {
                var json = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<JobInfo[]>(json);
            }
        }

        public static async Task<string[]> GetLogsNames()
        {
            (int code, string[] data) = await MakeRequest<string[]>("/logs/list");
            if (code != 200)
            {
                MessageBox.Show("Api Error");
            }

            return data;
        }

        public static async Task<LogInfo> GetLogInfo(string logName)
        {
            (int code, LogInfo info) = await MakeRequest<LogInfo>($"/logs/{logName}/info" );
            if (code != 200)
            {
                MessageBox.Show("Api error");
            }

            return info;
        }

        public static async Task<LogTraceWithLabels[]> GetLogAtPos(string logName, long pos, long count)
        {
            var obj = new
            {
                Pos = pos,
                Count = count
            };
            var json = JsonConvert.SerializeObject(obj);
            (int code, LogTraceWithLabels[] data) = await MakeRequest<LogTraceWithLabels[]>($"/logs/{logName}/at_pos", "POST", json);
            if (code != 200)
            {
                MessageBox.Show("Api error");
            }

            return data;

        }

        public static async Task DeleteJob(JobInfo job)
        {
            HttpWebRequest wq = HttpWebRequest.CreateHttp(SERVER_PATH + "/jobs/" + job.Id.ToString());
            wq.Method = "DELETE";
            var res = await wq.GetResponseAsync() as HttpWebResponse;
            if ((int)res.StatusCode != 200)
            {
                MessageBox.Show("error");
                return;
            }

        }

        public static async Task<string[]> GetFiles()
        {
            HttpWebRequest wq = WebRequest.CreateHttp(SERVER_PATH + "/files");
            var res = await wq.GetResponseAsync() as HttpWebResponse;
            using (var sr = new StreamReader(res.GetResponseStream()))
            {
                var json = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<string[]>(json);
            }
        }

        public static async Task<string[]> GetHeaders(string fname)
        {
            var obj = new
            {
                FileName = fname
            };
            string jsonReq = JsonConvert.SerializeObject(obj);

            HttpWebRequest wq = WebRequest.CreateHttp(SERVER_PATH + "/files/get_headers");
            wq.Method = "POST";
            var reqStr = await wq.GetRequestStreamAsync();
            using (var sw = new StreamWriter(reqStr))
            {
                await sw.WriteAsync(jsonReq);
                sw.Close();
            }

            var res = await wq.GetResponseAsync() as HttpWebResponse;
            using (var sr = new StreamReader(res.GetResponseStream()))
            {
                var jsonRes = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<string[]>(jsonRes);
            }


        }

        public static async Task AddImportTask(ImportArgs args)
        {
            var json = JsonConvert.SerializeObject(args);
            HttpWebRequest wq = WebRequest.CreateHttp(SERVER_PATH + "/jobs/new/import");
            wq.Method = "POST";
            var reqStr = await wq.GetRequestStreamAsync();
            using (var sw = new StreamWriter(reqStr))
            {
                await sw.WriteAsync(json);
                sw.Close();
            }
            var res = await wq.GetResponseAsync() as HttpWebResponse;
            if ((int) res.StatusCode != 200)
            {
                MessageBox.Show("error");
            }
        }
        private static async Task<(int code, T res)> MakeRequest<T>(string path, string method = "GET", string payload = null)
        {
            HttpWebRequest wq = WebRequest.CreateHttp(SERVER_PATH + path);
            wq.Method = method;
            if (payload != null)
            {
                var reqStr = await wq.GetRequestStreamAsync();
                using (var sw = new StreamWriter(reqStr))
                {
                    await sw.WriteAsync(payload);
                    sw.Close();
                }
            }
            var res = await wq.GetResponseAsync() as HttpWebResponse;
            if (typeof(T) == typeof(void))
            {
                return ((int)res.StatusCode, default(T));
            }
            //if code is not 200, no data
            if (res.StatusCode != HttpStatusCode.OK)
            {
                return ((int)res.StatusCode, default(T));
            }
            using (var sr = new StreamReader(res.GetResponseStream()))
            {
                var json = await sr.ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<T>(json);
                return (200, data);
            }
        }
    }


    public class ImportArgs
    {
        [JsonRequired]
        public string FileName { get; set; }
        [JsonRequired]
        public string GroupingField { get; set; }
        [JsonRequired]
        public string TimeField { get; set; }
        [JsonRequired]
        public string GroupingFieldType { get; set; }
        [JsonRequired]
        public string TimeFieldType { get; set; }
        //another data here
    }
}
