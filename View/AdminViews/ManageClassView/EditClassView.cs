using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageClassView
{
    public class EditClassView
    {
        public EditClassView() { }
        public int ClassToEdit()
        {
            int id;
            AnsiConsole.WriteLine("[blue]Podaj idKlasy do edycji: [/]");
            string value = Console.ReadLine();
            id = int.Parse(value);
            return id;
        }
        public List<bool> ChooseOptionsToEdit()
        {
            List<bool> result = new List<bool> { false, false, false };
            var options = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("Wybierz opcje do edycji: ")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(new[] {
            "Numer klasy", "Litera klasy", "Wychowawca"}));

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = options.Contains(new[] { "Numer klasy", "Litera klasy", "Wychowawca"}[i]);
            }

            return result;
        }
        public string EditOption(int i)
        {
            if (i == 0)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy numer klasy: [/]");
                return Console.ReadLine();
            }
            else if (i == 1)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nową literę klasy: [/]");
                return Console.ReadLine();
            }
            else if (i == 2)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj id nowego wychowawcy: [/]");
                return Console.ReadLine();
            }
            return "";
        }
        public void ShowDifference(StudentClass classBeforChanges, StudentClass classAfterChanges)
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Numer"); table.AddColumn("Litera"); table.AddColumn("IdWychowawcy");

            table.AddRow(
                classBeforChanges.number,
                classBeforChanges.letter,
                classBeforChanges.teacherId.ToString()
            );

            table.AddRow(
                HighlightIfDifferent(classBeforChanges.number, classAfterChanges.number),
                HighlightIfDifferent(classBeforChanges.letter, classAfterChanges.letter),
                HighlightIfDifferent(classBeforChanges.teacherId.ToString(), classAfterChanges.teacherId.ToString())
            );
            AnsiConsole.Render(table);
        }
        string HighlightIfDifferent(string before, string after)
        {
            return before != after ? $"[red]{after}[/]" : after;
        }
    }
}
