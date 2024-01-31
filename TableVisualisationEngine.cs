﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace CodingTracker
{
    public class TableVisualisationEngine
    {
        public static void DisplayRecords(List<CodingRecord> codingRecords)
        {
            var table = new Table();
            table.AddColumn("Id");
            table.AddColumn("Start time");
            table.AddColumn("End time");
            table.AddColumn("Duration");

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
