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
using ClientApp.LogExplorer.ProcessMapEditor;
using ClientApp.LogExplorer.View;
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp
{
    public partial class LogExplorerForm : Form
    {
        private LogExplorerController _controller;
        public LogExplorerForm()
        {
            _controller = new LogExplorerController();
            InitializeComponent();
            _controller.BindForm(this);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dialog = new OpenLogDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _controller.OpenNewLog(dialog.Result);
            }
        }

        private void LogExplorerForm_Load(object sender, EventArgs e)
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length == 2)
            {
                _controller.OpenNewLog(args[1]);
            }
        }

        private void editorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.GoEditRules();
        }

        private void jobEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Form1().ShowDialog();
        }

        private void newEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _controller.GoEditRulesNew();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProcessMapForm form = new CreateProcessMapForm(_controller);
            form.Show();
        }

        private void openToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenMapDialog dialog = new OpenMapDialog(_controller.State.Info.Name);
            var res = dialog.ShowDialog();
            if (res == DialogResult.OK)
            {
                var id = dialog.Result;
                ProcessMapForm form = new ProcessMapForm(_controller, id);
                form.ShowDialog();
            }

        }
    }
}
