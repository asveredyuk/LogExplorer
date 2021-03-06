﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientApp.LogExplorer.View
{
    public partial class JobWaiterForm : Form
    {
        private bool completed;
        public JobWaiterForm(params Guid[] jobIds)
        {
            InitializeComponent();
            jobWaiter.SetJobs(jobIds);
            jobWaiter.onAllJobsCompleted += JobWaiterOnOnAllJobsCompleted;
        }

        private void JobWaiterOnOnAllJobsCompleted()
        {
            completed = true;
            if(checkBoxAutoClose.Checked)
                Close();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void JobWaiterForm_Load(object sender, EventArgs e)
        {
            jobWaiter.Start();
        }

        private void JobWaiterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = completed ? DialogResult.OK : DialogResult.Ignore;
        }
    }
}
