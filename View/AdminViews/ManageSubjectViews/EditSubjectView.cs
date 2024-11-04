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
            AnsiConsole.WriteLine("[blue]Podaj idPrzedmiotu do edycji: [/]");
            string value = Console.ReadLine();
            id = int.Parse(value);
            return id;
        }
        public string EditOption()
        {
            string name;
            AnsiConsole.MarkupLine("[blue] Podaj nową nazwę przedmiotu: [/]");
            return Console.ReadLine();
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

