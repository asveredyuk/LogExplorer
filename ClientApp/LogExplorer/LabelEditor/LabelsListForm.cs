﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Controller;
using ClientApp.LogExplorer.Model;
using ClientApp.LogExplorer.View;
using ClientApp.LogExplorer.View.RecycleListView;
using ClientApp.LogExplorer.View.WinformsComponents;
using JobSystem.Jobs;
using LogEntity;

namespace ClientApp.LogExplorer.LabelEditor
{
    public partial class LabelsListForm : Form
    {
        private const string PROFILES_ALL_NAME = "<all>";
        private const string CREATE_NEW_PROFILE = "<add new>";
        private RecycleListView<LogLabel> recycleListView;
        private RecycleListViewAdapter<LogLabel> adapter;

        private State _state;
        private LogExplorerController _controller;

        private List<string> _profiles;
        private string _selectedProfile = PROFILES_ALL_NAME;

        private string _justCreatedProfile;
        public LabelsListForm()
        {
            InitializeComponent();
        }

        public LabelsListForm(State state, LogExplorerController controller)
        {
            InitializeComponent();
            _state = state;
            _controller = controller;

        }

        private void LabelsList_Load(object sender, EventArgs e)
        {
            recycleListView = new RecycleListView<LogLabel>();
            recycleListView.Dock = DockStyle.Fill;
            panelForListView.Controls.Add(recycleListView);

            adapter = new RecycleListViewAdapter<LogLabel>(
                () => new LabelListItem(_controller),
                () => adapter.UpdateData(_state.Labels.Where(ProfileFilter)));
            recycleListView.Adapter = adapter;

            adapter.OnDataChanged += AdapterOnOnDataChanged;
            adapter.UpdateData(_state.Labels);

        }

        private void RefreshDataInAdapter()
        {
            adapter.UpdateData(_state.Labels.Where(ProfileFilter));
        }

        private bool ProfileFilter(LogLabel lb)
        {
            if (_selectedProfile == PROFILES_ALL_NAME)
                return true;

            return lb.ProfileName == _selectedProfile;

        }
        List<string> ParseProfiles()
        {
            var lst = _state.Labels.Select(t => t.ProfileName).Distinct().ToList();
            if (_justCreatedProfile != null)
            {
                lst.Add(_justCreatedProfile);
                _justCreatedProfile = null;
            }
            return lst;
        }

        void UpdateProfileComboBox()
        {
            var lst = new List<string>();
            lst.Add(PROFILES_ALL_NAME);
            lst.AddRange(_profiles);
            lst.Add(CREATE_NEW_PROFILE);
            int pos = lst.IndexOf(_selectedProfile);
            if (pos == -1)
                pos = 0;
            cbProfile.Items.Clear();
            cbProfile.Items.AddRange(lst.Cast<object>().ToArray());
            cbProfile.SelectedIndex = pos;

        }

        void SelectProfile(string profile)
        {
            var pos = cbProfile.Items.IndexOf(profile);
            cbProfile.SelectedIndex = pos >= 0 ? pos : 0;
        }
        private void AdapterOnOnDataChanged()
        {
            _profiles = ParseProfiles();
            UpdateProfileComboBox();
        }

        private async void addButton_Click(object sender, EventArgs e)
        {
            if (_selectedProfile == PROFILES_ALL_NAME)
            {
                MessageBox.Show("Select profile first");
                return;
            }
            var (res, form) = LabelEditorForm.GoCreate(_selectedProfile);
            if (res == DialogResult.OK)
            {
                await _controller.AddLabel(form.Result);
                RefreshDataInAdapter();
                //adapter.UpdateData(_state.Labels);
            }
        }

        private void cbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProfile.SelectedItem.ToString() == _selectedProfile)
                return;//nothing changed
            if (cbProfile.SelectedItem.ToString() == CREATE_NEW_PROFILE)
            {
                var form = new TextFieldDialog("Create new profile", "Profile name");
                var res = form.ShowDialog();
                if (res == DialogResult.OK)
                {
                    string newProfile = form.Result;
                    if (string.IsNullOrEmpty(newProfile))
                    {
                        MessageBox.Show("Given name is null or empty");
                        SelectProfile(_selectedProfile);
                        return;
                    }
                    if (_profiles.Contains(newProfile))
                    {
                        MessageBox.Show("Such profile already exists");
                        SelectProfile(_selectedProfile);
                        return;
                    }
                    _selectedProfile = newProfile;
                    _justCreatedProfile = newProfile;
                    RefreshDataInAdapter();

                }
                else
                {
                    SelectProfile(_selectedProfile);
                }
                return;
            }

            _selectedProfile = cbProfile.SelectedItem.ToString();
            RefreshDataInAdapter();
        }

        private async void btDeleteAll_Click(object sender, EventArgs e)
        {
            var labelsToDelete = _state.Labels.Where(ProfileFilter).ToList();
            var res = MessageBox.Show($"Delete {labelsToDelete.Count} labels in {_selectedProfile} profile?", "Confirm", MessageBoxButtons.YesNo);
            if (res == DialogResult.Yes)
            {
                foreach (var label in labelsToDelete)
                {
                    await _controller.DeleteLabel(label);
                }
                AdapterOnOnDataChanged();
            }

        }

        private async void btCacheAll_Click(object sender, EventArgs e)
        {
            var labelsToCache = _state.Labels.Where(ProfileFilter).Where(t=>!t.HasCache).ToList();
            if (labelsToCache.Count == 0)
            {
                MessageBox.Show("Nothing to cache");
                return;
            }
            List<Guid> guids = new List<Guid>();
            foreach (var label in labelsToCache)
            {
                var job = new CacheLabelJob()
                {
                    LabelId = label._id,
                    LogName = _controller.State.Info.Name
                };
                guids.Add(job.Id);
                await ApiBoundary.AddCacheLabelJob(job);
            }

            //MessageBox.Show($"Added {labelsToCache.Count} cache jobs, wait and do not click this button again");
            JobWaiterForm form = new JobWaiterForm(guids.ToArray());
            form.jobWaiter.onAllJobsCompleted += async delegate
            {
                await _controller.LoadLabels();
                RefreshDataInAdapter();

            };
            form.Show();

        }
    }
}
