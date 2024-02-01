using Spectre.Console;

namespace CodingTracker
{
    public class TableVisualisationEngine
    {
        public static void DisplayRecords(List<CodingRecord> codingRecords)
        {
            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.AddColumn("Id");
            table.AddColumn("Start time");
            table.AddColumn("End time");
            table.AddColumn("Duration");

            AnsiConsole.Write(
            new FigletText("Daily Coding Time")
            .Centered()
            .Color(Color.Red)); ;

            foreach (var record in codingRecords)
            {
                table.AddRow(
                    record.Id.ToString(),
                    record.StartTime.ToString("dd-MM-yyyy HH:mm"),
                    record.EndTime.ToString("dd-MM-yyyy HH:mm"),
                    record.Duration.ToString("hh\\:mm")
                );
            }

            AnsiConsole.Write(table);
        }
    }

}
