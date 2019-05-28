using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        }


        private void UpdateJobsDataGrid()
        {
            var arr = new string[] {"completed", "pending", "active", "new"};
            jobs = jobs.OrderBy(t => 100 - Array.IndexOf(arr, t.State)).ToArray();
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
                var job = jobs[e.RowIndex];
                if (job.State == "active")
                {
                    MessageBox.Show("Cannot delete active job");
                    return;
                }
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

        private async void timerRefresh_Tick(object sender, EventArgs e)
        {
            jobs = await ApiBoundary.GetJobs();
            
            UpdateJobsDataGrid();
        }

        private void cbAutoRefresh_CheckedChanged(object sender, EventArgs e)
        {
            if (cbAutoRefresh.Checked)
            {
                timerRefresh.Start();
            }
            else
            {
                timerRefresh.Stop();
            }
        }
    }




}
