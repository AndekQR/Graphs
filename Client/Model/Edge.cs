namespace Client.Model {
    public class Edge {
        
        public Edge(Node dest, double weight, string uid) {
            Destination = dest;
            Weight = weight;
            Uid = uid;
        }

        public int Id { get; set; }
        public Node Destination { get; set; }
        public double Weight { get; set; }
        public string Uid { get; set; }

    }
}