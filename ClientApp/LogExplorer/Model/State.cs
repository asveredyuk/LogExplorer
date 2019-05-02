using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using LogEntity;

namespace ClientApp.LogExplorer.Model
{
    [DataContract]
    public class State : IState
    {
        
        public LogInfo Info { get; set; } //may be null
        public LazyLog Log { get; set; }  = new LazyLog();
        public long TracesInView { get; set; } = 10;
        public long Pos { get; set; } 

        public string ActiveLabelProfile { get; set; }
        //public Dictionary<string, Rule> Rules { get; set; } = new Dictionary<string, Rule>();
        public List<LogLabel> Labels { get; set; } = new List<LogLabel>();

        public IEnumerable<string> LabelProfiles
        {
            get { return Labels.Select(t => t.ProfileName).Distinct(); }
        }


        public static State Default()
        {
            return new State();
            //var d = new Dictionary<string, Rule>();
            ////d["test"] = (new Rule()
            ////{
            ////    Name = "test",
            ////    Color = Color.Red,
            ////    Enabled = false,
            ////    Js = "a"
            ////});
            //return new State()
            //{
            //    Log = new LazyLog(),
            //    Rules = d,
            //    TracesInView = 10
            //};
        }


    }
}
