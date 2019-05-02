using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace LogEntity
{
    public class LogLabel
    {
        public string _id;
        /// <summary>
        /// The profile of the label
        /// </summary>
        public string ProfileName = "default";
        /// <summary>
        /// The name of the label
        /// </summary>
        public string Name;
        /// <summary>
        /// Javascript filter function
        /// </summary>
        public string JSFilter;
        /// <summary>
        /// Color in hex
        /// </summary>
        public string Color;
        /// <summary>
        /// Short text to display
        /// </summary>
        public string Text;
        [BsonIgnore]
        public bool HasCache;
    }
}
