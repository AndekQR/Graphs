using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Service.helpers;
using Service.Models;
using Service.Services.Interfaces;

namespace Service.Services {
    public class GraphService : IGraphService {
        public Graph NewGraph(bool directed) {
            Graph graph = new Graph(directed) {Uid = Guid.NewGuid().ToString(), GraphPart = new List<GraphPart>()};
            return graph;
        }

        public Node AddNode(ref Graph graph, string label) {
            Node node = new Node(label) {Uid = Guid.NewGuid().ToString()};
            GraphPart graphPart = new GraphPart {Uid = Guid.NewGuid().ToString(), Edge = new List<Edge>(), Node = node};
            graph.GraphPart.Add(graphPart);
            return node;
        }

        public void AddEdge(Node source, Node destination, ref Graph graph, double weight = 1.0D) {
            if (IsInGraph(graph, source) && IsInGraph(graph, destination)) {
                Edge edgeToCheck = IsEdgeExists(source, destination, graph);
                if (edgeToCheck.IsNotNull()) {
                    ChangeEdgeData(ref edgeToCheck, NewEdge(destination, weight));
                }
                else {
                    Edge edge = NewEdge(destination, weight);
                    graph.GraphPart.ToList().Find(part => part.Node.Uid == source.Uid).Edge.Add(edge);
                }

                if (!graph.Directed) {
                    Edge edge = IsEdgeExists(destination, source, graph);
                    if (edge.IsNotNull()) {
                        ChangeEdgeData(ref edge, NewEdge(source, weight));
                    }
                    else {
                        Edge directedEdge = NewEdge(source, weight);
                        graph.GraphPart.ToList().Find(part => part.Node.Uid == destination.Uid).Edge.Add(directedEdge);
                    }
                }
            }
            else {
                throw new DataException("Node not in graph");
            }
        }

        public bool IsInGraph(Graph graph, Node node) {
            return graph.GraphPart.Any(graphPart => graphPart.Node.Uid == node.Uid);
        }

        public Edge IsEdgeExists(Node source, Node dest, Graph graph) {
            GraphPart partGraph = graph.GraphPart.ToList().Find(part => part.Node.Uid == source.Uid);
            Edge edge = partGraph.Edge.ToList().Find(x => x.Destination.Uid == dest.Uid);
            if (edge.IsNotNull()) return edge;
            return null;
        }

        //tylko lokalnie, nie tyka bazy danych
        public void RemoveEdge(Node source, Node destination, ref Graph graph) {
            foreach (GraphPart graphPart in graph.GraphPart)
                if (graphPart.Node.Uid == source.Uid)
                    foreach (Edge edge in graphPart.Edge.ToList())
                        if (edge.Destination.Uid == destination.Uid)
                            graphPart.Edge.Remove(edge);
        }

        public void DeleteGraph(int id) {
            using (MyDbContext db = new MyDbContext()) {
                Graph target = db.Graphs.FirstOrDefault(x => x.Id == id);
                if (target.IsNull()) throw new KeyNotFoundException("Graph id not found");

                IQueryable<GraphPart> graphParts = db.GraphParts
                    .Include(x=> x.Node)
                    .Where(x => x.Graph.Id == target.Id);

                foreach (GraphPart graphPart in graphParts.ToList()) {
                    IQueryable<Edge> edges = db.Edges
                        .Include(x => x.Destination)
                        .Where(x => x.GraphPart.Id == graphPart.Id);
                    foreach (Edge edge in edges.ToList()) {
                        //bo z innego graphPart mogła być strzałka do tego nodea i został już usunięty
                        if (edge.Destination.IsNotNull())
                            db.Nodes.Remove(edge.Destination);
                        db.Edges.Remove(edge);
                    }

                    //nie usunie się w pętli wyżej gdy nie ma krawędzi do tego nodea
                    if (graphPart.Node.IsNotNull())
                        db.Nodes.Remove(graphPart.Node);
                    db.GraphParts.Remove(graphPart);
                }

                db.Graphs.Remove(target);
                db.SaveChanges();
            }
        }


        public void SaveGraph(Graph graph) {
            using (MyDbContext db = new MyDbContext()) {
                db.Graphs.Add(graph);
                db.SaveChanges();
            }
        }


        public List<Graph> GetAllGraphs() {
            using (MyDbContext db = new MyDbContext()) {
                return db.Graphs
                    .Include(x => x.GraphPart)
                    .Include(x => x.GraphPart.Select(s => s.Edge))
                    .Include(x => x.GraphPart.Select(s => s.Edge.Select(s2 => s2.Destination)))
                    .Include(x => x.GraphPart.Select(s => s.Node))
                    .ToList();
            }
        }

