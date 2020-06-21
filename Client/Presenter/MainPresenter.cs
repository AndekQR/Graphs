using Client.API;
using Client.helpers;
using Client.Model;
using Client.Service;
using Client.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        public DirectedGraphVisualizer convertToDirectedVisualization(Graph graph) {
            DirectedGraphVisualizer dataGraph = new DirectedGraphVisualizer();
            _mainView.LogTextBox = "convertToDirectedVisualization";

            foreach (GraphPart part in graph.GraphPart) {
                dataGraph.AddVertex(part.Node);
            }
            var nodes = dataGraph.Vertices.ToList();

            foreach (GraphPart part in graph.GraphPart) {
                foreach (Edge edge in part.Edge) {
                    edge.Source = findNode(nodes, part.Node.ID);
                    edge.Target = findNode(nodes, edge.Target.ID);

                    try {
                        bool result = dataGraph.AddEdge(edge);
                    } catch (Exception e) {
                        _mainView.LogTextBox = e.Message;
                    }
                }
            }
            return dataGraph;
        }

        private Node findNode(List<Node> nodes, long id) {
            return nodes.Find(x => x.ID == id);
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
    }
}