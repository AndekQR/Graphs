using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Service.Models;

namespace Service.Services {
    public class GraphService {
        private readonly MyDbContext db;

        public GraphService() {
            db = new MyDbContext();
        }

        public Graph NewGraph(bool directed) {
            Graph graph = new Graph(directed);
            graph.Uid = Guid.NewGuid().ToString();
            graph.GraphPart = new List<GraphPart>();
            return graph;
        }

        public Node AddNode(ref Graph graph, string label) {
            Node node = new Node(label);
            node.Uid = Guid.NewGuid().ToString();
            GraphPart graphPart = new GraphPart();
            graphPart.Uid = Guid.NewGuid().ToString();
            graphPart.Edge = new List<Edge>();
            graphPart.Node = node;
            graph.GraphPart.Add(graphPart);
            return node;
        }

        public void AddEdge(Node source, Node destination, ref Graph graph, double weight = 1.0D) {
            if (IsInGraph(graph, source) && IsInGraph(graph, destination)) {
                if (IsEdgeExists(source, destination, graph)) RemoveEdge(source, destination, ref graph);
                Edge edge = new Edge(destination, weight);
                edge.Uid = Guid.NewGuid().ToString();
                graph.GraphPart.ToList().Find(part => part.Node.Uid == source.Uid).Edge.Add(edge);

                if (!graph.Directed) {
                    if (IsEdgeExists(destination, source, graph)) RemoveEdge(destination, source, ref graph);
                    Edge directedEdge = new Edge(source, weight);
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
            if (partGraph.Edge.ToList().Any(edge => edge.Destination.Uid == dest.Uid)) return true;
            return false;
        }

        public void RemoveEdge(Node source, Node destination, ref Graph graph) { //trzeba usuwac edge z bazy
            foreach (GraphPart graphPart in graph.GraphPart)
                if (graphPart.Node.Uid == source.Uid)
                    foreach (Edge edge in graphPart.Edge.ToList())
                        if (edge.Destination.Uid == destination.Uid)
                            graphPart.Edge.Remove(edge);
        }

        public void DeleteGraph(int id) {
            Graph target = db.Graphs.Include(x => x.GraphPart)
                .FirstOrDefault(x => x.Id == id);
            if (target == null) throw new KeyNotFoundException("Graph id not found");
            
            IQueryable<GraphPart> graphParts = db.GraphParts.Include(x => x.Edge)
                .Where(x => x.Graph.Id == target.Id);

            foreach (GraphPart graphPart in graphParts.ToList()) {
                IQueryable<Edge> edges = db.Edges.Include(x => x.Destination)
                    .Where(x => x.GraphPart.Id == graphPart.Id);
                foreach (Edge edge in edges.ToList()) {
                    //bo z innego graphPart mogła być strzałka do tego nodea i został już usunięty
                    if (edge.Destination != null) 
                        db.Nodes.Remove(edge.Destination);
                    db.Edges.Remove(edge);
                }
                //nie usunie się w pętli wyżej gdy nie ma krawędzi do tego nodea
                if (graphPart.Node != null) 
                    db.Nodes.Remove(graphPart.Node);
                db.GraphParts.Remove(graphPart);
            }

            db.Graphs.Remove(target);
            db.SaveChanges();
        }


        public void SaveGraph(Graph graph) {
            db.Graphs.Add(graph);
            db.SaveChanges();
        }
    }
}