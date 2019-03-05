using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSVLogImporter
{
    //compiles multiple meta files into one big, including intersections
    class MetaCompiler
    {
        public static int Compile(string dataDir)
        {
            Program.Progress.Progress.CurrentStage = "Compiling meta info";
            Program.Progress.Progress.CurrentStagePercentage = 0;
            Program.Progress.CommitProgress();

            Console.WriteLine("Compiling metas");
            var stw = Stopwatch.StartNew();

            List<Dictionary<string, string>> metas = new List<Dictionary<string, string>>(
                Directory.GetFiles(dataDir, "*.meta").Select(Read)
            );
            List<string> indexes = new List<string>();
            StreamWriter sw = new StreamWriter(dataDir + "\\compiled.meta");
            long totalTraces = 0;
            for (int i = 0; i < metas.Count; i++)
            {
                Program.Progress.Progress.CurrentStagePercentage = i * 100 / metas.Count;
                Program.Progress.CommitProgress();

                var kurMeta = metas[i];

                foreach (var kv in kurMeta)
                {
                    //todo: move to consts
                    indexes.Add($"{i}{GlobalConsts.META_COMPILED_PREFIX}{kv.Value}");
                    //check the other metas
                    for (int j = i + 1; j < metas.Count; j++)
                    {
                        string val;
                        if (metas[j].TryGetValue(kv.Key, out val))
                        {
                            metas[j].Remove(kv.Key);
                            indexes.Add($"{j}{GlobalConsts.META_COMPILED_PREFIX}{val}");
                        }
                    }
                    sw.WriteLine($"{kv.Key}{GlobalConsts.META_SPLITTER}{String.Join(GlobalConsts.META_COMPILED_MULTI.ToString(), indexes)}");
                    totalTraces++;
                    //release resources
                    indexes.Clear();
                }
            }
            sw.Close();
            stw.Stop();
            Console.WriteLine("Done in " + stw.ElapsedMilliseconds + "ms");
            Console.WriteLine("Total traces : " + totalTraces);
            File.WriteAllText(dataDir + "\\" + "compiled.count", totalTraces.ToString());
            //Console.ReadLine();
            return 0;
        }
        static Dictionary<string, string> Read(string fname)
        {
            var stw = Stopwatch.StartNew();

            Dictionary<string, string> meta0 = new Dictionary<string, string>();
            using (StreamReader sr = new StreamReader(fname))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] spl = line.Split(',');
                    meta0[spl[0]] = spl[1];
                }
            }
            stw.Stop();
            Console.WriteLine("Read time " + stw.ElapsedMilliseconds);
            return meta0;
        }
    }
}
