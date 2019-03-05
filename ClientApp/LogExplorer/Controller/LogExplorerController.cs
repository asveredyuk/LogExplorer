using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientApp.LogExplorer.Model;
using ClientApp.LogExplorer.RuleEditor;
using ClientApp.LogExplorer.View;
using Newtonsoft.Json;

namespace ClientApp.LogExplorer.Controller
{
    class LogExplorerController
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
                var data = await ApiBoundary.GetLogAtPos(State.Info.Name, State.Pos, State.TracesInView);
                State.Log.PushData(data);
                OnLazyLogLoaded();
            }
        }
        public void OnLazyLogLoaded()
        {
            //colorify if enabled
            if(_state.Log == null)
                return;
            ColorifyMachine.Colorify(_state.Log.Enumerate(), _state.Rules.Select(t=>t.Value).Where(t=>t.Enabled).ToList());
            mainView.Refresh();
        }
        public async void OpenNewLog(string name)
        {
            var info = await ApiBoundary.GetLogInfo(name);
            _state.Info = info;
            _state.Pos = 0;
            //refresh views
            //MessageBox.Show(JsonConvert.SerializeObject(info, Formatting.Indented));
            mainView.Refresh();
            LoadLazyLogIfNeeded();
        }

        public void GoEditRules()
        {
            var f = new RuleListForm(_state);
            f.ShowDialog();
            //need to colorify the elements
            OnLazyLogLoaded();
            //need to save state
            SaveState();
        }

        private void SaveState()
        {
            File.WriteAllText(STATE_FILE_PATH, JsonConvert.SerializeObject(_state));
        }

        private void LoadState()
        {
            if(!File.Exists(STATE_FILE_PATH))
            {
                _state = Model.State.Default();
                return;
            }
            var json = File.ReadAllText(STATE_FILE_PATH);
            _state = JsonConvert.DeserializeObject<State>(json);
        }
    }
}
