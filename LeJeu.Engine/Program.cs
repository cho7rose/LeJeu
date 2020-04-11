using System;
using MySql.Data.MySqlClient;


namespace LeJeu
{
    class ConnectToMySQL
    {
        public MySql.Data.MySqlClient.MySqlConnection conn;
        public ConnectToMySQL()
        {
            string myConnectionString;
            string server="localhost";
            string uid="root";
            string password="";
            string database="lejeu";
            myConnectionString = "server="+server+";"+"uid="+uid+";"+"pwd="+password+";"+"database="+database+";";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    
                    Console.WriteLine(ex.Message);
                }
        }
    }
    class SqlStuff
    {
        public void CreateTables(MySql.Data.MySqlClient.MySqlConnection conn)
        {
            string[] sqlQuery;
            //string sql="";
            try
            {
                sqlQuery=System.IO.File.ReadAllLines("./CreateTables.sql");
                foreach (string s in sqlQuery)
                {
                    MySqlCommand cmd = new MySqlCommand(s, conn);
                    MySqlDataReader rdr = cmd.ExecuteReader();
                    rdr.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var cnx = new ConnectToMySQL();
            new SqlStuff().CreateTables(cnx.conn);

        }
    }
}