        public Graph GetGraph(int id) {
            using (MyDbContext db = new MyDbContext()) {
                return db.Graphs
                    .Include(x => x.GraphPart)
                    .Include(x => x.GraphPart.Select(s => s.Edge))
                    .Include(x => x.GraphPart.Select(s => s.Edge.Select(s2 => s2.Destination)))
                    .Include(x => x.GraphPart.Select(s => s.Node))
                    .FirstOrDefault(x => x.Id == id);
            }
        }

        //TODO: do poprawy
        public void UpdateGraph(Graph graph) {
            DeleteGraph(graph.Id);
            SaveGraph(graph);
        }

        // public void UpdateGraph(Graph graph) {
        //     //TODO: do poprawy
        //     using (MyDbContext db = new MyDbContext()) {
        //         bool entity = db.Graphs.AsNoTracking().Any(x => x.Id == graph.Id);
        //         try {
        //             if (entity) {
        //                 var trackedGraphs = db.ChangeTracker.Entries<Graph>().ToList();
        //                 var trackedGraphsParts = db.ChangeTracker.Entries<Graph>().ToList();
        //                 var trackedEdges = db.ChangeTracker.Entries<Graph>().ToList();
        //                 var trackedNodes = db.ChangeTracker.Entries<Node>().ToList();
        //                 // db.Entry(graph).State = EntityState.Modified;
        //                 foreach (GraphPart graphPart in graph.GraphPart) {
        //                     var trackedGraphs1 = db.ChangeTracker.Entries<Graph>().ToList();
        //                     var trackedGraphsParts1 = db.ChangeTracker.Entries<GraphPart>().ToList();
        //                     var trackedEdges1 = db.ChangeTracker.Entries<Edge>().ToList();
        //                     var trackedNodes1 = db.ChangeTracker.Entries<Node>().ToList();
        //                     if (!db.Set<GraphPart>().Local.Any(x=> x.Id == graphPart.Id)) {
        //                         if (graphPart.Id == default) {
        //                             db.Entry(graphPart).State = EntityState.Added;
        //                         }
        //                         else {
        //                             db.Entry(graphPart).State = EntityState.Modified;
        //                         }
        //                     }
        //                     var trackedGraphs2 = db.ChangeTracker.Entries<Graph>().ToList();
        //                     var trackedGraphsParts2 = db.ChangeTracker.Entries<GraphPart>().ToList();
        //                     var trackedEdges2 = db.ChangeTracker.Entries<Edge>().ToList();
        //                     var trackedNodes2 = db.ChangeTracker.Entries<Node>().ToList();
        //                     // db.Entry(graphPart.Node).State = EntityState.Modified;
        //                     var trackedGraphs3 = db.ChangeTracker.Entries<Graph>().ToList();
        //                     var trackedGraphsParts3 = db.ChangeTracker.Entries<GraphPart>().ToList();
        //                     var trackedEdges3 = db.ChangeTracker.Entries<Edge>().ToList();
        //                     var trackedNodes3 = db.ChangeTracker.Entries<Node>().ToList();
        //                     foreach (Edge edge in graphPart.Edge) {
        //                         if (!db.Set<Edge>().Local.Any(x=> x.Id == edge.Id)) {
        //                             if (edge.Id == default) {
        //                                 db.Entry(edge).State = EntityState.Added;
        //                             }
        //                             else {
        //                                 db.Entry(edge).State = EntityState.Modified;
        //                             }
        //                         }
        //                         var trackedGraphs4 = db.ChangeTracker.Entries<Graph>().ToList();
        //                         var trackedGraphsParts4 = db.ChangeTracker.Entries<GraphPart>().ToList();
        //                         var trackedEdges4 = db.ChangeTracker.Entries<Edge>().ToList();
        //                         var trackedNodes4 = db.ChangeTracker.Entries<Node>().ToList();
        //                         // db.Entry(edge.Destination).State = EntityState.Modified;
        //                     }
        //                 }
        //                 
        //                 db.SaveChanges();
        //             }
        //             else {
        //                 throw new KeyNotFoundException("graph not in DB");
        //             }
        //         }
        //         catch (Exception e) {
        //             Console.WriteLine(e);
        //             throw;
        //         }
        //     }
        // }

        private Edge NewEdge(Node destination, double weight) {
            Edge edge = new Edge(destination, weight);
            edge.Uid = Guid.NewGuid().ToString();
            return edge;
        }

        // zmieniamy dane edge a nie tworzymy nowej bo ID musi zostać to samo żeby ją zaktualizować w DB
        private void ChangeEdgeData(ref Edge oldEdge, Edge newEdge) {
            oldEdge.Destination = newEdge.Destination;
            oldEdge.Uid = newEdge.Uid;
            oldEdge.Weight = newEdge.Weight;
        }
    }
}