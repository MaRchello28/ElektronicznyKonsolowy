using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageSubjectViews
{
    public class EditSubjectView
    {
        public EditSubjectView() { }
        public int SubjectToEdit()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj id do edycji: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne idKlasy[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.Subjects.Any(c => c.subjectId == id))
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
        public string EditOption()
        {
            string name;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj nową nazwę przedmiotu: [/]");
                name = (Console.ReadLine());
                if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj poprawną nazwe[/]"); }
            }
            while (run);
            return name;
        }
        public void ShowDifference(Subject subjectBeforChanges, Subject subjectAfterChanges)
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Nazwa");

            table.AddRow(
                subjectBeforChanges.name
            );

            table.AddRow(
                HighlightIfDifferent(subjectBeforChanges.name, subjectAfterChanges.name)
            );
            AnsiConsole.Render(table);
        }
        string HighlightIfDifferent(string before, string after)
        {
            return before != after ? $"[red]{after}[/]" : after;
        }
    }
}

