using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Model;

namespace ClientApp.LogExplorer.RuleEditor
{
    public partial class RuleListForm : Form
    {
        private State _state;
        public RuleListForm(State state)
        {
            _state = state;
            InitializeComponent();
            
            //checkedListBoxRules.Items.AddRange(_state.Rules.Select(t=>t.Name).Cast<object>().ToArray());
            foreach (var kv in _state.Rules)
            {
                var r = kv.Value;
                checkedListBoxRules.Items.Add(r.Name, r.Enabled);
            }

            //fullfill the list of rules

        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            var name = checkedListBoxRules.SelectedItem?.ToString();
            if (name == null)
            {
                MessageBox.Show("Select item first");
                return;
            }

            _state.Rules.Remove(name);
            //_state.Rules.Remove(_state.Rules.FirstOrDefault(t => t.Name == name));
            checkedListBoxRules.Items.Remove(checkedListBoxRules.SelectedItem);
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            var name = checkedListBoxRules.SelectedItem?.ToString();
            if (name == null)
            {
                MessageBox.Show("Select item first!");
                return;
            }

            var item = _state.Rules[name];//_state.Rules.FirstOrDefault(t => t.Name == name);
            //check?
            var f = new RuleEditorForm(item);
            if (f.ShowDialog() == DialogResult.OK)
            {
                var newName = f.Result.Name;
                if (newName == name)
                {
                    //name not changed
                    //only replace
                    _state.Rules[name] = f.Result;
                }
                if (_state.Rules.ContainsKey(newName))
                {
                    MessageBox.Show("Item with such name already exist, info is lost");
                }
                else
                {
                    _state.Rules.Remove(name);
                    _state.Rules[newName] = f.Result;
                    var index = checkedListBoxRules.Items.IndexOf(name);
                    checkedListBoxRules.Items.Remove(name);
                    checkedListBoxRules.Items.Insert(index, newName);
                }
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            var f = new RuleEditorForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                //check if there exist such item
                if (_state.Rules.ContainsKey(f.Result.Name))
                {
                    MessageBox.Show("Item with such name already exist, info is lost");
                }
                else
                {
                    _state.Rules[f.Result.Name] = f.Result;
                    checkedListBoxRules.Items.Add(f.Result.Name, true);
                }
            }

        }

        private void RuleListForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            var names = checkedListBoxRules.CheckedItems.Cast<string>().ToList();
            foreach (var kv in _state.Rules)
            {
                kv.Value.Enabled = names.Contains(kv.Key);
            }
        }
    }
}
