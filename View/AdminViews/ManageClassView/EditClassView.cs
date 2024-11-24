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
            bool run=true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj idKlasy do edycji: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if(id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne idKlasy[/]"); }
                else
                {
                    using(var context = new MyDbContext())
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
            while(run);
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
                int id;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowy numer klasy: [/]");
                    name=(Console.ReadLine());
                    id= int.Parse(name);
                    if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawny numer[/]"); }
                }
                while (id <= 0);
                return name;
            }
            else if (i == 1)
            {
                string name;
                bool run=true;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nową literę klasy: [/]");
                    name = (Console.ReadLine());
                    if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj poprawną literę[/]"); }
                }
                while (run);
                return name;
            }
            else if (i == 2)
            {
                string name;
                int id;
                bool run=true;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowe id wychowawcy: [/]");
                    name = (Console.ReadLine());
                    id = int.Parse(name);
                    if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                    else
                    {
                        using (var context = new MyDbContext())
                        {
                            if (!context.Teachers.Any(c => c.teacherId == id))
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
