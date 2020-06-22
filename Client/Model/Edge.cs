using GraphX.PCL.Common.Models;
using System;

namespace Client.Model {

    public class Edge {

        public Edge() {
        }

        public Edge(Node dest, double weight, string uid) {
            Destination = dest;
            Weight = weight;
            Uid = uid;
        }

        public int Id { set; get; }

        public Node Destination { get; set; }

        public double Weight { get; set; }
        public string Uid { get; set; }

        public override string ToString() {
            return Weight.ToString();
        }
    }
}