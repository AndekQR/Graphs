using System;
using System.Collections.Generic;

namespace Client.Model {
    public class Graph {
        public Graph(bool directed, List<GraphPart> graphPart, string uid) {
            Directed = directed;
            GraphPart = graphPart;
            Uid = uid;
        }

        public List<GraphPart> GraphPart { get; set; }
        public string Uid { get; set; }
        public bool Directed { get; set; }

        public override string ToString() {
            string result = "";
            foreach (GraphPart graphPart in GraphPart) {
                foreach (Edge edge in graphPart.Edge) {
                    result += graphPart.Node.Label + " " + edge.Weight +" -> " + edge.Destination.Label + Environment.NewLine;
                }
            }

            return result;
        }
    }
}