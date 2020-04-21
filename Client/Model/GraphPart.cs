using System.Collections.Generic;

namespace Client.Model {
    public class GraphPart {

        public GraphPart(Node node, List<Edge> edge, string uid) {
            Node = node;
            Edge = edge;
            Uid = uid;
        }
        
        public int Id { get; set; }
        public  Node Node { get; set; }
        public List<Edge> Edge { get; set; }
        public string Uid { get; set; }
    }
}