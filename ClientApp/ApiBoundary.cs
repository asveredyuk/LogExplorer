using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Model;
using JobSystem;
using JobSystem.Jobs;
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp
{

    static partial class ApiBoundary
    {
        public static string SERVER_PATH = "http://localhost:8080/";

        static ApiBoundary()
        {

        }

        public static async Task<string> Ping(string server)
        {
            WebRequest wq = WebRequest.CreateHttp(server+ "/ping");
            wq.Timeout = 2000;
            var res = await wq.GetResponseAsync();
            using (var sr = new StreamReader(res.GetResponseStream()))
            {
                var json = await sr.ReadToEndAsync();
                return json;
            }

        }
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

        public static async Task<JobInfo> GetJobInfo(Guid id)
        {
            (int code, JobInfo info) = await MakeRequest<JobInfo>($"/jobs/{id.ToString()}");
            if (code != 200)
            {
                //MessageBox.Show("Api error");
                //placeholde
                return new JobInfo()
                {
                    Id = id,
                    State = "unknown"
                };
            }

            return info;
            //WebRequest wq = WebRequest.CreateHttp(SERVER_PATH + "/jobs/" + id.ToString());
            //var res = await wq.GetResponseAsync();
            //using (var sr = new StreamReader(res.GetResponseStream()))
            //{
            //    var json = await sr.ReadToEndAsync();
            //    return JsonConvert.DeserializeObject<JobInfo>(json);
            //}
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
            (int code, LogInfo info) = await MakeRequest<LogInfo>($"/logs/{logName}/info");
            if (code != 200)
            {
                MessageBox.Show("Api error");
            }

            return info;
        }
        public static async Task<string[]> GetFieldNames(string logname)
        {
            (int code, string[] names) = await MakeRequest<string[]>($"/logs/{logname}/field_names");
            if (code != 200)
            {
                MessageBox.Show("Api error");
            }

            return names;

        }

        public static async Task<string[]> GetDistinctFieldValues(string logname, string fieldname)
        {
            var obj = new
            {
                FieldName = fieldname
            };
            var json = JsonConvert.SerializeObject(obj);
            (int code, string[] arr) =
                await MakeRequest<string[]>($"/logs/{logname}/get_distinct_field_values", "POST", json);
            if (code != 200)
            {
                if (code == 400)
                {
                    //was too much values
                    return null;
                }
                MessageBox.Show("Api error");
            }

            return arr;
        }

        public static async Task<LabelledLogTraceExt[]> GetLogAtPos(string logName, long pos, long count, string[] labelIds = null)
        {
            var obj = new
            {
                Pos = pos,
                Count = count,
                LabelIds = labelIds
            };
            var json = JsonConvert.SerializeObject(obj);
            (int code, LabelledLogTraceExt[] data) = await MakeRequest<LabelledLogTraceExt[]>($"/logs/{logName}/at_pos_with_labels", "POST", json);
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

        public static async Task AddProcessMapJob(ProcessMapJob job)
        {
            var json = JsonConvert.SerializeObject(job);
            HttpWebRequest wq = WebRequest.CreateHttp(SERVER_PATH + "/jobs/new/processmap");
            wq.Method = "POST";
            var reqStr = await wq.GetRequestStreamAsync();
            using (var sw = new StreamWriter(reqStr))
            {
                await sw.WriteAsync(json);
                sw.Close();
            }
            var res = await wq.GetResponseAsync() as HttpWebResponse;
            if ((int)res.StatusCode != 200)
            {
                MessageBox.Show("error");
            }
        }
        public static async Task AddCacheLabelJob(CacheLabelJob job)
        {
            var json = JsonConvert.SerializeObject(job);
            HttpWebRequest wq = WebRequest.CreateHttp(SERVER_PATH + "/jobs/new/cachelabel");
            wq.Method = "POST";
            var reqStr = await wq.GetRequestStreamAsync();
            using (var sw = new StreamWriter(reqStr))
            {
                await sw.WriteAsync(json);
                sw.Close();
            }
            var res = await wq.GetResponseAsync() as HttpWebResponse;
            if ((int)res.StatusCode != 200)
            {
                MessageBox.Show("error");
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
            if ((int)res.StatusCode != 200)
            {
                MessageBox.Show("error");
            }
        }

        private static async Task<int> MakeRequest(string path, string method = "GET",
            string payload = null)
        {
            (int code, string str) = await MakeRequest<string>(path, method, payload);
            return code;
        }
        private static async Task<(int code, T res)> MakeRequest<T>(string path, string method = "GET", string payload = null)
        {
            try
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
            catch (WebException e)
            {
                return ((int)(e.Response as HttpWebResponse).StatusCode, default(T));
            }
            catch (Exception e)
            {
                throw;
            }


        }
    }


    public class ImportArgs
    {
        [JsonRequired]
        public Guid JobID { get; set; }
        [JsonRequired]
        public string FileName { get; set; }
        [JsonRequired]
        public string LogName { get; set; }
        [JsonRequired]
        public string GroupingField { get; set; }
        [JsonRequired]
        public string TimeField { get; set; }
        [JsonRequired]
        public string GroupingFieldType { get; set; }
        [JsonRequired]
        public string TimeFieldType { get; set; }
        [JsonRequired]
        public char CsvDelimiter { get; set; }
        //another data here
    }
}
