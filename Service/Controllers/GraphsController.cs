using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.EntityFrameworkCore;
using Service.Models;
using Service.Services;
using EntityState = System.Data.Entity.EntityState;

namespace Service.Controllers {
    public class GraphsController : ApiController {
        private readonly GraphService _graphService = new GraphService();

        [HttpGet]
        public IHttpActionResult CreateGraph() {
            Graph graph = _graphService.NewGraph(true);

            Node nodeA = _graphService.AddNode(ref graph, "nodeA");
            Node nodeB = _graphService.AddNode(ref graph, "nodeB");
            Node nodeC = _graphService.AddNode(ref graph, "nodeC");

            _graphService.AddEdge(nodeA, nodeB, ref graph, 12.0D);
            _graphService.AddEdge(nodeB, nodeA, ref graph, 10.0D);
            _graphService.AddEdge(nodeC, nodeA, ref graph, 7.0D);

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

        // PUT: api/Graphs/5
        // [ResponseType(typeof(void))]
        // public IHttpActionResult PutGraph(int id, Graph graph) {
        //     if (!ModelState.IsValid) {
        //         return BadRequest(ModelState);
        //     }
        //
        //     if (id != graph.Id) {
        //         return BadRequest();
        //     }
        //     
        //     _db.Entry(graph).State = EntityState.Modified;
        //
        //     try {
        //         _db.SaveChanges();
        //     } catch (DbUpdateConcurrencyException) {
        //         if (!GraphExists(id)) {
        //             return NotFound();
        //         } else {
        //             throw;
        //         }
        //     }
        //
        //     return StatusCode(HttpStatusCode.NoContent);
        // }
        
        [HttpPost]
        public IHttpActionResult PostGraph(Graph graph) {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            _graphService.SaveGraph(graph);
            return CreatedAtRoute("ActionApi", new {id = graph.Id}, graph);
        }
        
        [HttpDelete]
        [ResponseType(typeof(Graph))]
        public IHttpActionResult DeleteGraph(int id) {
            try {
                _graphService.DeleteGraph(id);
                return Ok();
            }
            catch (KeyNotFoundException) {
                return NotFound();
            }
        }
    }
}