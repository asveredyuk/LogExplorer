using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ClientApp.LogExplorer.Controller;
using LogEntity;
using Microsoft.Msagl.Core.Layout;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;
using Microsoft.Msagl.Miscellaneous;
using Newtonsoft.Json;
using Edge = Microsoft.Msagl.Drawing.Edge;
using Node = Microsoft.Msagl.Drawing.Node;

namespace ClientApp.LogExplorer.ProcessMapEditor
{
    public partial class ProcessMapForm : Form
    {
        enum TruncateMode
        {
            byValue,
            byNumberOfConnections
        }

        private TruncateMode _truncateMode = TruncateMode.byValue;
        private GViewer viewer;
        private LogExplorerController _controller;
        private string _mapId;
        private ProcessMap _map;
        public ProcessMapForm(LogExplorerController controller, string id)
        {
            InitializeComponent();
            _controller = controller;
            _mapId = id;
            viewer = new GViewer();
            viewer.Dock = DockStyle.Fill;
            panelForMainView.Controls.Add(viewer);
            viewer.MouseClick += ViewerOnMouseClick;
        }

        private void ViewerOnMouseClick(object sender, MouseEventArgs mouseEventArgs)
        {
            var node = viewer.SelectedObject as Node;
            if (node != null)
            {
                SetAsSelectedObject(node.UserData);
                return;
            }

            var edge = viewer.SelectedObject as Edge;
            if (edge != null)
            {
                SetAsSelectedObject(edge.UserData);
                return;
            }
            SetAsSelectedObject(null);
            SetAsSelectedObject(null);
            
        }

        private async void ProcessMapForm_Load(object sender, EventArgs e)
        {
            _map = await ApiBoundary.GetMap(_controller.State.Info.Name, _mapId);
            FillGraph();
        }

        private void FillGraph()
        {
            Dictionary<string, LogLabel> labels = new Dictionary<string, LogLabel>();
            foreach (var mapLabel in _map.Labels)
            {
                labels[mapLabel._id] = mapLabel;
            }

            var graph = new Graph();
            double total = _map.Relations.OrderByDescending(t => t.count).Take(_map.Relations.Length/10).Average(t=>t.count);
            double maxWidth = 3;
            double minWidth = 0.25;
            int maxA = 255;
            int minA = 100;

            foreach (var relation in GetFilteredRelations())
            {
                //var a = labels[relation.labelFrom];
                //var b = labels[relation.labelTo];
                var edge = graph.AddEdge(relation.labelFrom, relation.labelTo);
                double width = minWidth + (maxWidth-minWidth) * (relation.count * 1.0 / total);
                if (width > maxWidth)
                    width = maxWidth;
                edge.Attr.LineWidth = width;
                if (checkBoxShowValues.Checked)
                {
                    edge.LabelText = relation.count.ToString();
                }

                edge.UserData = relation;
                //edge.Attr.Weight = (int)relation.count;
                //edge.Weight = (int) relation.count;
                var alpha = minA + (maxA - minA) * (relation.count / total);
                if (alpha > maxA)
                    alpha = maxA;
                edge.Attr.Color = new Color(Convert.ToByte(alpha),0,0,0);
            }

            foreach (var graphNode in graph.Nodes)
            {
                var id = graphNode.LabelText;
                var label = labels[id];
                graphNode.LabelText = label.Text;
                graphNode.UserData = label;
            }

            
            viewer.Graph = graph;
        }

        void SetAsSelectedObject(object obj)
        {
            if (obj == null)
            {
                dgvSelectedObject.DataSource = null;
                return;
            }
            var json = JsonConvert.SerializeObject(obj);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            var list = new List<KeyValuePair<string,string>>(dict);
            dgvSelectedObject.DataSource = list;
            dgvSelectedObject.Columns[0].Width = 100;
            dgvSelectedObject.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        IEnumerable<ProcessMapRelation> GetFilteredRelations()
        {
            if (_truncateMode == TruncateMode.byNumberOfConnections)
            {
                return _map.Relations.OrderByDescending(t => t.count).Take((100-trackBarTruncate.Value)*_map.Relations.Length/100);
            }
            //byValue
            long max = _map.Relations.Max(t => t.count);
            long min = max * trackBarTruncate.Value / 100;


            return _map.Relations.Where(t => t.count > min);
        }

        private bool ignoreTruncateEvent = false;
        private void trackBarTruncate_Scroll(object sender, EventArgs e)
        {
            ignoreTruncateEvent = true;
            nudTruncate.Value = trackBarTruncate.Value;
            ignoreTruncateEvent = false;
            if(checkBoxLiveRefresh.Checked)
                FillGraph();
        }

        private void nudTruncate_ValueChanged(object sender, EventArgs e)
        {
            ignoreTruncateEvent = true;
            trackBarTruncate.Value = (int)nudTruncate.Value;
            ignoreTruncateEvent = false;
            if (checkBoxLiveRefresh.Checked)
                FillGraph();

        }

        private void btTruncate_Click(object sender, EventArgs e)
        {
            FillGraph();
        }

        private void radioButtonTruncate_CheckedChanged(object sender, EventArgs e)
        {
            var newTr = radioButtonTruncateByValue.Checked ? TruncateMode.byValue : TruncateMode.byNumberOfConnections;
            if (newTr != _truncateMode)
            {
                _truncateMode = newTr;
                if (checkBoxLiveRefresh.Checked)
                {
                    FillGraph();
                }
            }
        }

        private void checkBoxShowValues_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBoxLiveRefresh.Checked)
                FillGraph();
        }
    }
}
