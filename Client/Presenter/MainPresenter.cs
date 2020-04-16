using System;
using System.Threading;
using System.Threading.Tasks;
using Client.API;
using Client.Model;
using Client.Service;
using Client.View;

namespace Client.Presenter {

    public class MainPresenter {

        private readonly IMainView _mainView;
        private readonly GraphService _graphService;
        private readonly ApiService _apiService;

        public MainPresenter(IMainView mainView) {
            this._mainView = mainView;
            _graphService = new GraphService();
            _apiService = new ApiService();
        }

        public async Task MakeFullName() {
            Graph graph = _graphService.NewGraph(true);

            Node nodeA = _graphService.AddNode(ref graph, "nodeA");
            Node nodeB = _graphService.AddNode(ref graph, "nodeB");
            Node nodeC = _graphService.AddNode(ref graph, "nodeC");

            _graphService.AddEdge(nodeA, nodeB, ref graph, 12.0D);
            _graphService.AddEdge(nodeB, nodeA, ref graph, 10.0D);
            _graphService.AddEdge(nodeC, nodeA, ref graph, 7.0D);

            _graphService.RemoveEdge(nodeB, nodeA, ref graph);
            
            string firstName = _mainView.PersonName;
            string lastName = _mainView.LastName;
            string fullName = firstName + " " + lastName;



            new Thread(async x => {
                Graph downloadedGraph = await _apiService.GetGraph(2);
                _mainView.LogTextBox = downloadedGraph.ToString();
            }).Start();
            
            _mainView.FullName = fullName;
        }
    }
}