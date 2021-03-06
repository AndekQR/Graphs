﻿using Client.helpers;
using Client.Model;
using Client.Model.Visualization;
using Client.Presenter;
using GraphX.Controls;
using GraphX.Controls.Models;
using GraphX.PCL.Common.Enums;
using GraphX.PCL.Logic.Algorithms.OverlapRemoval;
using GraphX.PCL.Logic.Models;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace Client.View {

    public partial class MainForm : Form, IMainView {
        private ZoomControl _zoomControl;
        private GraphAreaVisualizer _graphAreaVisualizer;

        public MainForm() {
            InitializeComponent();
            Load += GraphLoad;

            new Thread(new ThreadStart(() => {
                initListView();
            })).Start();
        }

        private async void initListView() {
            graphsListView.Columns.Add("Graphs", graphsListView.Width);

            graphsListView.BeginUpdate();

            List<Graph> graphs = await new MainPresenter(this).getAllGraphsAsync();
            foreach (Graph graph in graphs) {
                ListViewItem item = new ListViewItem(graph.ToString());
                item.Tag = graph;

                if (graphsListView.InvokeRequired) {
                    graphsListView.Invoke(new Action(() => graphsListView.Items.Add(item)));
                } else {
                    graphsListView.Items.Add(item);
                }
            }

            graphsListView.EndUpdate();
        }

        private void GraphLoad(object sender, EventArgs e) {
            wpfHost.Child = initEmptyGraphVisualization();
            _graphAreaVisualizer.GenerateGraph(true);
            _graphAreaVisualizer.SetVerticesDrag(true, true);
        }

        private UIElement initEmptyGraphVisualization() {
            _zoomControl = new ZoomControl();
            ZoomControl.SetViewFinderVisibility(_zoomControl, Visibility.Visible);
            var logic = new GXLogicCore<NodeV, EdgeV, BidirectionalGraph<NodeV, EdgeV>>();
            _graphAreaVisualizer = new GraphAreaVisualizer {
                LogicCore = logic,
                EdgeLabelFactory = new DefaultEdgelabelFactory()
            };
            logic.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.LinLog;
            logic.DefaultLayoutAlgorithmParams = logic.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.LinLog);
            //((LinLogLayoutParameters)logic.DefaultLayoutAlgorithmParams). = 100;
            logic.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            logic.DefaultOverlapRemovalAlgorithmParams = logic.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)logic.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)logic.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;
            logic.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.None;
            logic.AsyncAlgorithmCompute = false;
            _graphAreaVisualizer.ShowAllEdgesLabels(true);
            _zoomControl.Content = _graphAreaVisualizer;

            var myResourceDictionary = new ResourceDictionary { Source = new Uri("Templates\\template.xaml", UriKind.Relative) };
            _zoomControl.Resources.MergedDictionaries.Add(myResourceDictionary);

            return _zoomControl;
        }

        public string LogTextBox {
            get => logTextBox.Text;
            set {
                if (logTextBox.InvokeRequired) {
                    logTextBox.Invoke(new Action(() => LogTextBox = value));
                } else {
                    logTextBox.Text = value;
                }
            }
        }

        public ListView graphsListViewProperty {
            get => graphsListView;
        }

        private void graphsListView_Click(object sender, EventArgs e) {
            Graph selectedGraph = (Graph)graphsListView.SelectedItems[0].Tag;

            BackgroundWorker background = new BackgroundWorker();
            background.DoWork += UpdateGraphVisualization;
            background.RunWorkerAsync(selectedGraph.Id);
        }

        private async void UpdateGraphVisualization(object sender, DoWorkEventArgs e) {
            //pobieranie id z listy
            MainPresenter mainPresenter = new MainPresenter(this);
            Graph graphToShow = await mainPresenter.GetGraphAsync((int)e.Argument);
            if (graphToShow.IsNull()) throw new ArgumentException();
            BeginInvoke(new Action(() => {
                _graphAreaVisualizer.ClearLayout();

                _graphAreaVisualizer.GenerateGraph(mainPresenter.convertToDirectedVisualization(graphToShow));
            }));
        }

        private void AddGraphButton_Click(object sender, EventArgs e) {
            NewGraphForm newGraphForm = new NewGraphForm() {
                Owner = this
            };
            newGraphForm.ShowDialog();
        }

        private void DeleteGraphButton_Click(object sender, EventArgs e) {
            MainPresenter mainPresenter = new MainPresenter(this);
            int idToDelete = ((Graph)graphsListView.SelectedItems[0].Tag).Id;
            mainPresenter.deleteGraph(idToDelete);
        }

        private void bfs_fine_button_Click(object sender, EventArgs e) {
            LogTextBox = "Computing...";
            new Thread(async () => {
                var watch = Stopwatch.StartNew();
                MainPresenter mainPresenter = new MainPresenter(this);
                if (graphsListView.InvokeRequired) {
                    graphsListView.Invoke(new Action(async () => {
                        await this.BfsFine(watch, mainPresenter);
                    }));
                } else {
                    await this.BfsFine(watch, mainPresenter);
                }
            }).Start();
        }

        private async Task BfsFine(Stopwatch watch, MainPresenter mainPresenter) {
            if (graphsListView.SelectedItems.Count == 0) return;
            Graph graph = (Graph)graphsListView.SelectedItems[0].Tag;
            Node result = await mainPresenter.getMinNodeFineGradientAsync(graph);
            watch.Stop();
            LogTextBox = "Using BFS fine-grained calculations " + Environment.NewLine;
            if (result == null) LogTextBox += "null";
            else LogTextBox += result.Label;

            LogTextBox += Environment.NewLine + "Time: " + watch.ElapsedMilliseconds + " ms";
        }

        private void bfs_coarse_button_Click(object sender, EventArgs e) {
            LogTextBox = "Computing...";
            new Thread(async () => {
                var watch = Stopwatch.StartNew();
                MainPresenter mainPresenter = new MainPresenter(this);
                if (graphsListView.InvokeRequired) {
                    graphsListView.Invoke(new Action(async () => {
                        await this.BfsCoarse(watch, mainPresenter);
                    }));
                } else {
                    await this.BfsCoarse(watch, mainPresenter);
                }
            }).Start();
        }

        private async Task BfsCoarse(Stopwatch watch, MainPresenter mainPresenter) {
            if (graphsListView.SelectedItems.Count == 0) return;
            Graph graph = (Graph)graphsListView.SelectedItems[0].Tag;
            Node result = await mainPresenter.getMinNodeCoarseGrainedAsync(graph);
            watch.Stop();
            LogTextBox = "Using BFS coarse-grained calculations " + Environment.NewLine;
            if (result == null) LogTextBox += "null";
            else LogTextBox += result.Label;

            LogTextBox += Environment.NewLine + "Time: " + watch.ElapsedMilliseconds + " ms";
        }

        private void dijkstra_fineGradient_Click(object sender, EventArgs e) {
            LogTextBox = "Computing...";
            new Thread(async () => {
                var watch = Stopwatch.StartNew();
                MainPresenter mainPresenter = new MainPresenter(this);
                if (graphsListView.InvokeRequired) {
                    graphsListView.Invoke(new Action(async () => {
                        await this.DijkstraFine(watch, mainPresenter);
                    }));
                } else {
                    await this.DijkstraFine(watch, mainPresenter);
                }
            }).Start();
        }

        private async Task DijkstraFine(Stopwatch watch, MainPresenter mainPresenter) {
            if (graphsListView.SelectedItems.Count == 0) return;
            Graph graph = (Graph)graphsListView.SelectedItems[0].Tag;
            Node result = await mainPresenter.getMinNodeUsingDijkstraFineGradient(graph);
            watch.Stop();
            LogTextBox = "Using Dijkstra fine-grained calculations " + Environment.NewLine;
            if (result == null) LogTextBox += "null";
            else LogTextBox += result.Label;

            LogTextBox += Environment.NewLine + "Time: " + watch.ElapsedMilliseconds + " ms";
        }

        private void dijkstra_coarseGradient_Click(object sender, EventArgs e) {
            LogTextBox = "Computing...";
            new Thread(async () => {
                var watch = Stopwatch.StartNew();
                MainPresenter mainPresenter = new MainPresenter(this);
                if (graphsListView.InvokeRequired) {
                    graphsListView.Invoke(new Action(async () => {
                        await this.DijkstraCoarseAsync(watch, mainPresenter);
                    }));
                } else {
                    await this.DijkstraCoarseAsync(watch, mainPresenter);
                }
            }).Start();
        }

        private async Task DijkstraCoarseAsync(Stopwatch watch, MainPresenter mainPresenter) {
            if (graphsListView.SelectedItems.Count == 0) return;
            Graph graph = (Graph)graphsListView.SelectedItems[0].Tag;
            Node result = await mainPresenter.getMinNodeUsingDijkstraCoarseGradientAsync(graph);
            watch.Stop();
            LogTextBox = "Using Dijkstra coarse-grained calculations " + Environment.NewLine;
            if (result == null) LogTextBox += "null";
            else LogTextBox += result.Label;

            LogTextBox += Environment.NewLine + "Time: " + watch.ElapsedMilliseconds + " ms";
        }
    }
}