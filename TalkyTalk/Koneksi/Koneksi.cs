using System;
using System.Data.SqlClient;

namespace ForumApp
{
    internal class Koneksi
    {
        string source;
        public SqlConnection con;

        public Koneksi()
        {
            source = "Integrated Security=true;Initial Catalog=DBforum;Data Source=.";
            con = new SqlConnection(source);
        }

        public void bukaKoneksi()
        {
            try
            {
                con.Open();
                Console.WriteLine("Database connection opened successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error opening database connection: " + ex.Message);
                throw;
            }
        }

        public void tutupKoneksi()
        {
            try
            {
                con.Close();
                Console.WriteLine("Database connection closed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error closing database connection: " + ex.Message);
                throw;
            }
        }
    }
}
