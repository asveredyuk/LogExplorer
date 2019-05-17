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
using ClientApp.LogExplorer.LabelEditor;
using ClientApp.LogExplorer.View;
using JobSystem;
using JobSystem.Jobs;
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp.LogExplorer.ProcessMapEditor
{
    public partial class CreateProcessMapBasicForm : Form
    {
        private LogExplorerController _controller;
        private string[] fields;

        private string selectedField;
        private ProcessMapJob Job;

        public CreateProcessMapBasicForm(LogExplorerController controller)
        {
            _controller = controller;
            InitializeComponent();
        }

        private async void CreateProcessMapBasicForm_Load(object sender, EventArgs e)
        {
            fields = await ApiBoundary.GetFieldNames(_controller.State.Info.Name);
            comboBoxLabelField.Items.AddRange(fields.Cast<object>().ToArray());
        }

        private async void comboBoxLabelField_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedName = comboBoxLabelField.SelectedItem.ToString();
            selectedField = selectedName;
            var distinctValues = await ApiBoundary.GetDistinctFieldValues(_controller.State.Info.Name, selectedName);
            if (distinctValues == null)
            {
                MessageBox.Show("Too much values of given field, select another field");
                buttonCreate.Enabled = false;
                checkedListBoxValues.Items.Clear();
                return;
            }
            checkedListBoxValues.Items.Clear();
            checkedListBoxValues.Items.AddRange(distinctValues.Cast<object>().ToArray());
            for (int i = 0; i < checkedListBoxValues.Items.Count; i++)
            {
                checkedListBoxValues.SetItemChecked(i,true);
            }
            buttonCreate.Enabled = true;
        }

        private async void buttonCreate_Click(object sender, EventArgs e)
        {
            var selected = new List<string>(checkedListBoxValues.CheckedItems.Cast<string>());
            if (selected.Count < 2)
            {
                MessageBox.Show("Select at least two values, each value will represent one node in process map");
                return;
            }

            var mapName = tbMapName.Text;
            if (string.IsNullOrEmpty(mapName))
            {
                MessageBox.Show("Enter map name");
                return;
            }

            var profileName = $"_map_{mapName}_autogenerated";
            var fieldName = selectedField;
            var labels = selected.Select(t => MakeLabel(fieldName, t, profileName)).ToList();
            foreach (var logLabel in labels)
            {
                await _controller.AddLabel(logLabel);
            }
            Job = new ProcessMapJob()
            {
                Labels = labels.Select(t => t._id).ToArray(),
                LogName = _controller.State.Info.Name,
                MapName = mapName,
                Id = Guid.NewGuid(),
                MapId = Guid.NewGuid()
            };

            await ApiBoundary.AddProcessMapJob(Job);
            JobWaiterForm form = new JobWaiterForm(Job.Id);
            var res = form.ShowDialog();
            if (res == DialogResult.OK)
            {
                btOpenMap.Enabled = true;
            }
            else
            {
                Close();
            }

        }
        private LogLabel MakeLabel(string field, string value, string profileName)
        {
            var pred = MakeCheck(field, value);
            var js = "function(o){\r\n\treturn " + pred + ";\r\n};";
            return new LogLabel()
            {
                _id = Guid.NewGuid().ToString(),
                Color = "White",
                JSFilter = js,
                Name = value,
                ProfileName = profileName,
                Text = value
            };
        }

        private string MakeCheck(string fieldName, string value)
        {
            return $"o[\"{fieldName}\"] == \"{value}\"";
        }

        private void btOpenMap_Click(object sender, EventArgs e)
        {
            ProcessMapForm form = new ProcessMapForm(_controller, Job.MapId.ToString());
            form.Show();
            Close();
        }
    }
}
