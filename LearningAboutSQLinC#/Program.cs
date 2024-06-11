using System;
using System.Data.SqlClient;

namespace FetchDataExample
{
    class Program
    {

        static void Main(string[] args)
        {
            //variables
            string connectionString = @"Server=LocalHost\sqlexpress01;Database=MichaelFirstDatabase;Trusted_Connection=True;";
            string queryString = "SELECT cast(Dates as date) as Dates, Event_ FROM dbo.Dates";
            string y_nAppointment = ""; //used to see if they want to put in an event
            string appointment = ""; //the actual event that goes to the db
            DateTime dateSearch = DateTime.MinValue; //whats passed to the db to search for the date the inputted
            string dateTempinput = ""; //takes their date input 


            //these temparoarly store the data from the db before passing it to an array.
            List<DateTime> dateList = new List<DateTime>();
            List<string> stringList = new List<string>();

            // Create and open a connection to SQL Server
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //takes the query and connection string and saves it to a variable
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

            //arrays to store data from the db
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


            //while loop to make sure they are inputting the date correctly
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
            //checks to see if they want to input an appointment
            foreach (DateTime date in datesArray)
            {
                //need so i could print out the current event
                foreach (string event_ in stringListArray)
                {
                    //finds the date in the dates array
                    if (dateSearch == date)
                    {

                        Console.WriteLine("Here is the current event: ");
                        Console.WriteLine(event_);
                        Console.WriteLine("here is the date you are now looking at would you like to schedule something for this day? Y/N");
                        Console.WriteLine(date);
                        y_nAppointment = Console.ReadLine().ToLower();

                        break;
                    }
                    //need to put logic to output if the date is not in our database/array

                }

            }

            //checks to see if they want to input an appt.
            if (y_nAppointment == "y")
            {
                Console.WriteLine("What would you like to put on the schedule");
                appointment = Console.ReadLine();


            }
            else if (y_nAppointment == "n")
            {
                return;
            }

            //update query need to make into a procedure 
            string insertAppt = $"UPDATE Dates SET Event_ = '{appointment}' WHERE Dates = '{dateSearch}'";

            //excutes update query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand sqlCommandInsert = new SqlCommand(insertAppt, connection);
                connection.Open();
                sqlCommandInsert.ExecuteNonQuery();
                connection.Close();

                //using (SqlDataAdapter insert = insertAppt.E) 
            }

            //if they inputted an appt. this displays the new appt and the date its on
            if (y_nAppointment == "y")
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            DateTime dates = reader.GetDateTime(0);
                            string eventString = reader.GetString(1);
                            string event_ = eventString.ToString();
                            if (dates == dateSearch)
                            {
                                Console.WriteLine($"Here is your new appointment for {dates}: {eventString}");
                            }
                        }
                    }
                }
            }
            //list everything in the array
            //foreach (DateTime date in datesArray)
            //    {
            //        Console.WriteLine(date);
            //        foreach (string event_ in stringListArray)
            //        {
            //            Console.WriteLine(date + "   " + event_);
            //            //Console.WriteLine("        " + event_);
            //        }
            //    }
            //    Console.WriteLine("complete");
        }
    }
}