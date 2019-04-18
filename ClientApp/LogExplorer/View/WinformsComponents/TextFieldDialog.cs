using System;
using System.Windows.Forms;

namespace ClientApp.LogExplorer.View.WinformsComponents
{
    public partial class TextFieldDialog : Form
    {
        public string Result { get; private set; }
        public TextFieldDialog(string caption, string labelText, string defaultText = "")
        {
            InitializeComponent();
            Text = caption;
            label1.Text = labelText;
            textBox.Text = defaultText;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            Result = textBox.Text;
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
