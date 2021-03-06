﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Service.helpers;
using Service.Models;
using Service.Services;
using Service.Services.Interfaces;
using EntityState = System.Data.Entity.EntityState;

namespace Service.Controllers {

    public class GraphsController : ApiController {
        private readonly IGraphService _graphService = new GraphService();

        [HttpGet]
        public IHttpActionResult CreateGraph() {
            Graph graph = _graphService.NewGraph(true);

            Node nodeA = _graphService.AddNode(ref graph, "nodeA");
            Node nodeB = _graphService.AddNode(ref graph, "nodeB");
            Node nodeC = _graphService.AddNode(ref graph, "nodeC");

            _graphService.AddEdge(nodeA, nodeB, ref graph);
            _graphService.AddEdge(nodeB, nodeC, ref graph);
            _graphService.AddEdge(nodeC, nodeA, ref graph, 10.0D);
            // _graphService.AddEdge(nodeC, nodeA, ref graph, 7.0D);

            _graphService.SaveGraph(graph);

            return Ok(graph);
        }

        [HttpGet]
        public List<Graph> GetGraphs() {
            return _graphService.GetAllGraphs();
        }

        [HttpGet]
        [ResponseType(typeof(Graph))]
        public IHttpActionResult GetGraph(int id) {
            Graph graph = _graphService.GetGraph(id);
            if (graph == null) return NotFound();

            return Ok(graph);
        }

        //graph.id wskazuje który graf jest do aktualizacji
        [ResponseType(typeof(Graph))]
        public IHttpActionResult PutGraph(Graph graph) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            // 0 to domyślna wartość dla int i nie powinna być taka w db
            if (graph.Id == default) {
                return BadRequest("Invalid graph id");
            }
            try {
                _graphService.UpdateGraph(graph);
            } catch (KeyNotFoundException e) {
                return NotFound();
            }
            return CreatedAtRoute("ActionApi", new { id = graph.Id }, graph);
        }

        [HttpPost]
        public IHttpActionResult PostGraph(Graph graph) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _graphService.SaveGraph(graph);
            return CreatedAtRoute("ActionApi", new { id = graph.Id }, graph);
        }

        [HttpDelete]
        [ResponseType(typeof(Graph))]
        public IHttpActionResult DeleteGraph(int id) {
            try {
                _graphService.DeleteGraph(id);
                return Ok();
            } catch (KeyNotFoundException) {
                return NotFound();
            }
        }

        [HttpPost]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetMinimalPathWeightBFS(BFSObject bFSObject) {
            Graph graph = _graphService.GetGraph(bFSObject.graphId);
            Node startNode = _graphService.findNodeByLabel(graph, bFSObject.startNodeLabel);
            Node endNode = _graphService.findNodeByLabel(graph, bFSObject.endNodeLabel);

            if (startNode != null && endNode != null) {
                int weight = BFSMethods.ShortestPathToGiven(graph, startNode, endNode);
                return Ok(weight);
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ResponseType(typeof(Node))]
        public IHttpActionResult GetMinNodeBFS(BFSObject bfsObject) {
            Graph graph = _graphService.GetGraph(bfsObject.graphId);
            if (graph != null) {
                return Ok(_graphService.getMinNodeCoarseGrained(graph));
            } else {
                return NotFound();
            }
        }

        [HttpPost]
        [ResponseType(typeof(Node))]
        public IHttpActionResult GetMinNodeDijkstra(BFSObject bfsObject) {
            Graph graph = _graphService.GetGraph(bfsObject.graphId);
            if (graph == null) {
                return NotFound();
            } else {
                Node node = _graphService.GetMinNodeDijkstra(graph);
                return Ok(node);
            }
        }

        [HttpPost]
        [ResponseType(typeof(double))]
        public IHttpActionResult GetWeightBetweenNodesDijkstra(BFSObject bfsObject) {
            Graph graph = _graphService.GetGraph(bfsObject.graphId);
            Node startNode = _graphService.findNodeByLabel(graph, bfsObject.startNodeLabel);
            Node endNode = _graphService.findNodeByLabel(graph, bfsObject.endNodeLabel);

            if (graph != null && startNode != null && endNode != null) {
                double weight = _graphService.GetWeightBetweenNodesDijkstra(graph, startNode, endNode);
                return Ok(weight);
            } else {
                return NotFound();
            }
        }
    }
}