using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Documents;
using Client.Model;
using Newtonsoft.Json;

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
    }
}