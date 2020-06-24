using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Service.helpers
{
    public class BFSMethods
    {

        public static Dictionary<string, HashSet<string>> GraphAsAdjacencyList(Graph g)
        {
            Dictionary<string, HashSet<string>> result = new Dictionary<string, HashSet<string>>();
            foreach (GraphPart p in g.GraphPart){
                HashSet<string> adjacent = new HashSet<string>();
                foreach (Edge e in p.Edge)
                {
                    adjacent.Add(e.Destination.Label);
                }
                result.Add(p.Node.Label, adjacent);
            }
            return result;
        }

        private static Func<string, IEnumerable<string>> ShortestPathFunction(Dictionary<string, HashSet<string>> adjList, string start)
        {
            var previous = new Dictionary<string, string>();

            var queue = new Queue<string>();
            queue.Enqueue(start);

            while (queue.Count > 0)
            {
                var vertex = queue.Dequeue();
                foreach (var neighbor in adjList[vertex])
                {
                    if (previous.ContainsKey(neighbor))
                        continue;

                    previous[neighbor] = vertex;
                    queue.Enqueue(neighbor);
                }
            }

            Func<string, IEnumerable<string>> shortestPath = v => {
                var path = new List<string> { };

                var current = v;
                while (!current.Equals(start))
                {
                    if (previous.ContainsKey(current))
                    {
                        path.Add(current);
                        current = previous[current];
                    } else
                    {
                        // Coś poszło nie tak, zakładam że wierzochłek v jest nieosiągalny z danego startowego?
                        return path;
                    }
                };

                path.Add(start);
                path.Reverse();

                return path;
            };

            return shortestPath;
        }

        public static Dictionary<string, int> ShortestPathToAll(Graph graph, Node start)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            var adjList = GraphAsAdjacencyList(graph);
            var shortestPath = ShortestPathFunction(adjList, start.Label);
            foreach(string vertex in adjList.Keys)
            {
                // Graf nieskierowany - nie musi istnieć ścieżka do każdego z pozostałtych w tym kierunku
                Console.WriteLine("shortest path to {0,2}: {1}",vertex, string.Join(", ", shortestPath(vertex)));
                result.Add(vertex, shortestPath(vertex).Count()-1);
            }
            return result;
        }

        public static int ShortestPathToGiven(Graph graph, Node start, Node end)
        {
            var adjList = GraphAsAdjacencyList(graph);
            var shortestPath = ShortestPathFunction(adjList, start.Label);
            Console.WriteLine("Najkrótsza ścieżka od {0} do {1}: {2}", start.Label, end.Label, string.Join(", ", shortestPath(end.Label)));
            return shortestPath(end.Label).Count() - 1;
        }
    }
}