using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Client.Model;

namespace Client.Service {
    public class GraphService {
        public Graph NewGraph(bool directed) {
            string uid = Guid.NewGuid().ToString();
            List<GraphPart> graphParts = new List<GraphPart>();
            Graph graph = new Graph(directed, graphParts, uid);
            return graph;
        }

        public Node AddNode(ref Graph graph, string label) {
            string uid = Guid.NewGuid().ToString();
            Node node = new Node(label, uid);
            GraphPart graphPart = NewGraphPart(node);
            graph.GraphPart.Add(graphPart);
            return node;
        }

        private GraphPart NewGraphPart(Node node) {
            string uid = Guid.NewGuid().ToString();
            List<Edge> edges = new List<Edge>();
            return new GraphPart(node, edges, uid);
        }

        public void AddEdge(Node source, Node destination, ref Graph graph, double weight = 1.0D) {
            if (IsInGraph(graph, source) && IsInGraph(graph, destination)) {
                if (IsEdgeExists(source, destination, graph)) RemoveEdge(source, destination, ref graph);
                Edge edge = new Edge(destination, weight, Guid.NewGuid().ToString());
                edge.Uid = Guid.NewGuid().ToString();
                graph.GraphPart.ToList().Find(part => part.Node.Uid == source.Uid).Edge.Add(edge);

                if (!graph.Directed) {
                    if (IsEdgeExists(destination, source, graph)) RemoveEdge(destination, source, ref graph);
                    Edge directedEdge = new Edge(source, weight, Guid.NewGuid().ToString());
                    directedEdge.Uid = Guid.NewGuid().ToString();
                    graph.GraphPart.ToList().Find(part => part.Node.Uid == destination.Uid).Edge.Add(directedEdge);
                }
            }
            else {
                throw new DataException("Node not in graph");
            }
        }

        public bool IsInGraph(Graph graph, Node node) {
            return graph.GraphPart.Any(graphPart => graphPart.Node.Uid == node.Uid);
        }

        public bool IsEdgeExists(Node source, Node dest, Graph graph) {
            GraphPart partGraph = graph.GraphPart.ToList().Find(part => part.Node.Uid == source.Uid);
            return partGraph.Edge.Any(edge => edge.Destination.Uid == dest.Uid);
        }

        public void RemoveEdge(Node source, Node destination, ref Graph graph) {
            foreach (GraphPart graphPart in graph.GraphPart)
                if (graphPart.Node.Uid == source.Uid)
                    foreach (Edge edge in graphPart.Edge.ToList())
                        if (edge.Destination.Uid == destination.Uid)
                            graphPart.Edge.Remove(edge);
        }
    }
}