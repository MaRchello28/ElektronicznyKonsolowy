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
            AnsiConsole.WriteLine("[blue]Podaj idRodzica do edycji: [/]");
            string value = Console.ReadLine();
            id = int.Parse(value);
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
                AnsiConsole.MarkupLine("[blue] Podaj nowe imię rodzica: [/]");
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
            else if (i == 3)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowe hasło rodzica: [/]");
                return Console.ReadLine();
            }
            else if (i == 4)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy Email rodzica: [/]");
                return Console.ReadLine();
            }
            else if (i == 5)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowe Numer Telefonu rodzica: [/]");
                return Console.ReadLine();
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
        }
        string HighlightIfDifferent(string before, string after)
        {
            return before != after ? $"[red]{after}[/]" : after;
        }
    }
}
