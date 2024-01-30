using Microsoft.Data.Sqlite;


namespace CodingTracker
{
    public class Program
    {
        static string connectionString = @"Data Source=coding-Tracker.db";
        static void Main(string[] args)
        {
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                var tableCmd = connection.CreateCommand();

                tableCmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS daily_coding (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        StartTime TEXT,
                        EndTime TEXT,
                        Duration TEXT
                        )";

                tableCmd.ExecuteNonQuery();
                connection.Close();
            }
            UserInput.GetUserInput(connectionString);
        }
    }
}