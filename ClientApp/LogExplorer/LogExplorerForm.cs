using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer;
using ClientApp.LogExplorer.Controller;
using ClientApp.LogExplorer.LabelEditor;
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
                ChangeLogIsOpen(true);
            }
        }

        private void LogExplorerForm_Load(object sender, EventArgs e)
        {
            var res = new ConnectionForm().ShowDialog();
            if (res != DialogResult.OK)
            {
                this.Close();
                return;
            }
            var args = Environment.GetCommandLineArgs();
            if (args.Length == 2)
            {
                _controller.OpenNewLog(args[1]);
            }
            ChangeLogIsOpen(false);
        }

        private void ChangeLogIsOpen(bool enabled)
        {
            mapToolStripMenuItem.Enabled = enabled;
            rulesToolStripMenuItem.Enabled = enabled;
            comboBoxProfile.Enabled = enabled;
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

        private void generateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LabelGenerateForm form = new LabelGenerateForm(_controller);
            form.ShowDialog();

        }

        private void basicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateProcessMapBasicForm form = new CreateProcessMapBasicForm(_controller);
            form.ShowDialog();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LogImportForm form = new LogImportForm();
            form.ShowDialog();
        }
    }
}
