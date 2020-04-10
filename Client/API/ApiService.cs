using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Service {

    internal class ApiService {
        // żądania do serwisów

        private HttpClient client;
        private string baseAddress = "https://localhost:44313/api";

        public ApiService() {
            this.client = new HttpClient();
        }
    }
}