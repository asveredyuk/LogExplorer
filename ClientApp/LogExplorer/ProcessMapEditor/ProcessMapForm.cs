using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ClientApp.LogExplorer.Controller;
using LogEntity;
using Microsoft.Msagl.Drawing;
using Microsoft.Msagl.GraphViewerGdi;

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
            foreach (var relation in _map.Relations)
            {
                var a = labels[relation.labelFrom];
                var b = labels[relation.labelTo];
                graph.AddEdge(a.Text, b.Text);
            }

            viewer.Graph = graph;
        }
    }
}
