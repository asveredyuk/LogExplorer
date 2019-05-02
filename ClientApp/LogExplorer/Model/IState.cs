using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogEntity;

namespace ClientApp.LogExplorer.Model
{
    public interface IState
    {
        LogInfo Info { get; }
        LazyLog Log { get; }
        List<LogLabel> Labels { get; }
        long TracesInView { get; }
        long Pos { get; }

        IEnumerable<string> LabelProfiles { get; }
        string ActiveLabelProfile { get; }
    }
}
