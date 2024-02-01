using Microsoft.Data.Sqlite;
using Spectre.Console;
using System.Globalization;

namespace CodingTracker
{
    public static class CodingController
    {
        public static void GetAllRecords(string connectionString)
        {
            AnsiConsole.Clear();
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
                                StartTime = DateTime.ParseExact(reader.GetString(1), "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
                                EndTime = DateTime.ParseExact(reader.GetString(2), "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture),
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

        public static void InsertRecord(string connectionString)
        {
            AnsiConsole.Clear();
            string startTime = GetDateInput();
            string endTime = GetDateInput();

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"INSERT INTO daily_coding(StartTime, EndTime) VALUES('{startTime}', '{endTime}')";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static void UpdateRecord(string connectionString)
        {
            AnsiConsole.Clear();
            GetAllRecords(connectionString);
            var recordId = AnsiConsole.Ask<int>("Please enter the ID of the record you want to update");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText =
                    $"SELECT EXISTS(SELECT 1 FROM daily_coding WHERE Id = {recordId})";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    AnsiConsole.WriteLine($"Record with ID {recordId} does not exist");
                    connection.Close();
                    UpdateRecord(connectionString);
                }

                string newStartTime = GetDateInput();
                string newEndTime = GetDateInput();

                var tableCmd = connection.CreateCommand();
                tableCmd.CommandText = $"UPDATE daily_coding SET StartTime = '{newStartTime}', EndTime = '{newEndTime}' WHERE Id = {recordId}";
                tableCmd.ExecuteNonQuery();
                connection.Close();
            }

        }

        public static void DeleteRecord(string connectionString)
        {
            AnsiConsole.Clear();
            GetAllRecords(connectionString);

            var recordId = AnsiConsole.Ask<int>("Please enter the ID of the record you want to delete");

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    $"DELETE from daily_coding WHERE Id = '{recordId}'";

                int rowCount = tableCmd.ExecuteNonQuery();

                if (rowCount == 0)
                {
                    AnsiConsole.WriteLine($"\n\nRecord with ID {recordId} does not exist \n\n");
                    DeleteRecord(connectionString);
                }
            }
            AnsiConsole.WriteLine($"\n\nRecord with ID {recordId} has been deleted.\n\n");

        }

        public static string GetDateInput()
        {
            var dateInput = AnsiConsole.Ask<string>("Please enter the date: (Format: dd-MM-yyyy HH:mm)");

            while (!DateTime.TryParseExact(dateInput, "dd-MM-yyyy HH:mm", new CultureInfo("en-US"), DateTimeStyles.None, out _))
            {
                AnsiConsole.WriteLine("\n\nInvalid date. Please enter a correctly formatted date (dd-MM-yyyy HH:mm). Type 0 to return to main menu or try again\n\n");
                dateInput = AnsiConsole.Ask<string>("Please enter the date: (Format: dd-MM-yyyy HH:mm) Type 0 to return to main menu");
            }

            return dateInput;

        }



    }
}
