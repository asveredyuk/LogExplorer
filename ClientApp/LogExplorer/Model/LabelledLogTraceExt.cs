using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogEntity;

namespace ClientApp.LogExplorer.Model
{
    public class LabelledLogTraceExt : LabeledLogTrace
    {
        public List<List<string>> ItemsLabels = new List<List<string>>();

        public void Compile()
        {
            ItemsLabels.Clear();
            for (int i = 0; i < Items.Count; i++)
            {
                ItemsLabels.Add(new List<string>());
            }
            if(Filters == null)
                return;
            foreach (var filter in Filters)
            {
                foreach (var pos in filter.Value)
                {
                    ItemsLabels[pos].Add(filter.Key);
                }
            }
        }

        public bool HasLabelPrecompiled(string labelId)
        {
            if (Filters == null)
                return false;
            return Filters.ContainsKey(labelId);
        }


    }
}
