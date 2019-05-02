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

        public IEnumerable<LabeledLogTrace> GetTracesWithLabelsAtPos(long pos, long count, string[] labelIds)
        {
            List<BsonDocument> stages = new List<BsonDocument>();
            var stage1 = new BsonDocument
            {
                {
                    "$match" , new BsonDocument
                    {
                        {
                            "_pos", new BsonDocument
                            {
                                {"$gte",pos },
                                {"$lt",pos + count }
                            }

                        }
                    }
                }
            };
            stages.Add(stage1);
            foreach (var labelId in labelIds)
            {
                if(!LabelHasCache(labelId))
                    continue;
                var substage1 = new BsonDocument
                {
                    {
                        "$lookup", new BsonDocument
                        {
                            { "from",MakeCacheNameForLabel(labelId)},
                            {"localField","_id" },
                            {"foreignField", "_id"},
                            {"as","Filters." + labelId}
                        }
                    }
                };
                var substage2 = new BsonDocument
                {
                    {
                        "$addFields", new BsonDocument
                        {
                            {
                                "Filters." + labelId, new BsonDocument
                                {
                                    {
                                        "$arrayElemAt",new BsonArray
                                        {
                                            "$Filters." + labelId + ".value",
                                            0
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
                var substage3 = new BsonDocument
                {
                    {
                        "$addFields", new BsonDocument
                        {
                            {
                                "Filters." + labelId, new BsonDocument
                                {
                                    {
                                        "$ifNull", new BsonArray
                                        {
                                            "$Filters." + labelId,
                                            new BsonArray()
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
                stages.Add(substage1);
                stages.Add(substage2);
                stages.Add(substage3);
            }

            var pipeline = PipelineDefinition<LogTrace, LabeledLogTrace>.Create(stages);
            return GetTracesCollection().Aggregate(pipeline).ToEnumerable();

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

        public ProcessMap GetMap(string id)
        {
            var col = GetMapsCollection();
            var map = col.Find(t => t._id == id).FirstOrDefault();
            return map;
        }

        public IEnumerable<ProcessMap> GetMaps()
        {
            return GetMapsCollection().AsQueryable();
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

        public IEnumerable<string> GetFieldNames()
        {
            var data = GetTracesCollection();
            var oneInList = data.Find(t => true).Limit(1).ToList();
            if (oneInList.Count != 1)
            {
                throw new Exception("NO data in traces collections");
            }
            //TODO: is it enough?
            var trace = oneInList[0];
            var record = trace.Items[0];
            return record.Keys;
        }
        /// <summary>
        /// Get all distinct values of given field in logrecord
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="success"></param>
        /// <returns></returns>
        public IEnumerable<string> GetDistinctFieldValues(string fieldName, out bool success)
        {
            var data = GetTracesCollection();
            int testNum = 1000;
            int maxDistinctValues = 100;

            var dfvpipeline = DFVPipeline(fieldName).ToArray();
            var limit = LimitPipelineStage(testNum);
            
            var testPipelineStages = new List<BsonDocument>();
            testPipelineStages.Add(limit);
            testPipelineStages.AddRange(dfvpipeline);

            var testPipeline = PipelineDefinition<LogTrace, BsonDocument>.Create(testPipelineStages);
            var testRes = data.Aggregate(testPipeline).ToList();
            if (testRes.Count > maxDistinctValues)
            {
                success = false;
                return testRes.Select(t => t.GetValue("_id").AsString);
            }

            if (testRes.Count == 1)
            {
                if (testRes[0].GetValue("_id").IsBsonNull)
                {
                    success = false;
                    return null;
                }
            }

            var pipeline = PipelineDefinition<LogTrace, BsonDocument>.Create(dfvpipeline);
            var res = data.Aggregate(pipeline).ToList();
            if (res.Count > maxDistinctValues)
            {
                success = false;
                return res.Take(maxDistinctValues).Select(t=>t.GetValue("_id").AsString);
            }
            success = true;
            return res.Select(t => t.GetValue("_id").AsString);



        }

        private IEnumerable<BsonDocument> DFVPipeline(string fieldname)
        {
            var stage1 = new BsonDocument
            {
                {
                    "$unwind", new BsonDocument
                    {
                        {"path","$Items" }
                    }
                }
            };
            var stage2 = new BsonDocument
            {
                {
                    "$group", new BsonDocument
                    {
                        {"_id","$Items." + fieldname }
                    }
                }
            };
            yield return stage1;
            yield return stage2;
        }

        private BsonDocument LimitPipelineStage(int limit)
        {
            var stage1 = new BsonDocument
            {
                {
                    "$limit", limit
                }
            };
            return stage1;
        }



    }
}
