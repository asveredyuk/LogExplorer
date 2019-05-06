using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AppServer
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                string arg = args[0];
                if (arg == "makeconfig")
                {
                    if (arg.Length < 2)
                    {
                        ServerConfig.Config.FILE_PATH = args[1];
                    }

                    var config = ServerConfig.Config.Self;
                    var json = JsonConvert.SerializeObject(config, Formatting.Indented);
                    File.WriteAllText(ServerConfig.Config.FILE_PATH, json);
                    Console.WriteLine("Config file written");
                    return;
                }
                //this first item is a path to config file
                ServerConfig.Config.FILE_PATH = args[0];
            }
            Server serv = new Server();
            serv.Start();
            Console.ReadLine();
        }
    }
}
