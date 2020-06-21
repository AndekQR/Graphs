using GraphX.PCL.Common.Models;
using System;

namespace Client.Model {

    public class Edge : EdgeBase<Node> {

        public Edge() : base(null, null, 1) {
        }

        // przy wyświetlaniu grafu trzeba ustawiać source
        public Edge(Node dest, double weight, string uid) : base(null, dest, weight) {
            Destination = dest;
            Weight = weight;
            Uid = uid;
            ID = new DateTime().Ticks;
        }

        public Node Destination { get => Target; set => Target = value; }

        // public double Weight { get; set; }
        public string Uid { get; set; }

        public override string ToString() {
            return Weight.ToString();
        }
    }
}