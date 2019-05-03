using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogEntity;

namespace ClientApp.LogExplorer.Model
{
    public static class ProcessMapRelationExt
    {
        public static bool IsStartOrEnd(this ProcessMapRelation rel)
        {
            var ids = new string[] {rel.labelFrom, rel.labelTo};
            var startend = new string[]{LogLabel.START_ID,LogLabel.END_ID};
            return ids.Any(t => startend.Contains(t));
        }
    }
}
