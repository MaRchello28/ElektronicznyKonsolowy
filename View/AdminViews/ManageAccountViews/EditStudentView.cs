using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews
{
    public class EditStudentView
    {
        public EditStudentView() { }
        public int StudentToEdit()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj id ucznia do edycji: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.Students.Any(c => c.studentId == id))
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
            List<bool> result = new List<bool> { false, false, false, false, false, false };
            var options = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("Wybierz opcje do edycji: ")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(new[] {
            "Imie", "Nazwisko", "Login", "Haslo", "IdKlasy", "IdRodzica"}));

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = options.Contains(new[] {"Imie", "Nazwisko", "Login", "Haslo", "IdKlasy", "IdRodzica" }[i]);
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
                    AnsiConsole.MarkupLine("[blue]Podaj nowe imię ucznia: [/]");
                    name = Console.ReadLine();
                    if (name == null) { AnsiConsole.MarkupLine("[red]Podaj poprawne imie[/]"); }

                } while (string.IsNullOrWhiteSpace(name));
                return name;
            }
            else if (i == 1)
            {
                string name;
                do
                {
                    AnsiConsole.MarkupLine("[blue]Podaj nowe nazwisko ucznia: [/]");
                    name = Console.ReadLine();
                    if (name == null) { AnsiConsole.MarkupLine("[red]Podaj poprawne nazwisko[/]"); }

                } while (string.IsNullOrWhiteSpace(name));
                return name;
            }
            else if (i == 2)
            {
                string name;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowy login: [/]");
                    name = Console.ReadLine();
                    if (name.Length <= 3) { AnsiConsole.MarkupLine("[red]Login nie może być krótszy niż 4 znaki[/]"); }
                }
                while (name.Length <= 3);
                return name;
            }
            else if (i == 3)
            {
                string name;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowe hasło: [/]");
                    name = Console.ReadLine();
                    if (name.Length <= 6) { AnsiConsole.MarkupLine("[red]Hasło nie może być krótsze niż 6 znaków[/]"); }
                }
                while (name.Length <= 6);
                return name;
            }
            else if (i == 4)
            {
                string name;
                int id;
                bool run = true;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowe id klasy: [/]");
                    name = (Console.ReadLine());
                    id = int.Parse(name);
                    if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                    else
                    {
                        using (var context = new MyDbContext())
                        {
                            if (!context.StudentClasses.Any(c => c.studentClassId == id))
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
                return name;
            }
            else if (i == 5)
            {
                string name;
                int id;
                bool run=true;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowe id rodzica: [/]");
                    name = (Console.ReadLine());
                    id = int.Parse(name);
                    if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                    else
                    {
                        using (var context = new MyDbContext())
                        {
                            if (!context.Parents.Any(c => c.parentId == id))
                            {
                                AnsiConsole.MarkupLine("[red]Podane id nie jest w bazie[/]");
                            }
                            else
                            {
                                run = false;
                            }
                        }

                }    }
                while (run);
                return name;
            }

            return "";
        }
        public void ShowDifference(Student studentBeforChanges, Student studentAfterChanges)
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Imie"); table.AddColumn("Nazwisko"); table.AddColumn("Login"); table.AddColumn("Haslo"); table.AddColumn("IdKlasy");
            table.AddColumn("IdRodzica");

            table.AddRow(
                studentBeforChanges.name,
                studentBeforChanges.surname,
                studentBeforChanges.login,
                studentBeforChanges.password,
                studentBeforChanges.studentClassId.ToString(),
                studentBeforChanges.parentId.ToString()
            );

            table.AddRow(
                HighlightIfDifferent(studentBeforChanges.name, studentAfterChanges.name),
                HighlightIfDifferent(studentBeforChanges.surname, studentAfterChanges.surname),
                HighlightIfDifferent(studentBeforChanges.login, studentAfterChanges.login),
                HighlightIfDifferent(studentBeforChanges.password, studentAfterChanges.password),
                HighlightIfDifferent(studentBeforChanges.studentClassId.ToString(), studentAfterChanges.studentClassId.ToString()),
                HighlightIfDifferent(studentBeforChanges.parentId.ToString(), studentAfterChanges.parentId.ToString())
            );
            AnsiConsole.Render(table);
            AnsiConsole.MarkupLine("[grey]Naciśnij klawisz aby kontynuować...[/]");
            Console.ReadKey();
            Console.Clear();
        }
        string HighlightIfDifferent(string before, string after)
        {
            return before != after ? $"[red]{after}[/]" : after;
        }
    }
}
