using System;
using System.Data.SqlClient;

namespace FetchDataExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=LocalHost\sqlexpress01;Database=MichaelFirstDatabase;Trusted_Connection=True;";
            string queryString = "SELECT cast(Dates as date) as Dates, COALESCE(Event_,'FILLER') FROM dbo.Dates";
            string y_nAppointment = "";
            string appointment = "";
            DateTime dateSearch = DateTime.MinValue;
            string dateTempinput = "";

            List<DateTime> dateList = new List<DateTime>();
            List<string> stringList = new List<string>();

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
                        string eventString = reader.GetString(1);
                        string event_ = eventString.ToString();
                        dateList.Add(dates);
                        stringList.Add(event_);

                    }
                }
                connection.Close();
            }

            DateTime[] datesArray = dateList.ToArray();
            string[] stringListArray = stringList.ToArray();

            //foreach (DateTime date in datesArray)
            //{
            //    Console.WriteLine(date);
            //    foreach (string event_ in stringListArray) {
            //        Console.WriteLine(date + "   " + event_);
            //        //Console.WriteLine("        " + event_);
            //    }
            //}
            //Console.WriteLine("complete");

            

            while (true)
            {

                Console.WriteLine("please insert a date");
                dateTempinput = Console.ReadLine();

                DateTime result;

                if (DateTime.TryParse(dateTempinput, out result))
                {
                    dateSearch = result;
                    break;
                }
                else 
                {
                    Console.WriteLine("Date inputted incorrectly please use this format MM/DD/YYYY");
                }


            }
            foreach (DateTime date in datesArray)
            {
                if (dateSearch == date)
                {
                    Console.WriteLine("here is the date you are now looking at would you like to schedule something for this day? Y/N");
                    Console.WriteLine(date);
                    y_nAppointment = Console.ReadLine().ToLower();
                }
                //else if (dateSearch != date)
                //{
                //    Console.WriteLine("there is no date in our system that matches the provided date.");
                //}
                if (y_nAppointment == "y")
                {
                    Console.WriteLine("What would you like to put on the schedule");
                    appointment = Console.ReadLine();


                }

                string insertAppt = $"UPDATE Dates SET Event_ = '{appointment}' WHERE Dates = '{dateSearch}'";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand sqlCommandInsert = new SqlCommand(insertAppt, connection);
                    connection.Open();
                    sqlCommandInsert.ExecuteNonQuery();

                    //using (SqlDataAdapter insert = insertAppt.E) 
                }
            }
        }
    }
}