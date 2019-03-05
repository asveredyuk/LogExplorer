using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace SublogExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient();
            var db = client.GetDatabase("test");
            var collection = db.GetCollection<LogTrace>("traces");
            collection.Indexes.CreateOne(Builders<LogTrace>.IndexKeys.Ascending("Cache.begin"));
            collection.Indexes.CreateOne(Builders<LogTrace>.IndexKeys.Ascending("Cache.end"));
            Console.WriteLine("Go!");
            var dt1 = DateTime.Parse("2015-10-05 07:12:56.880Z");
            var dt2 = dt1.AddDays(100);
            var filter = Builders<LogTrace>.Filter;
            var resFilter = filter.Gt("Cache.begin", dt1) & filter.Lt("Cache.begin", dt2) | filter.Gt("Cache.end", dt1) & filter.Lt("Cache.end", dt2);

            var inner = collection.Find(resFilter).ToEnumerable();
            foreach (var a in inner)
            {
                Console.WriteLine(a._id);
            }

            Console.ReadLine();
        }

        static bool CheckInInter(LogTrace lt, DateTime a, DateTime b)
        {
            return lt.Cache.begin > a && lt.Cache.begin < b || lt.Cache.end > a && lt.Cache.end < b;
        }
    }

    class LogTrace
    {

        public string _id;
        //public int time_first;
        //public int time_last;
        /// <summary>
        /// Cached items
        /// </summary>
        public dynamic Cache;
        public List<Dictionary<string, dynamic>> Items;

    }
}
