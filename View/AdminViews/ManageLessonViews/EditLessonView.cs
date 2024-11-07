using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageLessonViews
{
    public class EditLessonView
    {
        public EditLessonView() { }
        public int LessonToEdit()
        {
            int id;
            AnsiConsole.WriteLine("[blue]Podaj IdLekcji do edycji: [/]");
            string value = Console.ReadLine();
            id = int.Parse(value);
            return id;
        }
        public List<bool> ChooseOptionsToEdit()
        {
            List<bool> result = new List<bool> { false, false, false };
            var options = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[green]Wybierz opcje do edycji: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(new[] {
            "Temat", "Opis", "Numer"}));

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = options.Contains(new[] { "Temat", "Opis", "Numer" }[i]);
            }

            return result;
        }
        public string EditOption(int i)
        {
            if (i == 0)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy temat lekcji: [/]");
                return Console.ReadLine();
            }
            else if (i == 1)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy opis lekcji: [/]");
                return Console.ReadLine();
            }
            else if (i == 2)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy numer lekcji: [/]");
                return Console.ReadLine();
            }
            return "";
        }
        public void ShowDifference(Lesson lessonBeforeChanges, Lesson lessonAfterChanges)
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Numer"); table.AddColumn("Temat"); table.AddColumn("Opis");

            table.AddRow(
                lessonBeforeChanges.nuberOfLesson.ToString(),
                lessonBeforeChanges.name,
                lessonBeforeChanges.description
            );

            table.AddRow(
                HighlightIfDifferent(lessonBeforeChanges.nuberOfLesson.ToString(), lessonAfterChanges.nuberOfLesson.ToString()),
                HighlightIfDifferent(lessonBeforeChanges.name, lessonAfterChanges.name),
                HighlightIfDifferent(lessonBeforeChanges.description, lessonAfterChanges.description)
            );
            AnsiConsole.Render(table);
        }
        string HighlightIfDifferent(string before, string after)
        {
            return before != after ? $"[red]{after}[/]" : after;
        }
    }
}

