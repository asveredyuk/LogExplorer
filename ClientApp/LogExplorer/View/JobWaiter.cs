﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using JobSystem;

namespace ClientApp.LogExplorer.View
{
    public partial class JobWaiter : UserControl
    {
        public event Action onAllJobsCompleted;
        private Guid[] _jobIds;
        public IReadOnlyList<JobInfo> JobInfos => _jobInfos;
        private List<JobInfo> _jobInfos = new List<JobInfo>();
        public JobWaiter()
        {
            InitializeComponent();
        }

        public void SetJobs(params Guid[] jobIds)
        {
            _jobIds = jobIds;
        }

        public void Start()
        {
            if (_jobIds.Length == 1)
            {
                progressBar1.Style = ProgressBarStyle.Marquee;
            }
            Check();
            checkTimer.Start();
        }

        private async void checkTimer_Tick(object sender, EventArgs e)
        {
            checkTimer.Stop();
            _jobInfos.Clear();
            foreach (var jobId in _jobIds)
            {
                var info = await ApiBoundary.GetJobInfo(jobId);
                _jobInfos.Add(info);
            }

            int finishedCount = _jobInfos.Count(t => !IsJobPendingOrActive(t));

            lbStatus.Text = MakeLabel();
            if (_jobInfos.Count > 1)
            {
                progressBar1.Value = finishedCount * 100 / _jobIds.Length;
            }
            else
            {
                var info = JobInfos[0];
                if (info.Progress == null)
                {
                    progressBar1.Style = ProgressBarStyle.Marquee;
                }
                else
                {
                    progressBar1.Style = ProgressBarStyle.Blocks;
                    if (info.Progress.TotalStagesCount > 0)
                    {
                        progressBar1.Value = info.Progress.CurrentStagePercentage;
                    }
                    else
                    {
                        progressBar1.Value = info.Progress.OverallProgress;
                    }
                }
            }

            if (finishedCount == _jobIds.Length)
            {
                //all jobs done
                onAllJobsCompleted?.Invoke();
                progressBar1.Style = ProgressBarStyle.Blocks;
            }
            else
            {
                checkTimer.Start();
            }

        }

        private string MakeLabel()
        {
            if (_jobInfos.Count == 1)
            {
                var info = _jobInfos[0];
                string r = "Status: ";
                r += info.State;
                if (info.State == "active" && info.Progress != null)
                {
                    if (info.Progress.TotalStagesCount > 0)
                    {
                        r +=
                            $" stage {info.Progress.CurrentStageNomber} of {info.Progress.TotalStagesCount}: {info.Progress.CurrentStage}";
                    }
                }

                return r;
            }
            string[] statuses = new[] { "new", "pending", "active", "completed", "failed", "cancelled", "invalid" };
            string res = "Status: ";
            var rr = statuses.Select(t => (t, _jobInfos.Count(q => q.State == t))).Where(t => t.Item2 > 0).Select(t=>t.Item2.ToString() + " " + t.Item1);
            res += string.Join(", ", rr);
            return res;

        }
        private bool IsJobPendingOrActive(JobInfo info)
        {
            string[] statuswait = new[] { "new", "pending", "active" };
            return statuswait.Contains(info.State);
        }

        private void Check()
        {

        }
    }
}
