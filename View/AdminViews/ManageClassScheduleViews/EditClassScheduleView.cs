using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews
{
    public class EditClassScheduleView
    {
        public EditClassScheduleView() { }
        public int ClassScheduleToEdit()
        {
            int id;
            AnsiConsole.WriteLine("[blue]Podaj idPrzedmiotu do edycji: [/]");
            string value = Console.ReadLine();
            id = int.Parse(value);
            return id;
        }
        public string EditOption()
        {
            string id;
            AnsiConsole.MarkupLine("[blue] Podaj nową nazwę przedmiotu: [/]");
            return Console.ReadLine();
        }
        public List<bool> ChooseOptionsToEdit()
        {
            List<bool> result = new List<bool> { false, false, false};
            var options = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[green]Wybierz opcje do edycji: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(new[] {
            "ClassId", "Dodaj zajęcia", "Usuń zajęcia"}));

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = options.Contains(new[] { "ClassId", "Dodaj zajęcia", "Usuń zajęcia" }[i]);
            }

            return result;
        }
        public string EditOption(int i)
        {
            if (i == 0)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowe Id Klasy: [/]");
                return Console.ReadLine();
            }
            else if (i == 1)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowe nazwisko rodzica: [/]");
                return Console.ReadLine();
            }
            else if (i == 2)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy login rodzica: [/]");
                return Console.ReadLine();
            }
            return "";
        }
        public void ShowDifference(ClassSchedule classScheduleBeforeChanges, ClassSchedule classScheduleAfterChanges)
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Nazwa");

            table.AddRow(
                classScheduleBeforeChanges.studentClassId.ToString()
            );

            table.AddRow(
                HighlightIfDifferent(classScheduleBeforeChanges.studentClassId.ToString(), classScheduleAfterChanges.studentClassId.ToString())
            );
            AnsiConsole.Render(table);
        }
        string HighlightIfDifferent(string before, string after)
        {
            return before != after ? $"[red]{after}[/]" : after;
        }
    }
}
