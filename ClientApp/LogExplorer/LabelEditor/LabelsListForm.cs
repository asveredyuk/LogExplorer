using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Controller;
using ClientApp.LogExplorer.Model;
using ClientApp.LogExplorer.View.RecycleListView;
using LogEntity;

namespace ClientApp.LogExplorer.LabelEditor
{
    public partial class LabelsListForm : Form
    {
        private RecycleListView<LogLabel> recycleListView;
        private RecycleListViewAdapter<LogLabel> adapter;

        private State _state;
        private LogExplorerController _controller;
        public LabelsListForm()
        {
            InitializeComponent();
        }

        public LabelsListForm(State state, LogExplorerController controller)
        {
            InitializeComponent();
            _state = state;
            _controller = controller;
            
        }

        private void LabelsList_Load(object sender, EventArgs e)
        {
            recycleListView = new RecycleListView<LogLabel>();
            recycleListView.Dock = DockStyle.Fill;
            panelForListView.Controls.Add(recycleListView);

            adapter = new RecycleListViewAdapter<LogLabel>(
                () => new LabelListItem(_controller), 
                ()=> adapter.UpdateData(_state.Labels));
            recycleListView.Adapter = adapter;

            adapter.UpdateData(_state.Labels);
        }

        private async void addButton_Click(object sender, EventArgs e)
        {
            var (res, form) = LabelEditorForm.GoEdit();
            if (res == DialogResult.OK)
            {
                await _controller.AddLabel(form.Result);
                adapter.UpdateData(_state.Labels);
            }
        }

        private void addButton1_Click(object sender, EventArgs e)
        {

        }
    }
}
