using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageSessionViews
{
    public class AddSessionView
    {
        MyDbContext db;
        public AddSessionView(MyDbContext db) { this.db = db; }
        public int EnterSubject()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj id subject: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.Subjects.Any(c => c.subjectId == id))
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
        
        public int EnterTeacher()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj id nauczyciela: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
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
            return id;
        }
        public int EnterDzien()
        {
            var options = new[] {
            "Poniedziałek",
            "Wtorek",
            "Środa",
            "Czwartek",
            "Piątek"
            };
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Zaznacz dzień tygodnia:")
                    .PageSize(10)
                    .AddChoices(options));

            return Array.IndexOf(options, selectedOption);
        }

        public int EnterHourFrom()
        {
            var options = new[] {
            "8:00",
            "8:55",
            "9:50",
            "10:55",
            "11:50",
            "12:45",
            "13:40",
            "14:35",
            "15:30",
            "16:25",
            "17:20"
            };
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz godzinę startu?")
                    .PageSize(10)
                    .AddChoices(options));

            return Array.IndexOf(options, selectedOption);
        }
        public int EnterSala()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj sale do zajęć: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawny numer[/]"); }
                else
                {
                    run = false;
                }

            }
            while (run);
            return id;
        }
        public void ShowCreatedSession(Session ses)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonego rodzica[/]");
            table.AddColumn("IdSesji");
            table.AddColumn("SubjectId"); table.AddColumn("Nauczyciel");table.AddColumn("Numer sali"); table.AddColumn("Dzień tygodnia"); table.AddColumn("Godzina od");
            table.AddColumn("Godzina do");

            table.AddRow(ses.sessionId.ToString(), ses.subjectId.ToString(), ses.teacherId.ToString(),ses.sala.ToString(), ((DayOfWeek)ses.dayOfTheWeek).ToString(), ses.hourFrom.ToString(), ses.hourTo.ToString());

            AnsiConsole.Render(table);
            AnsiConsole.MarkupLine("[grey]Naciśnij klawisz aby kontynuować...[/]");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
