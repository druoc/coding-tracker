using Spectre.Console;



namespace CodingTracker
{
    public static class UserInput
    {
        public static void GetUserInput(string connectionString)
        {
            bool closeApp = false;
            while (!closeApp)
            {
                var userSelection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                    .Title("[green]What would you like to do?[/]")
                    .AddChoices(new[]
                    {
                        "View all records", "Insert a record", "Update a record", "Delete a record", "Close application"
                    })) ;

                switch(userSelection) 
                {
                    case "View all records":
                        CodingController.GetAllRecords(connectionString);
                        break;
                    case "Insert a record":
                        CodingController.InsertRecord(connectionString);
                        break;
                    case "Update a record":
                        CodingController.UpdateRecord(connectionString);
                        break;
                    case "Delete a record":
                        CodingController.DeleteRecord(connectionString);
                        break;
                    case "Close application":
                        AnsiConsole.WriteLine("Bye!\n\n");
                        closeApp = true;
                        break;
                }
            }

        }

        
    }
}
