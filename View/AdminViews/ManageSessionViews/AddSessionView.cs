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
        public void ShowCreateWindow()
        {
            var header = new FigletText("Okno tworzenia Sesji").Centered().Color(Color.Green3);
            AnsiConsole.Render(header);
        }
        public int EnterSubject()
        {
            AnsiConsole.MarkupLine("[blue] Podaj Id Subject: [/]");
            string value = Console.ReadLine();
            int subject;
            subject = int.Parse(value);
            return subject;
        }
        
        public int EnterTeacher()
        {
            AnsiConsole.MarkupLine("[blue] Podaj Id nauczyciela: [/]");
            string value = Console.ReadLine();
            int teacherId;
            teacherId = int.Parse(value);
            return teacherId;
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
        public void ShowCreatedSession(int subject, int teacher, int dzien, TimeSpan hourfrom, TimeSpan hourstop)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonego rodzica[/]");
            table.AddColumn("IdSesji");
            table.AddColumn("SubjectId"); table.AddColumn("Nauczyciel"); table.AddColumn("Dzień tygodnia"); table.AddColumn("Godzina od");
            table.AddColumn("Godzina do");

            table.AddRow("", subject.ToString(), teacher.ToString(), ((DayOfWeek)dzien).ToString(), hourfrom.ToString(), hourstop.ToString());

            AnsiConsole.Render(table);
        }
    }
}
