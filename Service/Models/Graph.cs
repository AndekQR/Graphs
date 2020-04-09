using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Service.Models {

    public class Graph {
        public int Id { get; set; }

        /*        public Dictionary<Node, LinkedList<Edge>> adjencyMap { get; set; }
        */

        public virtual ICollection<GraphPart> GraphPart { get; set; }
        public string Uid { get; set; }
        public bool Directed { get; set; }

        public Graph(bool directed) {
            this.Directed = directed;
        }

        public Graph() {
        }
    }
}