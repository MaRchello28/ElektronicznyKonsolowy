using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageSessionViews
{
    public class EditSessionView
    {
        AddSessionView view; MyDbContext db;
        public EditSessionView() {db=this.db; view = new AddSessionView(db); }
        public int SessionToEdit()
        {
            int id;
            AnsiConsole.WriteLine("[blue]Podaj idSesji do edycji: [/]");
            string value = Console.ReadLine();
            id = int.Parse(value);
            return id;
        }
        public List<bool> ChooseOptionsToEdit()
        {
            List<bool> result = new List<bool> { false, false, false, false };
            var options = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[green]Wybierz opcje do edycji: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(new[] {
            "SubjectId", "TeacherId", "Dzień", "Godzina od"}));

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = options.Contains(new[] { "SubjectId", "TeacherId", "Dzień", "Godzina od"}[i]);
            }

            return result;
        }
        public string EditOption(int i)
        {
            if (i == 0)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowe SubjectId: [/]");
                return Console.ReadLine();
            }
            else if (i == 1)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy TeacherId: [/]");
                return Console.ReadLine();
            }
            else if (i == 2)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy Dzień: [/]");
                name=view.EnterDzien().ToString();
                return name;
            }
            else if (i == 3)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nową godzinę rozpoczęcia: [/]");
                name = view.EnterHourFrom().ToString();
                return name;
            }
            return "";
        }
        public void ShowDifference(Session sessionBeforeChanges, Session sessionAfterChanges)
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("SubjectId"); table.AddColumn("TeacherId"); table.AddColumn("Dzień"); table.AddColumn("Godzina od"); table.AddColumn("Godzina do");
            table.AddRow(
                sessionBeforeChanges.subjectId.ToString(),
                sessionBeforeChanges.teacherId.ToString(),
                sessionBeforeChanges.dayOfTheWeek.ToString(),
                sessionBeforeChanges.hourFrom.ToString(),
                sessionBeforeChanges.hourTo.ToString()
            );

            table.AddRow(
                HighlightIfDifferent(sessionBeforeChanges.subjectId.ToString(), sessionAfterChanges.subjectId.ToString()),
                HighlightIfDifferent(sessionBeforeChanges.teacherId.ToString(), sessionAfterChanges.teacherId.ToString()),
                HighlightIfDifferent(sessionBeforeChanges.dayOfTheWeek.ToString(), sessionAfterChanges.dayOfTheWeek.ToString()),
                HighlightIfDifferent(sessionBeforeChanges.hourFrom.ToString(), sessionAfterChanges.hourFrom.ToString()),
                HighlightIfDifferent(sessionBeforeChanges.hourTo.ToString(), sessionAfterChanges.hourTo.ToString())
            );
            AnsiConsole.Render(table);
        }
        string HighlightIfDifferent(string before, string after)
        {
            return before != after ? $"[red]{after}[/]" : after;
        }
    }
}
