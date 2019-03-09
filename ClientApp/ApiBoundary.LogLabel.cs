using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp
{
    static partial class ApiBoundary
    {
        public static async Task<IEnumerable<LogLabel>> GetLabels(string logName)
        {
            (int code, IEnumerable<LogLabel> labels) = await MakeRequest<LogLabel[]>($"/logs/{logName}/labels");
            if (code != 200)
            {
                MessageBox.Show("Api error");
            }

            return labels;

        }

        public static async Task AddLabel(LogLabel label, string logName)
        {
            var json = JsonConvert.SerializeObject(label);
            int code  = await MakeRequest($"/logs/{logName}/labels", "POST", json);

            if (code != 200)
            {
                MessageBox.Show("Api error");
            }

        }

        public static async Task DeleteLabel(LogLabel label, string logName)
        {
            int code = await MakeRequest($"/logs/{logName}/labels/{label._id}", "DELETE");
            if (code != 200)
            {
                MessageBox.Show("Api error");
            }
        }

        public static async Task UpdateLabel(LogLabel label, string logName)
        {
            var json = JsonConvert.SerializeObject(label);

            int code = await MakeRequest($"/logs/{logName}/labels/{label._id}", "PATCH", json);
            if (code != 200)
            {
                MessageBox.Show("Api error");
            }
        }
    }
}
