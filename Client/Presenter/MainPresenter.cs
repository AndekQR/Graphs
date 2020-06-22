using Client.API;
using Client.helpers;
using Client.Model;
using Client.Model.Visualization;
using Client.Service;
using Client.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Presenter {

    public class MainPresenter {
        private readonly ApiService _apiService;
        private readonly GraphService _graphService;

        private readonly IMainView _mainView;

        public MainPresenter(IMainView mainView) {
            _mainView = mainView;
            _graphService = new GraphService();
            _apiService = new ApiService();
        }

        public MainPresenter() {
            _graphService = new GraphService();
            _apiService = new ApiService();
        }

        public DirectedGraphVisualizer convertToDirectedVisualization(Graph graph) {
            DirectedGraphVisualizer dataGraph = new DirectedGraphVisualizer();

            foreach (GraphPart part in graph.GraphPart) {
                NodeV nodeV = new NodeV() {
                    Label = part.Node.Label,
                    Uid = part.Node.Uid,
                    ID = part.Node.Id
                };
                dataGraph.AddVertex(nodeV);
            }
            var nodes = dataGraph.Vertices.ToList();

            foreach (GraphPart part in graph.GraphPart) {
                foreach (Edge edge in part.Edge) {
                    EdgeV edgeV = new EdgeV() {
                        Source = findNode(nodes, part.Node.Id),
                        Target = findNode(nodes, edge.Destination.Id),
                        ID = edge.Id,
                        Uid = edge.Uid,
                        Weight = edge.Weight,
                    };

                    dataGraph.AddEdge(edgeV);
                }
            }
            return dataGraph;
        }

        private NodeV findNode(List<NodeV> nodes, long id) {
            var node = nodes.Find(x => x.ID == id);
            return node;
        }

        public async Task<Graph> GetGraphAsync(int graphId) {
            Graph graph = await _apiService.GetGraph(graphId);
            return graph;
        }

        public async Task<List<Graph>> getAllGraphsAsync() {
            List<Graph> graphs = await _apiService.getAllGraphs();
            if (graphs.IsNotNull()) {
                return graphs;
            } else {
                return new List<Graph>();
            }
        }

        public async Task<Graph> CreateRandomoGraphAsync(int vertices, Boolean directed) {
            Graph graph = _graphService.GetRandomGraph(vertices, directed);
            Graph savedGraph = await _apiService.SaveGraph(graph);
            return savedGraph;
        }

        public void deleteGraph(int id) {
            new Thread(async () => {
                bool result = await _apiService.DeleteGraph(id);
                if (result == true) {
                    if (_mainView.graphsListViewProperty.InvokeRequired) {
                        _mainView.graphsListViewProperty.Invoke(new Action(() => {
                            for (int i = 0; i < _mainView.graphsListViewProperty.Items.Count; i++) {
                                if (((Graph)_mainView.graphsListViewProperty.Items[i].Tag).Id == id) {
                                    _mainView.graphsListViewProperty.Items[i].Remove();
                                    break;
                                }
                            }
                        }));
                    } else {
                        for (int i = 0; i < _mainView.graphsListViewProperty.Items.Count; i++) {
                            if (((Graph)_mainView.graphsListViewProperty.Items[i].Tag).Id == id) {
                                _mainView.graphsListViewProperty.Items[i].Remove();
                                break;
                            }
                        }
                    }
                }
            }).Start();
        }
    }
}