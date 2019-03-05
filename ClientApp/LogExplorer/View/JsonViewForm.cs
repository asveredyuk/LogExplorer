using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FastColoredTextBoxNS;

namespace ClientApp.LogExplorer.View
{
    public partial class JsonViewForm : Form
    {
        public JsonViewForm(string json, string title = "JsonView")
        {
            InitializeComponent();
            this.Text = title;
            foreach (var style in fctb.Styles)
            {
                if (style is TextStyle textStyle)
                    textStyle.FontStyle = FontStyle.Regular;
            }

            fctb.Text = json;
        }

        private void fctb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
                Close();
        }

        private void JsonViewForm_Load(object sender, EventArgs e)
        {
            int x = Cursor.Position.X;
            int y = Cursor.Position.Y;
            var b = Screen.FromControl(this).Bounds;
            if (x + Width > b.Width)
                x = b.Width - Width;
            if (y + Height > b.Height)
                y = b.Height - Height;
            this.Location = new Point(x,y);

        }
    }
}
