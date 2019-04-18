using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.LogExplorer.ProcessMapEditor
{
    public partial class OpenMapDialog : Form
    {
        private readonly string _logname;
        private ApiBoundary.MapListItem[] maps;

        public string Result { get; private set; }
        public OpenMapDialog(string logname)
        {
            _logname = logname;
            InitializeComponent();
        }

        private async void OpenMapDialog_Load(object sender, EventArgs e)
        {
            maps = await ApiBoundary.GetMapList(_logname);
            listBox1.Items.AddRange(maps.Select(t=>t.Name).Cast<object>().ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            Result = maps[index].Id;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
