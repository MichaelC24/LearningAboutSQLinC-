using System;
using System.Data.SqlClient;

namespace FetchDataExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=LocalHost\sqlexpress01;Database=MichaelFirstDatabase;Trusted_Connection=True;";
            string queryString = "SELECT cast(Dates as date) as Dates FROM dbo.Dates";
            
           
            List<DateTime> dateList = new List<DateTime>();
            // Create and open a connection to SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                connection.Open();

                // Execute the command and read the data
                using (SqlDataReader reader = command.ExecuteReader())
                {


                    //List[] datesArray = new list[Dates];
                    while (reader.Read())
                    {
                        DateTime dates = reader.GetDateTime(0);
                        dateList.Add(dates);
                        

                    }
                }
            }

            DateTime[] datesArray = dateList.ToArray();

            foreach (DateTime date in datesArray)
            {
                Console.WriteLine(date);
            }
            //Console.WriteLine("complete");

            DateTime dateSearch = DateTime.MinValue;

            Console.WriteLine("please insert a date");
            string dateinput = Console.ReadLine();

            if(DateTime.TryParse(dateinput, out dateSearch))
            {
                Console.WriteLine("date inputted incorrectly");
            }

            foreach (DateTime date in datesArray)
            {
                if(dateSearch == date)
                {
                    Console.WriteLine("here is the date");
                    Console.WriteLine(date);
                }
            }

        }
    }
}
