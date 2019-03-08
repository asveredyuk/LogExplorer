using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AppServer
{
    static class DebugLogger
    {
        public const bool IsEnabled = true;

        public static void LogRequest(HttpListenerRequest req, HttpListenerResponse resp)
        {
            var str = req.HttpMethod.ToUpper() + ":" + req.Url.AbsolutePath + "\t\t\t" + resp.StatusCode + " " + resp.StatusDescription;
            if(resp.StatusCode == 200)
                PrintInColor(str, ConsoleColor.Green);
            else if(resp.StatusCode > 200 && resp.StatusCode < 500)
                PrintInColor(str, ConsoleColor.Yellow);
            else if(resp.StatusCode >= 500 )
                PrintInColor(str,ConsoleColor.Red);
            else
                Console.WriteLine(str);

        }        
        static void PrintInColor(string data, ConsoleColor color)
        {
            var wasColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(data);
            Console.ForegroundColor = wasColor;

        }
    }
}
