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
        public const string START_ID = "00000000-0000-0000-0000-000000000000";
        public const string END_ID = "ffffffff-ffff-ffff-ffff-ffffffffffff";

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

        public static LogLabel MakeStart()
        {
            return new LogLabel()
            {
                _id = START_ID,
                Color = "Green",
                JSFilter = "",
                Name = "Start",
                ProfileName = "",
                Text = "Start"
            };
        }

        public static LogLabel MakeEnd()
        {
            return new LogLabel()
            {
                _id = END_ID,
                Color = "Blue",
                JSFilter = "",
                Name = "End",
                ProfileName = "",
                Text = "End"
            };
        }
    }
}
