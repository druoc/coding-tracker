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
            List<CodingRecord> tableData = new List<CodingRecord>();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = "SELECT * FROM daily_coding";

                using (SqliteDataReader reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CodingRecord record = new CodingRecord
                            {
                                Id = reader.GetInt32(0),
                                StartTime = reader.GetDateTime(1),
                                EndTime = reader.GetDateTime(2),
                            };

                            // Calculate and set the duration as TimeSpan
                            record.Duration = record.EndTime - record.StartTime;

                            tableData.Add(record);
                        }
                    }
                    else
                    {
                        AnsiConsole.WriteLine("No rows found\n");
                    }
                    connection.Close();
                    TableVisualisationEngine.DisplayRecords(tableData);
                }
            }
        }



    }
}
