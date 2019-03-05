using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ClientApp.LogExplorer.Model
{
    public class Rule
    {
        [JsonIgnore]
        public bool Enabled { get; set; }
        /*
         * should be valid variable name for javascript engine
         */
        public string Name { get; set; }

        /*in format :
        function(o){
            //comparrison here, return bool
            return o.cache.animal == "cat"
        }

        */
        public string Js { get; set; }

        //display options
        public Color Color { get; set; }
        public string Text { get; set; }
    }
}
