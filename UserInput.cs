using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
using Spectre.Console.Cli;


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
                        AnsiConsole.WriteLine("Insert a record\n\n");
                        break;
                    case "Update a record":
                        AnsiConsole.WriteLine("Update a record\n\n");
                        break;
                    case "Delete a record":
                        AnsiConsole.WriteLine("Delete a record\n\n");
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
