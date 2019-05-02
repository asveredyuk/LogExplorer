using System;
using System.Linq;
using System.Windows.Forms;
using ClientApp.LogExplorer.Controller;
using JobSystem.Jobs;

namespace ClientApp.LogExplorer.ProcessMapEditor
{
    public partial class CreateProcessMapForm : Form
    {
        private LogExplorerController _controller;
        private ProcessMapJob Job;
        public CreateProcessMapForm(LogExplorerController controller)
        {
            InitializeComponent();
            _controller = controller;
        }

        private string[] profileList;
        private void CreateProcessMapForm_Load(object sender, EventArgs e)
        {
            profileList = _controller.State.Labels.Select(t => t.ProfileName).Distinct().ToArray();
            checkedListBoxProfiles.Items.AddRange(profileList.Cast<object>().ToArray());
        }

        private async void btMakeMap_Click(object sender, EventArgs e)
        {
            string[] selectedProfiles = checkedListBoxProfiles.SelectedItems.Cast<string>().ToArray();
            if (selectedProfiles.Length == 0)
            {
                MessageBox.Show("Select at least one profile");
                return;
            }

            string name = tbName.Text;
            var labels = _controller.State.Labels.Where(t => selectedProfiles.Contains(t.ProfileName)).ToArray();

            Job = new ProcessMapJob()
            {
                Labels = labels.Select(t => t._id).ToArray(),
                LogName = _controller.State.Info.Name,
                MapName = name,
                Id = Guid.NewGuid(),
                MapId = Guid.NewGuid()
            };

            await ApiBoundary.AddProcessMapJob(Job);

            tabControl.SelectTab(1);
            jobWaiter.SetJobs(new Guid[]{Job.Id});
            jobWaiter.Start();
            jobWaiter.onAllJobsCompleted += JobWaiterOnOnAllJobsCompleted;
            //timerCheckStatus.Start();
        }

        private void JobWaiterOnOnAllJobsCompleted()
        {
            btOpenMap.Enabled = true;
        }

        //private async void timerCheckStatus_Tick(object sender, EventArgs e)
        //{
        //    timerCheckStatus.Stop();

        //    var info = await ApiBoundary.GetJobInfo(Job.Id);
        //    labelStatus.Text = "Status: " + info.State;
        //    string[] statuswait = new[] {"new", "pending", "active"};
        //    if (statuswait.Contains(info.State))
        //    {
        //        timerCheckStatus.Start();
        //        return;
        //    }

        //    progressBar1.Style = ProgressBarStyle.Blocks;
        //    if (info.State == "completed")
        //    {
        //        btOpenMap.Enabled = true;
        //        return;
        //    }
        //}

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btOpenMap_Click(object sender, EventArgs e)
        {
            
            ProcessMapForm form = new ProcessMapForm(_controller, Job.MapId.ToString());
            form.Show();
            Close();
        }
    }
}
