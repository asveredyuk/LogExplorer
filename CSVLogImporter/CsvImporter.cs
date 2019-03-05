using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSystem;
using JobSystem.Jobs;
using Newtonsoft.Json;

namespace CSVLogImporter
{
    class CsvImporter
    {
        //const char SPLITTER = GlobalConsts.CSV_SPLITTER;
        public static int Import(ImportJob job)
        {

            Program.Progress.Progress.CurrentStage = "Splitting log into parts";
            Program.Progress.Progress.CurrentStagePercentage = 0;
            Program.Progress.CommitProgress();
            int counter = 0; //counts amount of items
            int groupCounter = 0; //counts each group
            
            
            //FileInfo csvFi = new FileInfo(csvFname);
            //long csv
            Dictionary<string, List<string[]>> dict = new Dictionary<string, List<string[]>>();
            using (var sr = new StreamReader(job.Input.CsvFileName))
            {
                string[] headers = sr.ReadLine().Split(job.Input.CsvDelimiter);
                File.WriteAllText(job.TmpFolder + "schema", JsonConvert.SerializeObject(headers));

                int groupingIndex = Array.IndexOf(headers, job.GroupingField.name);

                Stopwatch swRead = Stopwatch.StartNew();

                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(job.Input.CsvDelimiter);
                    string key = line[groupingIndex];
                    List<string[]> buf;
                    if (!dict.TryGetValue(key, out buf))
                    {
                        buf = new List<string[]>();
                        dict[key] = buf;
                    }

                    //todo: remove key from the line, it is contained in dict
                    buf.Add(line);
                    counter++;
                    if (counter % 1000 == 0)
                    {
                        long percentage = sr.BaseStream.Position * 100 / sr.BaseStream.Length;
                        long memory = GC.GetTotalMemory(false);
                        Console.Write("Done " + counter + " Mem : " + Util.MemConverter(memory) + " Percentage:" + percentage + "                 \r");

                        Program.Progress.Progress.CurrentStagePercentage = (int)percentage;
                        Program.Progress.CommitProgress();


                        if (memory >GlobalConsts.MEM_LIMIT)
                        {
                            Console.WriteLine();
                            Console.WriteLine("Memory limit reached");
                            Console.WriteLine("Grouped : " + dict.Count);
                            swRead.Stop();
                            Console.WriteLine("Read done in " + swRead.ElapsedMilliseconds + " ms");

                            Console.WriteLine("Writing to part file...");

                            var swWrite = Stopwatch.StartNew();
                            WritePart("part" + groupCounter, job.TmpFolder, dict);
                            swWrite.Stop();
                            Console.WriteLine("written in " + swWrite.ElapsedMilliseconds + " ms");
                            //Console.ReadLine();
                            dict.Clear();
                            GC.Collect();
                            //Console.ReadLine();
                            counter = 0;
                            groupCounter++;
                            swRead = Stopwatch.StartNew();
                        }
                    }
                }

                var swWriteq = Stopwatch.StartNew();
                WritePart("part" + groupCounter, job.TmpFolder, dict);
                swWriteq.Stop();
                Console.WriteLine("written in " + swWriteq.ElapsedMilliseconds + " ms");
                Console.WriteLine("done");
                //var sww = Stopwatch.StartNew();
                //sww.Stop();
                //Console.WriteLine();
                //Console.WriteLine(sww.ElapsedMilliseconds);
                //Console.ReadLine();

            }

            return 0;
        }
        static void WritePart(string name, string dir, Dictionary<string, List<string[]>> dict)
        {
            int done = 0;
            using (StreamWriter swMeta = new StreamWriter(dir + name + ".meta"))
            {
                using (StreamWriter swData = new StreamWriter(dir + name + ".data") { AutoFlush = true })
                {
                    foreach (var kv in dict)
                    {
                        long posBegin = swData.BaseStream.Position;
                        foreach (var item in kv.Value)
                        {
                            swData.WriteLine(string.Join(GlobalConsts.DATA_SPLITTER.ToString(), item));
                        }
                        swData.WriteLine();
                        swMeta.WriteLine(kv.Key + GlobalConsts.META_SPLITTER + posBegin);
                        done++;
                        Util.ProgressEachFraction(done, dict.Count, 100);
                    }
                }
            }

            Console.WriteLine();



        }
    }
}
