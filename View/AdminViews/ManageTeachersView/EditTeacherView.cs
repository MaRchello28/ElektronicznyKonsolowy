using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageTeachersView
{
    public class EditTeacherView
    {
        public EditTeacherView() { }
        public int StudentToEdit()
        {
            int id;
            do
            {
                AnsiConsole.WriteLine("[blue]Podaj idNauczyciela do Edycji: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if(id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
            }
            while(id <= 0);

            return id;
        }
        public List<bool> ChooseOptionsToEdit()
        {
            List<bool> result = new List<bool> { false, false, false, false, false};
            var options = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("Wybierz opcje do edycji: ")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(new[] {
            "Imie", "Nazwisko", "Login", "Haslo", "Email"}));

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = options.Contains(new[] { "Imie", "Nazwisko", "Login", "Haslo", "Email"}[i]);
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
                    AnsiConsole.MarkupLine("[blue]Podaj nowe imię: [/]");
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
                    AnsiConsole.MarkupLine("[blue]Podaj nowe nazwisko: [/]");
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
                string name; bool run = true;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowy email: [/]");
                    name = Console.ReadLine();
                    if (name == null || name == "") { AnsiConsole.MarkupLine("[red]Nie podano wartości![/]"); }
                    else if (!(name.Contains('@') && name.Contains('.'))) { AnsiConsole.MarkupLine("[red]To nie jest adres email![/]"); }
                    else { run = false; }
                }
                while (run);
                return name;
            }
            return "";
        }
        public void ShowDifference(Teacher teacherBeforChanges, Teacher teacherAfterChanges)
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Imie"); table.AddColumn("Nazwisko"); table.AddColumn("Login"); table.AddColumn("Haslo"); table.AddColumn("Email");

            table.AddRow(
                teacherBeforChanges.user.name,
                teacherBeforChanges.user.surname,
                teacherBeforChanges.user.login,
                teacherBeforChanges.user.password,
                teacherBeforChanges.email
            );

            table.AddRow(
                HighlightIfDifferent(teacherBeforChanges.user.name, teacherAfterChanges.user.name),
                HighlightIfDifferent(teacherBeforChanges.user.surname, teacherAfterChanges.user.surname),
                HighlightIfDifferent(teacherBeforChanges.user.login, teacherAfterChanges.user.login),
                HighlightIfDifferent(teacherBeforChanges.user.password, teacherAfterChanges.user.password),
                HighlightIfDifferent(teacherBeforChanges.email, teacherAfterChanges.email)
            );
            AnsiConsole.Render(table);
        }
        string HighlightIfDifferent(string before, string after)
        {
            return before != after ? $"[red]{after}[/]" : after;
        }
    }
}
