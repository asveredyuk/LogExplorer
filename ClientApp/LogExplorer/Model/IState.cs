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
        Dictionary<string, Rule> Rules { get; }
        long TracesInView { get; }
        long Pos { get; }
    }
}
