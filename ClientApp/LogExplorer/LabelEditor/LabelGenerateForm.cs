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
using LogEntity;

namespace ClientApp.LogExplorer.LabelEditor
{
    public partial class LabelGenerateForm : Form
    {
        private readonly LogExplorerController _controller;
        private const string NONE = "-^-";
        private string field;


        class GroupingValue
        {
            public string Value { get; set; }
            public bool Include { get; set; } = true;
            public int GroupIndex { get; set; }
            public string Name { get; set; }
            public string Text { get; set; }
        }
        private List<GroupingValue> GroupingValues = new List<GroupingValue>();

        public LabelGenerateForm(LogExplorerController controller)
        {
            _controller = controller;

            InitializeComponent();
        }

        private async void LabelGenerateForm_Load(object sender, EventArgs e)
        {
            EnableProgress(true);
            var fields = await ApiBoundary.GetFieldNames(_controller.State.Info.Name);
            comboBoxFieldName.Items.AddRange(fields);
            comboBoxFieldName.Enabled = true;
            EnableProgress(false);
        }

        private void EnableProgress(bool enabled)
        {
            progressBar.Style = enabled ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
        }

        private async void comboBoxFieldName_SelectedIndexChanged(object sender, EventArgs e)
        {
            dgv.DataSource = null;
            EnableProgress(true);

            var selectedName = comboBoxFieldName.SelectedItem.ToString();
            field = selectedName;
            var distinctValues = await ApiBoundary.GetDistinctFieldValues(_controller.State.Info.Name, selectedName);
            if (distinctValues == null)
            {
                MessageBox.Show("Too much values of given field, cannot enumerate");
                EnableProgress(false);
                return;
            }
            Array.Sort(distinctValues);
            GroupingValues = new List<GroupingValue>();
            for (int i = 0; i < distinctValues.Length; i++)
            {
                var dv = distinctValues[i];
                var gv = new GroupingValue()
                {
                    Value = dv,
                    Name = $"{selectedName} = {dv}",
                    Text = dv,
                    GroupIndex = i
                };
                GroupingValues.Add(gv);
            }

            dgv.DataSource = GroupingValues;
            tbProfileName.Text = "Autolabeled " + selectedName;
            EnableProgress(false);

        }

        private void btGroupSelected_Click(object sender, EventArgs e)
        {
            if (dgv.SelectedRows.Count < 2)
            {
                MessageBox.Show("Select at least 2 rows");
                return;
            }
            List<DataGridViewRow> rows = new List<DataGridViewRow>();
            for (int i = 0; i < dgv.SelectedRows.Count; i++)
            {
                rows.Add(dgv.SelectedRows[i]);
            }

            var firstObj = rows.OrderBy(t=>t.Index).First().DataBoundItem as GroupingValue;
            for (int i = 0; i < dgv.SelectedRows.Count; i++)
            {
                var otherObj = dgv.SelectedRows[i].DataBoundItem as GroupingValue;
                if(otherObj == firstObj)
                    continue;

                otherObj.GroupIndex = firstObj.GroupIndex;
                otherObj.Text = NONE;
                otherObj.Name = NONE;
            }

            dgv.Refresh();
        }

        private async void btGenerate_Click(object sender, EventArgs e)
        {
            EnableProgress(true);
            var profileName = tbProfileName.Text;
            if (string.IsNullOrWhiteSpace(profileName))
            {
                MessageBox.Show("Wrong profile name");
                return;
            }
            var grouped = GroupingValues.Where(t=>t.Include).GroupBy(t => t.GroupIndex);
            foreach (var gr in grouped)
            {
                var label = MakeLabel(gr.ToArray(), profileName);
                await _controller.AddLabel(label);
            }

            MessageBox.Show("OK");
            EnableProgress(false);
            Close();
        }

        private LogLabel MakeLabel(GroupingValue[] vals, string profileName)
        {
            var checks = vals.Select(t => MakeCheck(field, t.Value)).ToArray();
            var pred = string.Join("||\r\n\t", checks);
            var js = "function(o){\r\n\treturn " + pred + ";\r\n};";
            return new LogLabel()
            {
                _id = Guid.NewGuid().ToString(),
                Color = "White",
                JSFilter = js,
                Name = vals[0].Name,
                ProfileName = profileName,
                Text = vals[0].Text
            };
        }

        private string MakeCheck(string fieldName, string value)
        {
            return $"o[\"{fieldName}\"] == \"{value}\"";
        }
    }
}
