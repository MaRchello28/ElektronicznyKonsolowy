﻿using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews
{
    public class DeleteClassScheduleView
    {
        MyDbContext db;
        public DeleteClassScheduleView(MyDbContext db) { this.db = db; }
        public int PutIndex()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.Markup("[red]Podaj index, który chcesz usunąć: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawny index[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.ClassSchedules.Any(c => c.classScheduleId == id))
                        {
                            AnsiConsole.MarkupLine("[red]Podane id nie jest w bazie[/]");
                        }
                        else
                        {
                            run = false;
                        }
                    }
                }
            }
            while (run);
            return id;
        }
        public int Agree()
        {
            var options = new[] {
            "Tak",
            "Nie",
            };
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Czy jesteś pewny, że chcesz usunąć ten plan lekcji?")
                    .PageSize(10)
                    .AddChoices(options));

            return Array.IndexOf(options, selectedOption);
        }
    }
}
