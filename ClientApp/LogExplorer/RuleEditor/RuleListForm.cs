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
using ClientApp.LogExplorer.Model;
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp.LogExplorer.RuleEditor
{
    public partial class RuleListForm : Form
    {
        private State _state;
        private LogExplorerController _controller;
        public RuleListForm(State state, LogExplorerController controller)
        {
            _state = state;
            _controller = controller;
            InitializeComponent();
            
            //checkedListBoxRules.Items.AddRange(_state.Rules.Select(t=>t.Name).Cast<object>().ToArray());
            FillTheList();

            //fullfill the list of rules

        }

        private void FillTheList()
        {
            checkedListBoxRules.Items.Clear();
            foreach (var r in _state.Labels)
            {
                checkedListBoxRules.Items.Add(r.Name, false);
            }
        }

        private async void btDelete_Click(object sender, EventArgs e)
        {
            var name = checkedListBoxRules.SelectedItem?.ToString();
            if (name == null)
            {
                MessageBox.Show("Select item first");
                return;
            }

            //_state.Labels.Remove(_state.Labels.First(t=>t.Name == name));
            //_state.Rules.Remove(_state.Rules.FirstOrDefault(t => t.Name == name));
            //checkedListBoxRules.Items.Remove(checkedListBoxRules.SelectedItem);
            var label = _state.Labels.First(t => t.Name == name);
            await _controller.DeleteLabel(label);
            FillTheList();

        }

        private async void btEdit_Click(object sender, EventArgs e)
        {
            var name = checkedListBoxRules.SelectedItem?.ToString();
            if (name == null)
            {
                MessageBox.Show("Select item first!");
                return;
            }

            var item = _state.Labels.First(t=>t.Name == name);//_state.Rules.FirstOrDefault(t => t.Name == name);
            //check?
            var f = new RuleEditorForm(item);
            if (f.ShowDialog() == DialogResult.OK)
            {
                var label = f.Result;
                await _controller.UpdateLabel(label);
                FillTheList();
                //var newName = f.Result.Name;
                //if (newName == name)
                //{
                //    //name not changed
                //    //only replace
                //    //_state.Rules[name] = f.Result;
                //    _state.Labels.Remove(item);
                //    _state.Labels.Add(f.Result);
                //    return;
                //}
                //if (_state.Rules.ContainsKey(newName))
                //{
                //    MessageBox.Show("Item with such name already exist, info is lost");
                //}
                //else
                //{
                //    _state.Rules.Remove(name);
                //    _state.Rules[newName] = f.Result;
                //    var index = checkedListBoxRules.Items.IndexOf(name);
                //    checkedListBoxRules.Items.Remove(name);
                //    checkedListBoxRules.Items.Insert(index, newName);
                //}
            }
        }

        private async void btAdd_Click(object sender, EventArgs e)
        {
            var f = new RuleEditorForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                //check if there exist such item
                //if (_state.Rules.ContainsKey(f.Result.Name))
                //{
                //    MessageBox.Show("Item with such name already exist, info is lost");
                //}
                //else
                //{
                    await _controller.AddLabel(f.Result);
                    FillTheList();
                    //_state.Labels.Add(f.Result);
                    //checkedListBoxRules.Items.Add(f.Result.Name, true);
                //}
            }

        }

        private void RuleListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //var names = checkedListBoxRules.CheckedItems.Cast<string>().ToList();
            //foreach (var kv in _state.Rules)
            //{
            //    kv.Value.Enabled = names.Contains(kv.Key);
            //}
        }

        private async void btClone_Click(object sender, EventArgs e)
        {
            var name = checkedListBoxRules.SelectedItem?.ToString();
            if (name == null)
            {
                MessageBox.Show("Select item first!");
                return;
            }

            var item = _state.Labels.First(t => t.Name == name);//_state.Rules.FirstOrDefault(t => t.Name == name);
            var clonedItem = JsonConvert.DeserializeObject<LogLabel>(JsonConvert.SerializeObject(item));
            clonedItem._id = Guid.NewGuid().ToString();
            clonedItem.Name += "_cloned";
            await _controller.AddLabel(clonedItem);
            FillTheList();
        }
    }
}
