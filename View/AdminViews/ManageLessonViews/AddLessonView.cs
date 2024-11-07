using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageLessonViews
{
    public class AddLessonView
    {
        MyDbContext db;
        public AddLessonView(MyDbContext db) { this.db = db; }
        public void ShowCreateWindow()
        {
            var header = new FigletText("Okno tworzenia Lekcji").Centered().Color(Color.Green3);
            AnsiConsole.Render(header);
        }
        public string EnterName()
        {
            string name;
            AnsiConsole.MarkupLine("[blue] Podaj nazwę lekcji: [/]");
            return Console.ReadLine();
        }
        public string EnterDescription()
        {
            string description;
            AnsiConsole.MarkupLine("[blue] Podaj opis lekcji: [/]");
            return Console.ReadLine();
        }
        public int EnterNumberOfLesson()
        {
            int number;
            AnsiConsole.MarkupLine("[blue] Podaj numer lekcji: [/]");
            string value = Console.ReadLine();
            number = int.Parse(value);
            return number;
        }
        public void ShowCreatedLesson(string name, string description, int number)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonej lekcji[/]");
            table.AddColumn("IdLekcji");
            table.AddColumn("Numer"); table.AddColumn("Temat"); table.AddColumn("Opis");
            table.AddRow("", number.ToString(), name, description);

            AnsiConsole.Render(table);
        }
    }
}
