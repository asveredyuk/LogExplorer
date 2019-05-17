using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.View;

namespace ClientApp.LogExplorer
{
    public partial class LogImportForm : Form
    {
        public LogImportForm()
        {
            InitializeComponent();
        }

        private async void LogImportDialog_Load(object sender, EventArgs e)
        {
            var list = await ApiBoundary.GetFiles();
            cbFiles.Items.Clear();
            cbFiles.Items.AddRange(list.Cast<object>().ToArray());
        }

        private async void cbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            var fields = await ApiBoundary.GetHeaders(cbFiles.SelectedItem.ToString());
            cbGroupingField.SelectedIndex = -1;
            cbGroupingField.Items.Clear();
            cbGroupingField.Items.AddRange(fields);
            cbTimeField.Items.AddRange(fields);
        }

        private async void btImport_Click(object sender, EventArgs e)
        {
            var fname = cbFiles.SelectedItem.ToString();
            var logName = tbLogName.Text;
            if(string.IsNullOrWhiteSpace(logName))
            {
                MessageBox.Show("Enter name of the log");
                return;
            }
            var groupingField = cbGroupingField.SelectedItem?.ToString();
            if (groupingField == null)
            {
                
                MessageBox.Show("Select grouping field");
                return;
            }
            var groupingFieldType = cbGroupingFieldType.SelectedItem?.ToString();
            if (groupingFieldType == null)
            {

                MessageBox.Show("Select grouping field type");
                return;
            }
            var timeField = cbTimeField.SelectedItem?.ToString();
            if (timeField == null)
            {

                MessageBox.Show("Select time field");
                return;
            }
            var timeFieldType = cbTimeFieldType.SelectedItem?.ToString();
            if (timeFieldType == null)
            {

                MessageBox.Show("Select time field type");
                return;
            }

            if (tbDelimiter.Text.Length < 1)
            {
                MessageBox.Show("Enter delimiter character");
                return;
            }

            var delimiter = tbDelimiter.Text[0];

            var guid = Guid.NewGuid();
            var importArgs = new ImportArgs()
            {
                JobID = guid,
                LogName = logName,
                FileName = fname,
                GroupingField = groupingField,
                GroupingFieldType = groupingFieldType,
                TimeField = timeField,
                TimeFieldType = timeFieldType,
                CsvDelimiter = delimiter
            };
            await ApiBoundary.AddImportTask(importArgs);
            JobWaiterForm form = new JobWaiterForm(guid);
            form.ShowDialog();
            Close();
            //btJobsRefresh_Click(null, null);
        }
    }
}
