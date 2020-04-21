using System;
using System.Diagnostics;
using Service.Models;
using Service.Services;
using Service.Services.Interfaces;

namespace ServiceTest {
    public static class Utilities {
        public static Graph ExampleGraph() {
            IGraphService graphService = new GraphService();
            Graph graph = graphService.NewGraph(false);

            Node nodeA = graphService.AddNode(ref graph, "nodeA");
            Node nodeB = graphService.AddNode(ref graph, "nodeB");
            Node nodeC = graphService.AddNode(ref graph, "nodeC");

            graphService.AddEdge(nodeA, nodeB, ref graph);
            graphService.AddEdge(nodeA, nodeC, ref graph);
            graphService.AddEdge(nodeB, nodeA, ref graph, 10.0D);
            graphService.AddEdge(nodeC, nodeA, ref graph, 7.0D);

            return graph;
        }
    }
}