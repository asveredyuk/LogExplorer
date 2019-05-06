using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DatabaseBoundary
{
    public class DatabaseClient
    {
        public const string LOG_DATABASE_PREFIX = "_log_";

        public static DatabaseConnectionInfo ConnectionInfo { get; set; }
        private static DatabaseClient self;

        public static DatabaseClient Self
        {
            get
            {
                if (self == null)
                    self = new DatabaseClient();
                return self;
                
            }
        }

        private MongoClient client;

        private DatabaseClient()
        {
            if (ConnectionInfo == null)
            {
                var colorWas = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("! default mongo connection is used !");
                Console.ForegroundColor = colorWas;
                this.client = new MongoClient();
                
            }
            else
            {
                //var settings = new MongoClientSettings();
                //settings.Server = MongoServerAddress.Parse(ConnectionInfo.Server);
                //settings.
                var mongourlBuilder = new MongoUrlBuilder();
                mongourlBuilder.Server = MongoServerAddress.Parse(ConnectionInfo.Server);
                mongourlBuilder.Username = ConnectionInfo.Username;
                mongourlBuilder.Password = ConnectionInfo.Password;
                this.client = new MongoClient(mongourlBuilder.ToMongoUrl());
            }
            //TODO: implement connection
            
            //this.client = new MongoClient();
        }

        public IEnumerable<string> GetLogNames()
        {
            return client.ListDatabases().ToEnumerable()
                .Select(t=>t["name"].AsString)
                .Where(t => t.StartsWith(LOG_DATABASE_PREFIX))
                .Select(t => t.Substring(LOG_DATABASE_PREFIX.Length));
        }

        public LogDatabase GetLogDatabase(string name)
        {
            var db = client.GetDatabase(LOG_DATABASE_PREFIX + name);
            return new LogDatabase(db, name);
        }
    }
}
