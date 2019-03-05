using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp
{
    public partial class OpenLogDialog : Form
    {
        public string Result { get; private set; }

        public OpenLogDialog()
        {
            InitializeComponent();
        }

        private async void OpenLogDialog_Load(object sender, EventArgs e)
        {
            var list = await ApiBoundary.GetLogsNames();
            if (list == null)
            {
                //failed, do not know what to do?
                this.DialogResult = DialogResult.Abort;
                Close();
            }

            listBox1.Items.AddRange(list.Cast<object>().ToArray());
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedItem == null)
                return;
            Result = listBox1.SelectedItem.ToString();
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
