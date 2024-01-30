using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker
{
    public static class CodingController
    {
        public static void GetAllRecords(string connectionString)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText = "SELECT * FROM daily_coding";

                List<CodingRecord> tableData = new();

                SqliteDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tableData.Add(new CodingRecord
                        {
                            Id = reader.GetInt32(0),
                            StartTime = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),
                            EndTime = DateTime.ParseExact(reader.GetString(1), "dd-MM-yy", new CultureInfo("en-US")),

                        });
                    }
                }
                else
                {
                    AnsiConsole.WriteLine("No rows found");
                }
                connection.Close();



            }
        }
    }
}
