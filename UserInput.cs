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
    static public class UserInput
    {
        public static void GetUserInput()
        {
            bool closeApp = false;
            while (!closeApp)
            {
                var userSelection = AnsiConsole.Prompt(
                    new MultiSelectionPrompt<string>()
                    .Title("[blue]Welcome to the Coding Tracker[/]")
                    .Title("[green]What would you like to do?[/]")
                    .AddChoices(new[]
                    {
                        "View all records", "Insert a record", "Update a record", "Delete a record"
                    })) ;
            }

        }
    }
}
