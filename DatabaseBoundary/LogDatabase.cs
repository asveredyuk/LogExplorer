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
        public const string LABELS_COL_NAME = "labels";
        public const string MAPS_COL_NAME = "maps";
        public const string LABEL_CACHE_COL_PREFIX = "labelcache_";
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

        public IMongoCollection<LogTrace> GetTracesCollection()
        {
            return db.GetCollection<LogTrace>(DATA_COL_NAME);
        }

        public IMongoCollection<ProcessMap> GetMapsCollection()
        {
            return db.GetCollection<ProcessMap>(MAPS_COL_NAME);
        }

        public void AddMap(ProcessMap map)
        {
            GetMapsCollection().InsertOne(map);
        }        
        

        public IEnumerable<LogLabel> GetLabels()
        {
            var col = db.GetCollection<LogLabel>(LABELS_COL_NAME);
            return col.AsQueryable();
        }

        public void AddLabel(LogLabel label)
        {
            var col = db.GetCollection<LogLabel>(LABELS_COL_NAME);
            col.InsertOne(label);
        }

        public void DeleteLabel(string id)
        {
            var col = db.GetCollection<LogLabel>(LABELS_COL_NAME);
            col.DeleteOne(t=>t._id==id);
            ClearLabelCache(id);
            //TODO: check if really deleted
        }

        public void UpdateLabel(LogLabel label)
        {
            var col = db.GetCollection<LogLabel>(LABELS_COL_NAME);
            var res = col.ReplaceOne(t=>t._id == label._id, label);
            ClearLabelCache(label._id);
            //TODO: check if really replaced
        }

        public bool CollectionExists(string collectionName)
        {
            return CollectionExists(db, collectionName);
        }
        static bool CollectionExists(IMongoDatabase database, string collectionName)
        {
            var filter = new BsonDocument("name", collectionName);
            var options = new ListCollectionNamesOptions { Filter = filter };

            return database.ListCollectionNames(options).Any();
        }

        public string MakeCacheNameForLabel(string labelId)
        {
            return LABEL_CACHE_COL_PREFIX + labelId;
        }

        public bool LabelHasCache(string labelId)
        {
            return CollectionExists(MakeCacheNameForLabel(labelId));
        }

        private void ClearLabelCache(string labelId)
        {
            if(LabelHasCache(labelId))
                db.DropCollection(MakeCacheNameForLabel(labelId));
        }



    }
}
