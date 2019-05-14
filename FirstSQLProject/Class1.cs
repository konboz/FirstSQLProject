using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json;


namespace FirstSQLProject
{
    class Class1
    {
        private const string filename = @"JsonData.txt";
        public void Connection()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                string s3 = "Server =localhost; Database = GroceryStore; Integrated Security=SSPI;Persist Security Info=False;";


                conn.ConnectionString = s3;
                conn.Open();
                // Create the command
                SqlCommand command = new SqlCommand("SELECT [id] ,[name] FROM[dbo].[Groceries]", conn);
                // Add the parameters.
                command.Parameters.Add(new SqlParameter("0", 1));

                using (SqlDataReader reader = command.ExecuteReader())
                {


                    Console.WriteLine("FirstColumn\tSecond Column\t");
                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format("{0} \t | {1} \t  ",
                            reader[0], reader[1]));
                    }

                }




                Console.WriteLine("Press any key to exit");
                Console.ReadLine();

            }
        }

        public void SaveToJson()
        {
            List<Items> items = new List<Items>();
            using (SqlConnection conn = new SqlConnection())
            {
                string s3 = "Server =localhost; Database = GroceryStore; Integrated Security=SSPI;Persist Security Info=False;";


                conn.ConnectionString = s3;
                conn.Open();
                // Create the command
                SqlCommand command = new SqlCommand("SELECT [id] ,[name] FROM[dbo].[Groceries]", conn);
                // Add the parameters.
                command.Parameters.Add(new SqlParameter("0", 1));

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Items item = new Items(int.Parse(reader[0].ToString()), reader[1].ToString());
                        items.Add(item);
                    }
                    string jsonData = JsonConvert.SerializeObject(items);
                    //write string to file
                    File.WriteAllText(filename, jsonData);
                }
            }
        }

        public void InsertDb()
        {
            List<Items> groceries = new List<Items>();
            using (SqlConnection conn = new SqlConnection())
            {
                string s3 = "Server =localhost; Database = GroceryStore; Integrated Security=SSPI;Persist Security Info=False;";


                conn.ConnectionString = s3;
                conn.Open();
                string data = File.ReadAllText(filename);
                groceries = JsonConvert.DeserializeObject<List<Items>>(data);


                foreach (Items t in groceries)
                {

                    SqlCommand InsertCommand = new SqlCommand("INSERT INTO Groceries VALUES (@0)", conn);
                    InsertCommand.Parameters.Add(new SqlParameter("0", t.Name));
                    int c = InsertCommand.ExecuteNonQuery();
                    Console.WriteLine(c);

                }
            }
        }
    }
}
