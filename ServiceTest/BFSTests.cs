using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Core.Internal;
using Moq;
using NUnit.Framework;
using Service.helpers;
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
        public void TestBFS()
        {
            var graph = _graphService.NewGraph(true);
            var nodeA = _graphService.AddNode(ref graph, "nodeA");
            var nodeB = _graphService.AddNode(ref graph, "nodeB");
            var nodeC = _graphService.AddNode(ref graph, "nodeC");
            _graphService.AddEdge(nodeA, nodeB, ref graph, 10.0D);
            _graphService.AddEdge(nodeB, nodeA, ref graph, 12.0D);
            _graphService.AddEdge(nodeC, nodeA, ref graph, 5.0D);

            Dictionary<string, HashSet<string>> adjList = graphAsAdjacencyList(graph);
            foreach (KeyValuePair<string, HashSet<string>> kpv in adjList)
            {
                Console.WriteLine(kpv.Key + ": ");
                foreach (string s in kpv.Value)
                {
                    Console.Write(s + ", ");
                }
                Console.WriteLine();
            }
            var result = shortestPathToAll(graph, nodeA);
            Console.WriteLine("Od {0}", nodeA.Label);
            foreach (KeyValuePair<string, double> kpv in result)
            {
                Console.WriteLine("do {0} - długość {1}", kpv.Key, kpv.Value);
            }
        }
    }
}
