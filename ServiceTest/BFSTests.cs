using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Service.Models;
using Service.Services;
using Service.Services.Interfaces;
using static Service.helpers.BFSMethods;

namespace ServiceTest
{
    [TestFixture]
    class BFSTests
    {
        private IGraphService _graphService;
        private FakeDb<Graph> _fakeDb;

        [SetUp]
        public void Init()
        {
            _fakeDb = new FakeDb<Graph>();
            var myDbContextMock = new Mock<MyDbContext>();
            _graphService = new GraphService();
        }

        [Test]
        public void TestBFSToAll()
        {
            var graph = _graphService.NewGraph(true);
            var nodeA = _graphService.AddNode(ref graph, "nodeA");
            var nodeB = _graphService.AddNode(ref graph, "nodeB");
            var nodeC = _graphService.AddNode(ref graph, "nodeC");
            _graphService.AddEdge(nodeA, nodeB, ref graph, 10.0D);
            _graphService.AddEdge(nodeB, nodeA, ref graph, 12.0D);
            _graphService.AddEdge(nodeC, nodeA, ref graph, 5.0D);

            Dictionary<string, HashSet<string>> adjList = GraphAsAdjacencyList(graph);
            foreach (KeyValuePair<string, HashSet<string>> kpv in adjList)
            {
                Console.WriteLine(kpv.Key + ": ");
                foreach (var s in kpv.Value)
                {
                    Console.Write(s + ", ");
                }
                Console.WriteLine();
            }
            var result = ShortestPathToAll(graph, nodeA);
            Console.WriteLine("Od {0}", nodeA.Label);
            foreach (KeyValuePair<string, double> kpv in result)
            {
                Console.WriteLine("do {0} - długość {1}", kpv.Key, kpv.Value);
            }
        }

        [Test]
        public void TestBFSToGiven()
        {
            var graph = _graphService.NewGraph(true);
            var nodeA = _graphService.AddNode(ref graph, "nodeA");
            var nodeB = _graphService.AddNode(ref graph, "nodeB");
            var nodeC = _graphService.AddNode(ref graph, "nodeC");
            _graphService.AddEdge(nodeA, nodeB, ref graph, 10.0D);
            _graphService.AddEdge(nodeB, nodeA, ref graph, 12.0D);
            _graphService.AddEdge(nodeC, nodeA, ref graph, 5.0D);

            Assert.AreEqual(ShortestPathToGiven(graph, nodeA, nodeA), 0);
            Assert.AreEqual(ShortestPathToGiven(graph, nodeA, nodeB), 1);
            Assert.AreEqual(ShortestPathToGiven(graph, nodeA, nodeC), -1);
            Assert.AreEqual(ShortestPathToGiven(graph, nodeC, nodeA), 1);
        }
    }
}
