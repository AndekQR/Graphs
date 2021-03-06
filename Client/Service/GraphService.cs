﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Bogus;
using Client.helpers;
using Client.Model;
using Client.Model.Visualization;

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

        public void AddEdge(Node source, Node destination, ref Graph graph, double weight = 1.0D) {
            if (IsInGraph(graph, source) && IsInGraph(graph, destination)) {
                Edge edgeToCheck = IsEdgeExists(source, destination, graph);
                if (edgeToCheck.IsNotNull()) {
                    ChangeEdgeData(ref edgeToCheck, NewEdge(destination, weight));
                } else {
                    Edge edge = NewEdge(destination, weight);
                    graph.GraphPart.ToList().Find(part => part.Node.Uid == source.Uid).Edge.Add(edge);
                }

                if (!graph.Directed) {
                    Edge edge = IsEdgeExists(destination, source, graph);
                    if (edge.IsNotNull()) {
                        ChangeEdgeData(ref edge, NewEdge(source, weight));
                    } else {
                        Edge directedEdge = NewEdge(source, weight);
                        graph.GraphPart.ToList().Find(part => part.Node.Uid == destination.Uid).Edge.Add(directedEdge);
                    }
                }
            } else {
                throw new DataException("Node not in graph");
            }
        }

        private Edge NewEdge(Node destination, double weight) {
            string uid = Guid.NewGuid().ToString();
            Edge edge = new Edge(destination, weight, uid);
            return edge;
        }

        // zmieniamy dane edge a nie tworzymy nowej bo ID musi zostać to samo żeby ją zaktualizować w DB
        private void ChangeEdgeData(ref Edge oldEdge, Edge newEdge) {
            oldEdge.Destination = newEdge.Destination;
            oldEdge.Uid = newEdge.Uid;
            oldEdge.Weight = newEdge.Weight;
        }

        private GraphPart NewGraphPart(Node node) {
            string uid = Guid.NewGuid().ToString();
            List<Edge> edges = new List<Edge>();
            return new GraphPart(node, edges, uid);
        }

        public bool IsInGraph(Graph graph, Node node) {
            return graph.GraphPart.Any(graphPart => graphPart.Node.Uid == node.Uid);
        }

        public Edge IsEdgeExists(Node source, Node dest, Graph graph) {
            GraphPart partGraph = graph.GraphPart.ToList().Find(part => part.Node.Uid == source.Uid);
            Edge edge = partGraph.Edge.ToList().Find(x => x.Destination.Uid == dest.Uid);
            if (edge.IsNotNull()) {
                return edge;
            }
            return null;
        }

        public void RemoveEdge(Node source, Node destination, ref Graph graph) {
            foreach (GraphPart graphPart in graph.GraphPart)
                if (graphPart.Node.Uid == source.Uid)
                    foreach (Edge edge in graphPart.Edge.ToList())
                        if (edge.Destination.Uid == destination.Uid) {
                            graphPart.Edge.Remove(edge);
                        }
        }

        public Node FindNode(Graph graph, int id) {
            return graph.GraphPart.FirstOrDefault(x => x.Node.Id == id)?.Node;
        }

        public Node FindDestinationNode(Graph graph, int id) {
            foreach (GraphPart graphPart in graph.GraphPart) {
                foreach (Edge edge in graphPart.Edge) {
                    if (edge.Destination.Id == id) {
                        return edge.Destination;
                    }
                }
            }
            return null;
        }

        public Graph GetRandomGraph(int vertices, bool directed, bool weights) {
            var faker = new Faker("en");
            Graph graph = NewGraph(directed);

            List<Node> nodes = new List<Node>();
            for (int i = 0; i < vertices; i++) {
                var node = this.AddNode(ref graph, faker.Name.FirstName());
                nodes.Add(node);
            }

            foreach (Node node in nodes) {
                if (nodes.Count > 1) {
                    for (int j = 0; j < faker.Random.Int(1, 3); j++) {
                        Node dest = nodes[faker.Random.Int(0, nodes.Count - 1)];
                        Edge edge = IsEdgeExists(node, dest, graph);
                        if (edge == null) {
                            if (weights) {
                                AddEdge(node, dest, ref graph, Math.Round(faker.Random.Double(1, vertices * 2), 2));
                            } else {
                                AddEdge(node, dest, ref graph, 1.0);
                            }
                        }
                    }
                }
            }
            return graph;
        }
    }
}