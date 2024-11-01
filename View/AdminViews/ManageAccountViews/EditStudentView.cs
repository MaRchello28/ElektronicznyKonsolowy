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
            AnsiConsole.WriteLine("[blue]Podaj idUcznia do edycji: [/]");
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
                AnsiConsole.MarkupLine("[blue] Podaj nowe imię ucznia: [/]");
                return Console.ReadLine();
            }
            else if (i==1)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowe nazwisko ucznia: [/]");
                return Console.ReadLine();
            }
            else if(i==2)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy login ucznia: [/]");
                return Console.ReadLine();
            }
            else if(i==3)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowe hasło ucznia: [/]");
                return Console.ReadLine();
            }
            else if(i==4)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowe IdKlasy ucznia: [/]");
                return Console.ReadLine();
            }
            else if(i==5)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowe IdRodzica ucznia: [/]");
                return Console.ReadLine();
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
        }
        string HighlightIfDifferent(string before, string after)
        {
            return before != after ? $"[red]{after}[/]" : after;
        }
    }
}
