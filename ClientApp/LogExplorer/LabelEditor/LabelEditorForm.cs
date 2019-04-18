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
using LogEntity;
using DialogResult = System.Windows.Forms.DialogResult;

namespace ClientApp.LogExplorer.LabelEditor
{
    public partial class LabelEditorForm : Form
    {
        private string id = Guid.NewGuid().ToString();
        public LogLabel Result { get; private set; }
        private string _profileName;
        public LabelEditorForm(string profileName)
        {
            InitializeComponent();
            _profileName = profileName;
            foreach (var tbJsStyle in tbJs.Styles)
            {
                if (tbJsStyle is TextStyle textStyle)
                    textStyle.FontStyle = FontStyle.Regular;
            }
            DialogResult = DialogResult.Cancel;
        }

        public static (DialogResult res, LabelEditorForm form) GoEdit(LogLabel lb)
        {
            LabelEditorForm form =  new LabelEditorForm(lb);
            var res = form.ShowDialog();
            return (res, form);
        }

        public static (DialogResult res, LabelEditorForm form) GoCreate(string profileName)
        {
            var form = new LabelEditorForm(profileName);
            var res = form.ShowDialog();
            return (res, form);
        }

        public LabelEditorForm(LogLabel r) : this(r.ProfileName)
        {
            tbName.Text = r.Name;
            tbJs.Text = r.JSFilter;
            tbText.Text = r.Text;
            tbColorHex.Text = r.Color;
            id = r._id;
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
            Result = new LogLabel()
            {
                _id = id,
                ProfileName = _profileName,
                Name = tbName.Text,
                JSFilter = tbJs.Text,
                Text = tbText.Text,
                Color = ColorTranslator.ToHtml(pbColorExample.BackColor)
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
