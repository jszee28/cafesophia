using MySql.Data.MySqlClient;   
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cafesophia
{
    internal class DBConnection
    {
        private static string connectionString = "server=localhost;database=sophia_inventory;uid=root;pwd=JOHNZENDATABASE;";

        public static MySqlConnection connection = new MySqlConnection(connectionString);

        // Open Connection
        public static void Open()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        // Close Connection
        public static void Close()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }
    }
}
