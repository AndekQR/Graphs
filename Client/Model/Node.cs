using GraphX.PCL.Common.Models;
using System;

namespace Client.Model {

    public class Node : VertexBase {

        public Node() {
        }

        public Node(string label, string uid) {
            Label = label;
            Uid = uid;
            ID = new DateTime().Ticks;
        }

        public string Label { get; set; }
        public string Uid { get; set; }

        public override string ToString() {
            return Label;
        }
    }
}