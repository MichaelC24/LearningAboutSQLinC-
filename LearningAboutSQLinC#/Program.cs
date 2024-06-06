using System;
using System.Data.SqlClient;

namespace FetchDataExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=LocalHost\sqlexpress01;Database=MichaelFirstDatabase;Trusted_Connection=True;";
            string queryString = "SELECT Dates FROM dbo.Dates";

            // Create and open a connection to SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                // Execute the command and read the data
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Get the values from the current row
                        DateTime date = reader.GetDateTime(0);

                        // Print the values
                        Console.WriteLine($"Date: {date}");
                    }
                }
            }

            Console.WriteLine("Data retrieval completed.");
            Console.ReadKey();
        }
    }
}
