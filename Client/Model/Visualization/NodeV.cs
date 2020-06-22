using GraphX.PCL.Common.Models;
using System;

namespace Client.Model.Visualization {

    public class NodeV : VertexBase {

        public NodeV() {
        }

        public NodeV(string label, string uid) {
            Label = label;
            Uid = uid;
        }

        public string Label { get; set; }
        public string Uid { get; set; }

        public override string ToString() {
            return Label;
        }
    }
}