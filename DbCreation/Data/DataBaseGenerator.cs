using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace DbCreation.Data
{
    public class DataBaseGenerator
    {
        private readonly IDatabaseConnection _dbConnection;
        private readonly IDataBaseUrls _urls;

        public DataBaseGenerator(IDatabaseConnection dbConnection, IDataBaseUrls urls)
        {
            _dbConnection = dbConnection;
            _urls = urls;
        }

        public void Generate() {
            Console.WriteLine($"connectiong to {_dbConnection.ConnectionString}");
            SqlConnection connection = new SqlConnection(_dbConnection.ConnectionString);
            connection.Open();
            Generate(connection);
            connection.Close();    
        }

        private void Generate(SqlConnection connection)
        {
            Console.WriteLine("fetching queries for database creation");
            string[] commands = readText(_urls.CreationQueryName);
            Console.WriteLine("executing queries for database creation\n---");
            foreach (string s in commands)
            {
                Console.WriteLine(s);
                SqlCommand command = new SqlCommand(s, connection);
                command.ExecuteNonQuery();
            }
        }

        public void Regenerate() {
            Console.WriteLine($"connectiong to {_dbConnection.ConnectionString}");
            SqlConnection connection = new SqlConnection(_dbConnection.ConnectionString);
            connection.Open();
            Console.WriteLine("fetching queries for database removal");
            string[] commands = readText(_urls.RemovalQueryName);
            Console.WriteLine("executing queries for database removal\n---");
            SqlCommand command = new SqlCommand("", connection);
            foreach (string s in commands)
            {
                Console.WriteLine(s);
                command.CommandText = s;
                command.ExecuteNonQuery();
            }
            Generate(connection);
            connection.Close();
        }

        private string[] readText(string url) {
            return File.ReadAllText(url).Split(';').Select(s => s + ';').ToArray();
        }
    }
}
