using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Service {

    public class MainRepository {
        // zarządza baza danych i żadaniami do serisów (publiczna klasa)

        private ApiService apiService;
        private Database db;

        public MainRepository() {
            apiService = new ApiService();
            db = Database.Instance;
        }
    }
}