using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.LogExplorer.LabelEditor
{
    public partial class CreateProfileForm : Form
    {
        public string Result { get; private set; }
        public CreateProfileForm()
        {
            InitializeComponent();
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            Result = textBoxName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBoxName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                btOk_Click(sender,e);
        }
    }
}
