using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.LabelEditor;
using ClientApp.LogExplorer.Model;
using ClientApp.LogExplorer.RuleEditor;
using ClientApp.LogExplorer.View;
using LogEntity;
using Newtonsoft.Json;

namespace ClientApp.LogExplorer.Controller
{
    public class LogExplorerController
    {
        private const string STATE_FILE_PATH = "state.json";
        /// <summary>
        /// State, that can be changed only from this class, and RuleListForm (changes handled in this class)
        /// </summary>
        private State _state;
        public IState State => _state;

        private LogExplorerForm _form;

        private MainView mainView;

        private MainScrollView scr;

        public LogExplorerController()
        {
            LoadState();
        }

        public void BindForm(LogExplorerForm form)
        {
            _form = form;
            mainView = new MainView(this);
            mainView.Bind(_form.pbMain);
            scr = new MainScrollView(this);
            scr.Bind(_form.vScroll);
            
            //todo: init views
        }

        public void ChangePos(long pos, object sender, bool refreshSender = false)
        {
            if(_state.Pos == pos)
                return;
            _state.Pos = pos;
            if(mainView != sender || refreshSender)
                mainView.Refresh();
            if(scr != sender || refreshSender)
                scr.Refresh();
            //start loading?
            LoadLazyLogIfNeeded();
            //no need to serialize, non-persistant property changed
        }

        public void ChangeTracesInWindow(long num)
        {
            _state.TracesInView = num;
            mainView.Refresh();
            scr.Refresh();
            LoadLazyLogIfNeeded();
            //no need to serialize, non-persistant property changed
        }

        private async void LoadLazyLogIfNeeded()
        {
            if(State.Info == null)
                return;
            var posWas = State.Pos;
            await Task.Delay(50);
            if(posWas != State.Pos)
                return;

            if (!State.Log.CoversWindow(State.Pos, State.Pos + State.TracesInView))
            {
                var data = await ApiBoundary.GetLogAtPos(State.Info.Name, State.Pos, State.TracesInView, State.Labels.Select(t=>t._id).ToArray());
                State.Log.PushData(data);
                OnLazyLogLoaded();
            }
        }
        public void OnLazyLogLoaded()
        {
            //colorify if enabled
            if(_state.Log == null)
                return;
            ColorifyMachine.Colorify(_state.Log.Enumerate().ToList(), _state.Labels.ToList());
            mainView.Refresh();
        }
        public async void OpenNewLog(string name)
        {
            var info = await ApiBoundary.GetLogInfo(name);
            _state.Info = info;
            _state.Pos = 0;
            
            //_state.Labels = labels.ToList();
            LoadLabels();
            //refresh views
            //MessageBox.Show(JsonConvert.SerializeObject(info, Formatting.Indented));
            mainView.Refresh();
            LoadLazyLogIfNeeded();
        }

        private async Task LoadLabels()
        {
            var labels = await ApiBoundary.GetLabels(_state.Info.Name);
            _state.Labels = labels.ToList();
        }

        public void GoEditRules()
        {
            var f = new RuleListForm(_state, this);
            f.ShowDialog();
            //need to recolorify the elements
            OnLazyLogLoaded();
            //need to save state
            SaveState();
        }

        public void GoEditRulesNew()
        {
            var f = new LabelsListForm(_state, this);
            f.ShowDialog();
            OnLazyLogLoaded();

        }

        private void SaveState()
        {
           // File.WriteAllText(STATE_FILE_PATH, JsonConvert.SerializeObject(_state));
        }

        private void LoadState()
        {
            _state = Model.State.Default();
            //if(!File.Exists(STATE_FILE_PATH))
            //{
            //    _state = Model.State.Default();
            //    return;
            //}
            //var json = File.ReadAllText(STATE_FILE_PATH);
            //_state = JsonConvert.DeserializeObject<State>(json);
        }

        public async Task AddLabel(LogLabel label)
        {
            await ApiBoundary.AddLabel(label, _state.Info.Name);
            await LoadLabels();
        }

        public async Task DeleteLabel(LogLabel label)
        {
            await ApiBoundary.DeleteLabel(label, _state.Info.Name);
            await LoadLabels();
        }

        public async Task UpdateLabel(LogLabel label)
        {
            await ApiBoundary.UpdateLabel(label, _state.Info.Name);
            await LoadLabels();
        }

        
    }
}
