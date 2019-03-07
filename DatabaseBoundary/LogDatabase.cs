using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogEntity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseBoundary
{
    public class LogDatabase
    {
        public const string DATA_COL_NAME = "data";
        private readonly IMongoDatabase db;
        public IMongoDatabase Db => db;
        public readonly string Name;

        public LogInfo Info
        {
            get
            {
                var col = db.GetCollection<BsonDocument>(DATA_COL_NAME);
                //TODO: handle if does not exists

                var info = new LogInfo()
                {
                    Name = Name,
                    ItemsCount = col.Count(new BsonDocument())
                };
                return info;
            }
        }

        public LogDatabase(IMongoDatabase db, string name)
        {
            this.db = db;
            Name = name;
        }

        public IEnumerable<LogTrace> GetTracesAtPos(long pos, long count)
        {
            var col = db.GetCollection<LogTrace>(DATA_COL_NAME);
            
            var fb = Builders<LogTrace>.Filter;
            var filterGte = fb.Gte(t => t._pos, pos);
            var filterLt = fb.Lt(t => t._pos, pos + count);
            var filter = fb.And(filterGte, filterLt);
            //for performance, change this to bson redirect
            var res =  col.FindSync(filter).ToEnumerable();
            return res;
        }
    }
}
