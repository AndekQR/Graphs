using Service.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Service.Services {

    public class DijkstraAlgorithm {
        private const double NO_PATH = 99999.99999;

        private Graph graph;
        private Node startNode;

        private List<GraphPart> toProcess;
        private List<GraphPart> processed;
        private List<double> d;
        private List<GraphPart> p;

        public DijkstraAlgorithm(Graph graph) {
            this.graph = graph;
        }

        public void start(Node start) {
            startNode = start;
            this.toProcess = new List<GraphPart>(this.graph.GraphPart);
            processed = new List<GraphPart>();
            d = Enumerable.Repeat(NO_PATH, this.graph.GraphPart.Count).ToList();
            GraphPart graphPartOfStartNode = graph.GraphPart.Find(x => x.Node.Label == startNode.Label);
            if (graphPartOfStartNode != null) {
                d[graph.GraphPart.IndexOf(graphPartOfStartNode)] = 0.0;
            } else throw new ArgumentException("Start node doesn'y exists in graph");
            p = Enumerable.Repeat<GraphPart>(null, graph.GraphPart.Count).ToList();

            for (int i = 0; i < graph.GraphPart.Count; i++) {
                if (i == 0) {
                    GraphPart graphPartByNode = toProcess.Find(x => x.Node.Label == startNode.Label);
                    if (graphPartByNode != default) {
                        this.moveToProcessedList(graphPartByNode);
                        this.processAllDestinations(graphPartByNode);
                    }
                } else {
                    if (toProcess.Count > 0) {
                        GraphPart minimalDestination = getMinimalDest(processed[processed.Count - 1]);
                        if (minimalDestination != null) {
                            moveToProcessedList(minimalDestination);
                            processAllDestinations(minimalDestination);
                        } else {
                            break;
                        }
                    }
                }
            }
        }

        private void processAllDestinations(GraphPart part) {
            foreach (Edge edge in part.Edge) {
                GraphPart edgeDestinationPart = toProcess.Find(x => x.Node.Label == edge.Destination.Label);
                if (edgeDestinationPart == null) continue;
                if (d[graph.GraphPart.IndexOf(edgeDestinationPart)] > (d[graph.GraphPart.IndexOf(part)] + edge.Weight)) {
                    d[graph.GraphPart.IndexOf(edgeDestinationPart)] = d[graph.GraphPart.IndexOf(part)] + edge.Weight;
                    p[graph.GraphPart.IndexOf(edgeDestinationPart)] = part;
                }
            }
        }

        private void moveToProcessedList(GraphPart graphPart) {
            toProcess.Remove(graphPart);
            processed.Add(graphPart);
        }

        private GraphPart getMinimalDest(GraphPart part) {
            if (part.Edge.Count == 0) return null;

            Edge min = part.Edge[0];
            foreach (Edge edge in part.Edge) {
                if (edge.Weight < min.Weight) {
                    min = edge;
                }
            }
            return toProcess.Find(x => x.Node.Label == min.Destination.Label);
        }

        public double getRoadWeightToNode(Node end) {
            GraphPart partOfEndNode = this.graph.GraphPart.Find(x => x.Node.Label == end.Label);
            if (partOfEndNode == default) return default;

            int indexOfEndGraphPart = graph.GraphPart.IndexOf(partOfEndNode);
            if (indexOfEndGraphPart == -1) throw new ArgumentException("Node not found");
            double aDouble = this.d[indexOfEndGraphPart];
            if (aDouble == NO_PATH) {
                return default;
            } else {
                return aDouble;
            }
        }

        public Node getMinNode() {
            Dictionary<GraphPart, double> nodes = new Dictionary<GraphPart, double>();
            DijkstraAlgorithm dijkstra = new DijkstraAlgorithm(graph);

            foreach (GraphPart part in graph.GraphPart) {
                dijkstra.start(part.Node);
                double tmp = 0;
                foreach (GraphPart dest in graph.GraphPart) {
                    tmp += dijkstra.getRoadWeightToNode(dest.Node);
                }
                nodes.Add(part, tmp);
            }
            return getMinGraphPartFromList(nodes).Node;
        }

        private GraphPart getMinGraphPartFromList(Dictionary<GraphPart, double> dict) {
            GraphPart min = null;
            double value = double.MaxValue;

            foreach (KeyValuePair<GraphPart, double> pair in dict) {
                if (pair.Value < value) {
                    min = pair.Key;
                    value = pair.Value;
                }
            }
            return min;
        }

        public List<Node> getNodesInTheRouteTo(Node end) {
            List<Node> result = new List<Node>();
            GraphPart part = graph.GraphPart.Find(x => x.Node.Label == end.Label);
            do {
                result.Insert(0, part.Node);
                int indexOfNextNode = graph.GraphPart.IndexOf(part);
                part = this.p[indexOfNextNode];
            } while (part != null);
            return result;
        }
    }
}