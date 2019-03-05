using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Model;
using LogEntity;
using Microsoft.ClearScript.V8;
using Newtonsoft.Json;

namespace ClientApp.LogExplorer.Controller
{
    class ColorifyMachine
    {
        public static void Colorify(IEnumerable<LogTraceWithLabels> traces, List<Rule> activeRules)
        {
            using (var v8 = new V8ScriptEngine())
            {
                v8.AddHostType("Console", typeof(Console));
                foreach (var rule in activeRules)
                {
                    try
                    {
                        v8.Execute("var " + rule.Name + " = " + rule.Js + ";");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Rule " + rule.Name + " is wrong, cannot colorify");
                        return;
                    }
                }
                foreach (var logTrace in traces)
                {
                    logTrace.ItemsLabels.Clear();//remove old info if there is
                    foreach (var item in logTrace.Items)
                    {
                        List<string> labels = new List<string>();
                        var toPass = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(item));
                        foreach (var rule in activeRules)
                        {
                            if (v8.Script[rule.Name](toPass))
                            {
                                labels.Add(rule.Name);
                            }
                        }
                        logTrace.ItemsLabels.Add(labels.ToArray());
                    }
                }
            }

        }
    }
}
