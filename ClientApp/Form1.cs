using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JobSystem;

namespace ClientApp
{
    public partial class Form1 : Form
    {
        private JobInfo[] jobs = new JobInfo[0];
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //jobs = new List<JobInfo>();
            //jobs.Add(new JobInfo()
            //{
            //    Id = Guid.NewGuid(),
            //    State = "new"
            //});
            //jobs.Add(new JobInfo()
            //{
            //    Id = Guid.NewGuid(),
            //    State = "active"
            //});
            btJobsRefresh_Click(null,null);
            LoadFilesList();//dont await, go async
            //UpdateJobsDataGrid();
        }

        private async void LoadFilesList()
        {
            var list = await ApiBoundary.GetFiles();
            cbFiles.Items.Clear();
            cbFiles.Items.AddRange(list.Cast<object>().ToArray());
        }

        private void UpdateJobsDataGrid()
        {
            dgJobs.Rows.Clear();
            foreach (var jobInfo in jobs)
            {
                DataGridViewRow row = new DataGridViewRow();
                row.CreateCells(dgJobs);
                row.Cells[0].Value = jobInfo.Id.ToString();
                row.Cells[1].Value = jobInfo.State;
                if (jobInfo.Progress != null)
                {
                    string str = $"{jobInfo.Progress.CurrentStagePercentage}%\r\n Stage:{jobInfo.Progress.CurrentStage}";
                    row.Cells[2].Value = str;
                }

                if (jobInfo.Result != null)
                {
                    string str =
                        $"Exit code {jobInfo.Result.ReturnCode}, {jobInfo.Result.ElapsedMs}ms, msg:{jobInfo.Result.Message}";
                    row.Cells[3].Value = str;
                }
                dgJobs.Rows.Add(row);
            }
        }




        

        private void dgJobs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var senderGrid = (DataGridView)sender;

            if (senderGrid.Columns[e.ColumnIndex] is DataGridViewButtonColumn &&
                e.RowIndex >= 0)
            {

                DeleteJob(jobs[e.RowIndex]);
            }
        }

        private async void DeleteJob(JobInfo job)
        {
            //MessageBox.Show("Delete " + job.Id);
            await ApiBoundary.DeleteJob(job);
            btJobsRefresh_Click(null,null);
        }

        private async void btJobsRefresh_Click(object sender, EventArgs e)
        {
            jobs = await ApiBoundary.GetJobs();
            UpdateJobsDataGrid();
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
            var groupingField = cbGroupingField.SelectedItem.ToString();
            var groupingFieldType = cbGroupingFieldType.SelectedItem.ToString();
            var timeField = cbTimeField.SelectedItem.ToString();
            var timeFieldType = cbTimeFieldType.SelectedItem.ToString();
            var importArgs = new ImportArgs()
            {
                FileName = fname,
                GroupingField = groupingField,
                GroupingFieldType = groupingFieldType,
                TimeField = timeField,
                TimeFieldType = timeFieldType
            };
            await ApiBoundary.AddImportTask(importArgs);
            btJobsRefresh_Click(null,null);
        }
    }




}
