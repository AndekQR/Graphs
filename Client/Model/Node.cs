namespace Client.Model {
    public class Node {
        public Node(string label, string uid) {
            Label = label;
            Uid = uid;
        }
        public string Label { get; set; }
        public string Uid { get; set; }
    }
}