using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Service.Models;
using Service.Services;

namespace Service.Controllers {

    public class GraphsController : ApiController {
        private MyDbContext db = new MyDbContext();
        private GraphService graphService = new GraphService();

        [HttpGet]
        public IHttpActionResult CreateGraph() {
            Graph graph = graphService.NewGraph(true);

            Node nodeA = graphService.AddNode(ref graph, "nodeA");
            Node nodeB = graphService.AddNode(ref graph, "nodeB");
            Node nodeC = graphService.AddNode(ref graph, "nodeC");

            graphService.AddEdge(nodeA, nodeB, ref graph, 12.0D);
            graphService.AddEdge(nodeB, nodeA, ref graph, 10.0D);
            graphService.AddEdge(nodeC, nodeA, ref graph, 7.0D);

            return Ok(graph);
        }

        // pozostałem metody są do poprawienia.......................................
        
        
        // GET: api/Graphs
        public List<Graph> GetGraphs() {
            var graphs = db.Graphs;
            return graphs.ToList();
        }

        // GET: api/Graphs/5
        [ResponseType(typeof(Graph))]
        public IHttpActionResult GetGraph(int id) {
            Graph graph = db.Graphs.Find(id);
            if (graph == null) {
                return NotFound();
            }
            
            return Ok(graph);
           
        }

        // PUT: api/Graphs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGraph(int id, Graph graph) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            if (id != graph.Id) {
                return BadRequest();
            }

            db.Entry(graph).State = EntityState.Modified;

            try {
                db.SaveChanges();
            } catch (DbUpdateConcurrencyException) {
                if (!GraphExists(id)) {
                    return NotFound();
                } else {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Graphs
        [ResponseType(typeof(Graph))]
        public IHttpActionResult PostGraph(Graph graph) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            db.Graphs.Add(graph);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = graph.Id }, graph);
        }

        // DELETE: api/Graphs/5
        [ResponseType(typeof(Graph))]
        public IHttpActionResult DeleteGraph(int id) {
            Graph graph = db.Graphs.Find(id);
            if (graph == null) {
                return NotFound();
            }

            db.Graphs.Remove(graph);
            db.SaveChanges();

            return Ok(graph);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GraphExists(int id) {
            return db.Graphs.Count(e => e.Id == id) > 0;
        }
    }
}