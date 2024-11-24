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
                        if (!context.Lessons.Any(c => c.lessonId == id))
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
                do
                {
                    AnsiConsole.MarkupLine("[blue]Podaj nowy temat lekcji: [/]");
                    name = Console.ReadLine();
                    if(string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj poprawny temat[/]"); }
                } while (string.IsNullOrWhiteSpace(name));
                return name;
            }
            else if (i == 1)
            {
                string name;
                do
                {
                    AnsiConsole.MarkupLine("[blue]Podaj nowy opis lekcji: [/]");
                    name = Console.ReadLine();
                    if (name == null) { AnsiConsole.MarkupLine("[red]Podaj poprawny opis[/]"); }

                } while (string.IsNullOrWhiteSpace(name));
                return name;
            }
            else if (i == 2)
            {
                string name;
                int id;
                do
                {
                    AnsiConsole.MarkupLine("[blue]Podaj nowy numer lekcji: [/]");
                    name = Console.ReadLine();
                    id = int.Parse(name);
                    if (id <=0) { AnsiConsole.MarkupLine("[red]Podaj poprawny numer[/]"); }

                } while (id<=0);
                return name;
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

