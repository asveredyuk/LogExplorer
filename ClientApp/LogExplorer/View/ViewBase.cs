using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientApp.LogExplorer.Controller;
using ClientApp.LogExplorer.Model;

namespace ClientApp.LogExplorer.View
{
    abstract class ViewBase
    {
        public readonly LogExplorerController Controller;
        protected IState State => Controller.State;

        protected ViewBase(LogExplorerController controller)
        {
            Controller = controller;
        }

        public abstract void Refresh();
    }
}
