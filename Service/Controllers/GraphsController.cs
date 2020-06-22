using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
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

        //TODO: do poprawy, graph.id juz jset inne po wywołaniu metody savegraph
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
    }
}