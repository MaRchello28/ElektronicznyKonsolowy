using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews
{
    public class EditParentView
    {
        public EditParentView() { }
        public int ParentToEdit()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj id do edycji: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
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
            .Title("[green]Wybierz opcje do edycji: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(new[] {
            "Imie", "Nazwisko", "Login", "Haslo", "Email", "Numer Telefonu"}));

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = options.Contains(new[] { "Imie", "Nazwisko", "Login", "Haslo", "Email", "Numer Telefonu" }[i]);
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
                    AnsiConsole.MarkupLine("[blue]Podaj nowe imię rodzica: [/]");
                    name = Console.ReadLine();
                    if(string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj Imie[/]"); }
                } while (string.IsNullOrWhiteSpace(name));
                return name;
            }
            else if (i == 1)
            {
                string name;
                do
                {
                    AnsiConsole.MarkupLine("[blue]Podaj nowe nazwisko rodzica: [/]");
                    name = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj Nazwisko[/]"); }

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
            else if (i == 5)
            {
                string name; bool run = true;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowy Nr Telefonu: [/]");
                    name = Console.ReadLine();
                    if (name == null || name == "") { AnsiConsole.MarkupLine("[red]Nie podano wartości![/]"); }
                    else if (name.Length != 9) { AnsiConsole.MarkupLine("[red]To nie jest numer telefonu![/]"); }
                    else { run = false; }
                }
                while (run);
                return name;
            }

            return "";
        }
        public void ShowDifference(Parent parentBeforChanges, Parent parentAfterChanges)
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Imie"); table.AddColumn("Nazwisko"); table.AddColumn("Login"); table.AddColumn("Haslo"); table.AddColumn("Email");
            table.AddColumn("Numer Telefonu");

            table.AddRow(
                parentBeforChanges.name,
                parentBeforChanges.surname,
                parentBeforChanges.login,
                parentBeforChanges.password,
                parentBeforChanges.email,
                parentBeforChanges.phoneNumber
            );

            table.AddRow(
                HighlightIfDifferent(parentBeforChanges.name, parentAfterChanges.name),
                HighlightIfDifferent(parentBeforChanges.surname, parentAfterChanges.surname),
                HighlightIfDifferent(parentBeforChanges.login, parentAfterChanges.login),
                HighlightIfDifferent(parentBeforChanges.password, parentAfterChanges.password),
                HighlightIfDifferent(parentBeforChanges.email, parentAfterChanges.email),
                HighlightIfDifferent(parentBeforChanges.phoneNumber, parentAfterChanges.phoneNumber)
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
