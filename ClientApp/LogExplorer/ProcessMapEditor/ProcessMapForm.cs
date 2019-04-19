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
using Edge = Microsoft.Msagl.Drawing.Edge;

namespace ClientApp.LogExplorer.ProcessMapEditor
{
    public partial class ProcessMapForm : Form
    {
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
                var a = labels[relation.labelFrom];
                var b = labels[relation.labelTo];
                var edge = graph.AddEdge(a.Text, b.Text);
                double width = minWidth + (maxWidth-minWidth) * (relation.count * 1.0 / total);
                if (width > maxWidth)
                    width = maxWidth;
                edge.Attr.LineWidth = width;
                //edge.Attr.Weight = (int)relation.count;
                //edge.Weight = (int) relation.count;
                var alpha = minA + (maxA - minA) * (relation.count / total);
                if (alpha > maxA)
                    alpha = maxA;
                edge.Attr.Color = new Color(Convert.ToByte(alpha),0,0,0);
            }

            
            viewer.Graph = graph;
        }

        IEnumerable<ProcessMapRelation> GetFilteredRelations()
        {
            long total = _map.Relations.Sum(t => t.count);
            long min = 0;
            return _map.Relations.Where(t => t.count > min);
        }

    }
}
