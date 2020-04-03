using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Client.Service {

    internal class Database {
        // operacje na bazie danych
        //musicie sobie uruchomić baze przez xampa i stworzyć bazę danych graphDB w formacie UTF8_unicode
        //https://www.youtube.com/watch?v=2g4MPJ3fJt0

        //singleton
        private static volatile Database instance;

        private static object myLock = new object();
        private string connectionString = "datasource=127.0.0.1;port=3306;username=root;password=;database=graphDB";
        private MySqlConnection connection;

        private Database() {
            connection = new MySqlConnection(connectionString);
        }

        public static Database Instance {
            get {
                if (instance == null) {
                    lock (myLock) {
                        if (instance == null) instance = new Database();
                    }
                }
                return instance;
            }
        }

        public string getAllGraphs() {
            return "All graphs!";
        }
    }
}