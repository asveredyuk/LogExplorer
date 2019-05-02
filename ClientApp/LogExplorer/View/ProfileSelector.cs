using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Controller;

namespace ClientApp.LogExplorer.View
{
    class ProfileSelector : ViewBase
    {
        private List<string> profiles = new List<string>();
        private ComboBox _cb;
        private bool _skipChanged;

        public ProfileSelector(LogExplorerController controller) : base(controller)
        {
            
        }

        public override void Refresh()
        {
            var newProfiles = new List<string>();
            newProfiles.Add("--none--");
            newProfiles.AddRange(Controller.State.LabelProfiles);
            if (!profiles.SequenceEqual(newProfiles))
            {
                //try to find index for active profile
                profiles = newProfiles;

                var pos = newProfiles.IndexOf(Controller.State.ActiveLabelProfile);

                _skipChanged = true;
                _cb.Items.Clear();
                _cb.Items.AddRange(profiles.Cast<object>().ToArray());
                if (pos >= 0)
                {
                    _cb.SelectedIndex = pos;
                    _skipChanged = false;
                }
                else
                {
                    _skipChanged = false;
                    _cb.SelectedIndex = 0;
                }
            }
            
        }

        public void Bind(ComboBox cb)
        {
            _cb = cb;
            cb.SelectedIndexChanged += CbOnSelectedIndexChanged;
        }

        private void CbOnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if(_skipChanged)
                return;
            Controller.SetActiveProfile(_cb.SelectedItem.ToString());
        }
    }
}
