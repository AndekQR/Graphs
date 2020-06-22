using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Model {

    public class Graph {

        public Graph(bool directed, List<GraphPart> graphPart, string uid) {
            Directed = directed;
            GraphPart = graphPart;
            Uid = uid;
        }

        public Graph() {
            GraphPart = new List<GraphPart>();
        }

        public int Id { get; set; }
        public List<GraphPart> GraphPart { get; set; }
        public string Uid { get; set; }
        public bool Directed { get; set; }

        public override string ToString() {
            string name = $"{GraphPart.Count} nodes. {(Directed ? "Directed" : "Undirected")}";

            return name;
        }
    }
}