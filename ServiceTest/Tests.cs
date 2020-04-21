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

namespace ServiceTest {
    
    public class GraphServiceTests {
        
        private IGraphService _graphService;
        private FakeDb<Graph> _fakeDb;

        [SetUp]
        public void Init() {
            _fakeDb = new FakeDb<Graph>();
            var myDbContextMock = new Mock<MyDbContext>();
            // myDbContextMock.Setup(x => x.SaveChanges())
            //     .Returns(_fakeDb.SaveChanges);
            
            
            
            _graphService = new GraphService();
        }

        [Test]
        public void NewGraphTest() {
            var graph = _graphService.NewGraph(true);
            Assert.NotNull(graph);
            Assert.NotNull(graph.Uid);
            Assert.NotNull(graph.GraphPart);
        }

        [Test]
        public void AddNodeTest() {
            var graph = _graphService.NewGraph(true);
            var nodeA = _graphService.AddNode(ref graph, "nodeA");
            var nodeB = _graphService.AddNode(ref graph, "nodeB");
            
            Assert.That(graph.GraphPart.Count  == 2);
            foreach (GraphPart graphPart in graph.GraphPart) {
                Assert.That(graphPart.Node.Equals(nodeA) || graphPart.Node.Equals(nodeB));
                Assert.NotNull(graphPart.Edge);
                Assert.NotNull(graphPart.Uid);
            }
        }

        [Test]
        public void AddEdgeTest() {
            var graph = _graphService.NewGraph(true);
            var nodeA = _graphService.AddNode(ref graph, "nodeA");
            var nodeB = _graphService.AddNode(ref graph, "nodeB");
            var nodeC = _graphService.AddNode(ref graph, "nodeC");
            
            _graphService.AddEdge(nodeA, nodeB, ref graph, 10.0D);
            GraphPart graphPart = graph.GraphPart.ToList().Find(part => part.Node.Uid == nodeA.Uid);
            Assert.That(graphPart.IsNotNull);
            Edge edgeFromAToB = graphPart.Edge.ToList().Find(edge => edge.Destination.Uid == nodeB.Uid);
            Assert.NotNull(edgeFromAToB);
            Assert.That(edgeFromAToB.Weight.IsNotNull);
            Assert.That(edgeFromAToB.Uid.IsNotNull);
            Assert.That(edgeFromAToB.Weight == 10.0D);
            // Assert.NotNull(edgeFromAToB.GraphPart); - nie można sprawdzić bo ustawia to pole entity framework
            
            _graphService.AddEdge(nodeA, nodeB, ref graph, 5.0D);
            GraphPart graphPart1 = graph.GraphPart.ToList().Find(part => part.Node.Uid == nodeA.Uid);
            Assert.That(graphPart1.IsNotNull);
            Assert.That(graphPart.Edge.Count == 1);
            Edge edgeFromAToBMod = graphPart.Edge.ToList().Find(edge => edge.Destination.Uid == nodeB.Uid);
            Assert.NotNull(edgeFromAToBMod);
            Assert.That(edgeFromAToBMod.Weight == 5.0D);
            Assert.That(edgeFromAToB.Uid == edgeFromAToBMod.Uid);
        }
        
        [Test]
        public void IsEdgeExistsTest() {
            var graph = _graphService.NewGraph(true);
            var nodeA = _graphService.AddNode(ref graph, "nodeA");
            var nodeB = _graphService.AddNode(ref graph, "nodeB");

            _graphService.AddEdge(nodeA, nodeB, ref graph, 10.0D);
            Edge edge = _graphService.IsEdgeExists(nodeA, nodeB, graph);
            Assert.That(edge.Destination.Uid == nodeB.Uid);
            Assert.That(edge.Weight == 10.0D);
        }

        [Test]
        public void RemoveEdgeTest() {
            var graph = _graphService.NewGraph(true);
            var nodeA = _graphService.AddNode(ref graph, "nodeA");
            var nodeB = _graphService.AddNode(ref graph, "nodeB");
            _graphService.AddEdge(nodeA, nodeB, ref graph, 10.0D);

            GraphPart graphPart = graph.GraphPart.ToList().Find(x => x.Node.Uid == nodeA.Uid);
            Assert.NotNull(graphPart);
            Assert.That(graphPart.Edge.Count == 1);
            Edge edge = graphPart.Edge.ToList().Find(x => x.Destination.Uid == nodeB.Uid);
            Assert.NotNull(edge);
            
            _graphService.RemoveEdge(nodeA , nodeB, ref graph);
            GraphPart graphPart1 = graph.GraphPart.ToList().Find(x => x.Node.Uid == nodeA.Uid);
            Assert.NotNull(graphPart1);
            Assert.That(graphPart.Edge.Count == 0);
            Assert.True(_graphService.IsInGraph(graph, nodeA));
            Assert.True(_graphService.IsInGraph(graph, nodeB));
        }

        [Test]
        public void UpdateGraphTest() {
            var graph = _graphService.NewGraph(true);
            var nodeA = _graphService.AddNode(ref graph, "nodeA");
            var nodeB = _graphService.AddNode(ref graph, "nodeB");
            var nodeC = _graphService.AddNode(ref graph, "nodeC");
            _graphService.AddEdge(nodeA, nodeB, ref graph, 10.0D);
            _graphService.AddEdge(nodeB, nodeA, ref graph, 12.0D);
            _graphService.AddEdge(nodeC, nodeA, ref graph, 5.0D);
            
            
        }
    }
}