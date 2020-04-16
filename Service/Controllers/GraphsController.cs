using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using Service.Models;
using Service.Services;

namespace Service.Controllers {
    public class GraphsController : ApiController {
        private readonly MyDbContext _db = new MyDbContext();
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

        // GET: api/Graphs
        public List<Graph> GetGraphs() {
            DbSet<Graph> graphs = _db.Graphs;
            return graphs.ToList();
        }

        // GET: api/Graphs/5
        [ResponseType(typeof(Graph))]
        public IHttpActionResult GetGraph(int id) {
            Graph graph = _db.Graphs.Find(id);
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
        //     db.Entry(graph).State = EntityState.Modified;
        //
        //     try {
        //         db.SaveChanges();
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

        // POST: api/Graphs
        [ResponseType(typeof(Graph))] //do sprawdzenia
        public IHttpActionResult PostGraph(Graph graph) {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _db.Graphs.Add(graph);
            _db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new {id = graph.Id}, graph);
        }

        // DELETE: api/Graphs/5
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