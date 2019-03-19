using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Model;
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp.LogExplorer.View
{
    class MainViewClickHandler
    {
        private List<(RectangleF rect, LogTraceWithLabels trace, int itemPos)> rects = new List<(RectangleF rect, LogTraceWithLabels trace, int itemPos)>();

        public void Reset()
        {
            rects.Clear();
        }
        public void PushItemRect(RectangleF f, LogTraceWithLabels trace, int itemPos)
        {
            rects.Add((f,trace,itemPos));
        }

        public void PushTraceRect(RectangleF f, LogTraceWithLabels trace)
        {
            rects.Add((f,trace,-1));
        }

        public void HandleClick(PointF pt)
        {
            var item = rects.FirstOrDefault(t => t.rect.Contains(pt));
            if(item.trace == null)
                return;
            if(item.itemPos == -1)
                MessageBox.Show("Trace");
            else
            {

                new JsonViewForm(JsonConvert.SerializeObject(item.trace.Items[item.itemPos], Formatting.Indented)).Show();
                //MessageBox.Show("Item");
            }
        }
    }
}
