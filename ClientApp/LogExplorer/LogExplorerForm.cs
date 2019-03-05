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
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp
{
    public partial class LogExplorerForm : Form
    {
        private LogExplorerController controller;
        public LogExplorerForm()
        {
            controller = new LogExplorerController();
            InitializeComponent();
            controller.BindForm(this);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenLogDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                controller.OpenNewLog(dialog.Result);
            }
        }

        private void LogExplorerForm_Load(object sender, EventArgs e)
        {
            
        }

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.GoEditRules();
        }

        private void jobEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
        }
    }
}
