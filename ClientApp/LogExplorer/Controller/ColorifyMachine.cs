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
        public static void Colorify(List<LabelledLogTraceExt> traces, List<LogLabel> activeRules)
        {
            var sw = Stopwatch.StartNew();
            
            foreach (var trace in traces)
            {
                trace.Compile();
            }
            using (var v8 = new V8ScriptEngine())
            {
                int colorifiedOnClientCount = 0;
                int colorifiedOnServerCount = 0;
                v8.AddHostType("Console", typeof(Console));
                foreach (var rule in activeRules)
                {
                    try
                    {
                        v8.Execute("var " + "o" + " = " + rule.JSFilter + ";");
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show("Rule " + rule.Name + " is wrong, cannot colorify");
                        return;
                    }

                    bool wasJsUsed = false;
                    foreach (var trace in traces)
                    {

                        if(trace.HasLabelPrecompiled(rule._id))
                            continue;
                        wasJsUsed = true;
                        for (int i = 0; i < trace.Items.Count; i++)
                        {
                            var item = trace.Items[i];
                            var toPass = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(item));
                            if (v8.Script["o"](toPass))
                            {
                                trace.ItemsLabels[i].Add(rule._id);
                            }
                        }
                    }

                    if (wasJsUsed)
                    {
                        colorifiedOnClientCount++;
                    }
                    else
                    {
                        colorifiedOnServerCount++;
                    }
                }

                Console.WriteLine("--colorify info--");
                Console.WriteLine("Total traces " + traces.Count);
                Console.WriteLine("Rules on server " + colorifiedOnServerCount);
                Console.WriteLine("Rules on client " + colorifiedOnClientCount);
                sw.Stop();
                Console.WriteLine("Time elapsed " + sw.ElapsedMilliseconds + "ms");
                //foreach (var rule in activeRules)
                //{
                //    try
                //    {
                //        v8.Execute("var " + "o" + " = " + rule.JSFilter + ";");
                //    }
                //    catch (Exception e)
                //    {
                //        MessageBox.Show("Rule " + rule.Name + " is wrong, cannot colorify");
                //        return;
                //    }
                //}
                //foreach (var logTrace in traces)
                //{
                //    logTrace.ItemsLabels.Clear();//remove old info if there is
                //    foreach (var item in logTrace.Items)
                //    {
                //        List<string> labels = new List<string>();
                //        var toPass = JsonConvert.DeserializeObject<ExpandoObject>(JsonConvert.SerializeObject(item));
                //        foreach (var rule in activeRules)
                //        {
                //            if (v8.Script["o"](toPass))
                //            {
                //                labels.Add(rule._id);
                //            }
                //        }
                //        logTrace.ItemsLabels.Add(labels.ToArray());
                //    }
                //}
            }

        }
    }
}
