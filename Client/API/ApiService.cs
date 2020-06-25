using Client.Model;
using Service.helpers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Client.API {

    internal class ApiService {
        private static readonly HttpClient HttpClient;
        private const string BaseAddress = "https://localhost:44313/api/graphs/";

        static ApiService() {
            HttpClient = new HttpClient();
            HttpClient.BaseAddress = new Uri(BaseAddress);
            HttpClient.DefaultRequestHeaders.Accept.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<Graph> GetGraph(int id) {
            // string response = await HttpClient.GetStringAsync("getgraph/" + id);
            // Graph data = JsonConvert.DeserializeObject<Graph>(response);
            // return data;
            HttpResponseMessage response = await HttpClient.GetAsync("getgraph/" + id);
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Graph>().Result : null;
        }

        public async Task<Graph> SaveGraph(Graph graph) {
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("postgraph/", graph);
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Graph>().Result : null;
        }

        public async Task<Graph> UpdateGraph(Graph graph) {
            HttpResponseMessage response = await HttpClient.PutAsJsonAsync("putgraph/", graph);
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Graph>().Result : null;
        }

        public async Task<List<Graph>> getAllGraphs() {
            HttpResponseMessage response = await HttpClient.GetAsync("GetGraphs/");
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<List<Graph>>().Result : null;
        }

        public async Task<bool> DeleteGraph(int id) {
            HttpResponseMessage response = await HttpClient.DeleteAsync("DeleteGraph/" + id);
            return response.IsSuccessStatusCode ? true : false;
        }

        public async Task<int> GetMinimalPathWeight(int id, string start, string end) {
            BFSObject bFSObject = new BFSObject() {
                graphId = id,
                startNodeLabel = start,
                endNodeLabel = end
            };
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("GetMinimalPathWeightBFS/", bFSObject);
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<int>().Result : -1;
        }

        public async Task<Node> GetMinimalPathToAll(int graphId) {
            BFSObject bfsObject = new BFSObject() {
                graphId = graphId
            };
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("GetMinNodeBFS/", bfsObject);
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Node>().Result : null;
        }

        public async Task<Node> GetMinNodeDijkstra(int graphId) {
            BFSObject bfsObject = new BFSObject() {
                graphId = graphId
            };
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("GetMinNodeDijkstra/", bfsObject);
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<Node>().Result : null;
        }

        public async Task<double> GetMinimalPathWeightDijkstra(int id, string start, string end) {
            BFSObject bFSObject = new BFSObject() {
                graphId = id,
                startNodeLabel = start,
                endNodeLabel = end
            };
            HttpResponseMessage response = await HttpClient.PostAsJsonAsync("GetWeightBetweenNodesDijkstra/", bFSObject);
            return response.IsSuccessStatusCode ? response.Content.ReadAsAsync<double>().Result : -1.0;
        }
    }
}