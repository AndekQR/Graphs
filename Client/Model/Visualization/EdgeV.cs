using GraphX.PCL.Common.Models;
using System;

namespace Client.Model.Visualization {

    public class EdgeV : EdgeBase<NodeV> {

        public EdgeV() : base(null, null, 1) {
        }

        // przy wyświetlaniu grafu trzeba ustawiać source
        public EdgeV(NodeV source, NodeV dest, double weight, string uid) : base(source, dest, weight) {
            Weight = weight;
            Uid = uid;
        }

        public string Uid { get; set; }

        public override string ToString() {
            return Weight.ToString();
        }
    }
}