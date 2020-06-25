using System;
using System.Collections.Generic;
using Service.Models;

namespace Service.Services.Interfaces {

    public interface IGraphService {

        Graph NewGraph(bool directed);

        Node AddNode(ref Graph graph, string label);

        void AddEdge(Node source, Node destination, ref Graph graph, double weight = 1.0D);

        bool IsInGraph(Graph graph, Node node);

        Edge IsEdgeExists(Node source, Node dest, Graph graph);

        void RemoveEdge(Node source, Node destination, ref Graph graph);

        void DeleteGraph(int id);

        void SaveGraph(Graph graph);

        List<Graph> GetAllGraphs();

        Graph GetGraph(int id);

        void UpdateGraph(Graph graph);

        void fixReferences(ref Graph graph);

        Node findNodeByLabel(Graph graph, String label);

        Node getMinNodeCoarseGrained(Graph graph);

        Node GetMinNodeDijkstra(Graph graph);

        double GetWeightBetweenNodesDijkstra(Graph graph, Node start, Node end);
    }
}