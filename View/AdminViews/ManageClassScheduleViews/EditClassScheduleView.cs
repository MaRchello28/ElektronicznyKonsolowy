using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews
{
    public class EditClassScheduleView
    {
        public EditClassScheduleView() { }
        public int ClassScheduleToEdit()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.Markup("[red]Podaj id do edycji: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawny index[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.ClassSchedules.Any(c => c.classScheduleId == id))
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
            List<bool> result = new List<bool> { false, false, false};
            var options = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[green]Wybierz opcje do edycji: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(new[] {
            "ClassId", "Dodaj zajęcia", "Usuń zajęcia"}));

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = options.Contains(new[] { "ClassId", "Dodaj zajęcia", "Usuń zajęcia" }[i]);
            }

            return result;
        }
        public int EditOption(int i)
        {
            if (i == 0)
            {
                int id;
                bool run = true;
                do
                {
                    AnsiConsole.Markup("[red]Podaj nowe id Klasy [/]");
                    string value = Console.ReadLine();
                    id = int.Parse(value);
                    if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawny index[/]"); }
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
                return id;
            }
            else if (i == 1)
            {
                int id;
                bool run = true;
                do
                {
                    AnsiConsole.Markup("[red]Podaj id sesji do dodania [/]");
                    string value = Console.ReadLine();
                    id = int.Parse(value);
                    if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawny index[/]"); }
                    else
                    {
                        using (var context = new MyDbContext())
                        {
                            if (!context.Sessions.Any(c => c.sessionId == id))
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
            else if (i == 2)
            {
                int id;
                bool run = true;
                do
                {
                    AnsiConsole.Markup("[red]Podaj sesji do usunięcia z planu lekcji [/]");
                    string value = Console.ReadLine();
                    id = int.Parse(value);
                    if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawny index[/]"); }
                    else
                    {
                        using (var context = new MyDbContext())
                        {
                            if (!context.Sessions.Any(c => c.sessionId == id))
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
            return -1;
        }
    }
}
