using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.LogExplorer.View.RecycleListView
{
    public partial class RecycleListView<T> : UserControl
    {
        private List<RecycleLIstViewItemControl<T>> controlPool = new List<RecycleLIstViewItemControl<T>>();
        private RecycleListViewAdapter<T> _adapter;

        public RecycleListViewAdapter<T> Adapter
        {
            get { return _adapter; }
            set
            {
                if(_adapter != null)
                    throw new Exception("Cannot set adapter multiple times");
                _adapter = value;
                _adapter.OnDataChanged += AdapterOnOnDataChanged;
            }
        }

        private void AdapterOnOnDataChanged()
        {
            DoRefresh();
        }

        void DoRefresh()
        {
            var scroll = flowLayoutPanel.VerticalScroll.Value;
            while (Adapter.Data.Count > controlPool.Count)
            {
                var newControl = Adapter.ControlSpawner();
                newControl.OnDataDirty += ItemControlOnOnDataDirty;

                controlPool.Add(newControl);
            }
            flowLayoutPanel.Controls.Clear();
            for (int i = 0; i < Adapter.Data.Count; i++)
            {
                var data = Adapter.Data[i];
                var nowControl = controlPool[i];
                nowControl.BindData(data);
                nowControl.Width = flowLayoutPanel.Width-30;
                flowLayoutPanel.Controls.Add(nowControl);
            }

            flowLayoutPanel.VerticalScroll.Value = scroll;
        }

        private void ItemControlOnOnDataDirty()
        {
            Adapter.OnDataDirty?.Invoke();
        }

        public RecycleListView()
        {
            InitializeComponent();
        }

    }
}
