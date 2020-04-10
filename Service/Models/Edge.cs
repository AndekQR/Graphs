using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Service.Models {

    public class Edge {
        public int Id { get; set; }
        public virtual Node Destination { get; set; }
        public double Weight { get; set; }
        public string Uid { get; set; }

        [JsonIgnore]
        public virtual GraphPart GraphPart { get; set; }

        public Edge(Node dest, double weight) {
            this.Destination = dest;
            this.Weight = weight;
        }

        public Edge() {
        }
    }
}