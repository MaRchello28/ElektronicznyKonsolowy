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
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj nazwę lekcji: [/]");
                name = (Console.ReadLine());
                if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Nie zostawiaj pustego pola[/]"); }
            }
            while (run);
            return name;
        }
        public string EnterDescription()
        {
            string name;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj opis lekcji: [/]");
                name = (Console.ReadLine());
                if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Nie zostawiaj pustego pola[/]"); }
            }
            while (run);
            return name;
        }
        public int EnterNumberOfLesson()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj numer lekcji: [/]");
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
