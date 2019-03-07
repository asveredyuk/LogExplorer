using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DatabaseBoundary
{
    public class Database
    {
        public const string LOG_DATA_COL_NAME = "data";
        public const string LOG_DATABASE_PREFIX = "_log_";
        private static Database self;

        public static Database Self
        {
            get
            {
                if (self == null)
                    self = new Database();
                return self;
                
            }
        }

        private MongoClient client;

        private Database()
        {
            this.client = new MongoClient();
        }

        public IEnumerable<string> GetLogNames()
        {
            return client.ListDatabases().ToEnumerable()
                .Select(t=>t["name"].AsString)
                .Where(t => t.StartsWith(LOG_DATABASE_PREFIX))
                .Select(t => t.Substring(LOG_DATABASE_PREFIX.Length));
        }

        public IMongoDatabase GetLogDatabase(string name)
        {
            return client.GetDatabase(LOG_DATABASE_PREFIX + name);
        }
    }
}
