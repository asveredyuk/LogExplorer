using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Controller;

namespace ClientApp.LogExplorer.View
{
    class MainScrollView : ViewBase
    {
        private VScrollBar scroll;
        public MainScrollView(LogExplorerController controller) : base(controller)
        {
        }

        public override void Refresh()
        {
            if (State.Info != null)
            {
                //extract const to another
                //set max scroll val
                //if (State.Info.ItemsCount > int.MaxValue)
                //{
                //    //we use max int
                //    scroll.Maximum = int.MaxValue;
                //}
                //else
                //{
                //    scroll.Maximum = (int)State.Info.ItemsCount;
                //}
                this.scroll.Maximum = (int) State.Info.ItemsCount;
                this.scroll.Value = (int) State.Pos; //(int) (State.Pos * scroll.Maximum / State.Info.ItemsCount);
            }
        }

        public void Bind(VScrollBar bar)
        {
            scroll = bar;
            scroll.ValueChanged += ScrollOnValueChanged;
        }

        private void ScrollOnValueChanged(object sender, EventArgs eventArgs)
        {
            Controller.ChangePos(scroll.Value, this);
            //Controller.ChangePos(scroll.Value * State.Info.ItemsCount / scroll.Maximum, this);
        }
    }
}
