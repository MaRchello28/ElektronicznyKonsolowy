using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageSubjectViews
{
    public class AddSubjectView
    {
        MyDbContext db;
        public AddSubjectView(MyDbContext db) { this.db = db; }
        public string EnterName()
        {
            string name;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj nazwę przedmiotu: [/]");
                name = (Console.ReadLine());
                if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj poprawną nazwę[/]"); }
            }
            while (run);
            return name;
        }
        public void ShowCreatedSubject(string name)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonego przedmiotu[/]");
            table.AddColumn("IdPrzedmiotu");
            table.AddColumn("Nazwa");

            table.AddRow("", name);

            AnsiConsole.Render(table);
            AnsiConsole.MarkupLine("[grey]Naciśnij klawisz aby kontynuować...[/]");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
