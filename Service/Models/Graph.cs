using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Service.Models {
    public class Graph {
        public Graph() {
        }

        public Graph(bool directed) {
            Directed = directed;
        }

        public int Id { get; set; }
        public virtual ICollection<GraphPart> GraphPart { get; set; }
        public string Uid { get; set; }
        [Required]
        public bool Directed { get; set; }
    }
}