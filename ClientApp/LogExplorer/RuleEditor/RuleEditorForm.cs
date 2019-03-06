using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Model;
using FastColoredTextBoxNS;
using DialogResult = System.Windows.Forms.DialogResult;

namespace ClientApp.LogExplorer.RuleEditor
{
    public partial class RuleEditorForm : Form
    {
        public Rule Result { get; private set; }
        public RuleEditorForm()
        {
            InitializeComponent();
            foreach (var tbJsStyle in tbJs.Styles)
            {
                if (tbJsStyle is TextStyle textStyle)
                    textStyle.FontStyle = FontStyle.Regular;
            }
            DialogResult = DialogResult.Cancel;
        }

        public RuleEditorForm(Rule r) : this()
        {
            tbName.Text = r.Name;
            tbJs.Text = r.Js;
            tbText.Text = r.Text;
            tbColorHex.Text = ColorTranslator.ToHtml(r.Color);
        }

        private void tbColorHex_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var color = ColorTranslator.FromHtml(tbColorHex.Text);
                pbColorExample.BackColor = color;
                tbColorHex.BackColor = Color.White;
            }
            catch (Exception exception)
            {
               tbColorHex.BackColor = Color.Red;
                Console.WriteLine("Bad");
            }
        }

        private void btColorPick_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                var res = colorDialog.Color;
                tbColorHex.Text = ColorTranslator.ToHtml(res);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            //TODO: validate something?
            Result = new Rule()
            {
                Name = tbName.Text,
                Js = tbJs.Text,
                Text = tbText.Text,
                Color = pbColorExample.BackColor
            };

            this.DialogResult = DialogResult.OK;
        }

        private void RuleEditorForm_Load(object sender, EventArgs e)
        {
            if(tbColorHex.Text == "")
                tbColorHex.Text = "DarkGreen";
        }
    }
}
