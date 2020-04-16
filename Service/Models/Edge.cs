using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Service.Models {
    public class Edge {
        public Edge(Node dest, double weight) {
            Destination = dest;
            Weight = weight;
        }

        public Edge() {
        }

        public int Id { get; set; }

        [Required] public virtual Node Destination { get; set; }

        public double Weight { get; set; }
        public string Uid { get; set; }

        [JsonIgnore] public virtual GraphPart GraphPart { get; set; }
    }
}