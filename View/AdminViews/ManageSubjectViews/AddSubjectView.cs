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
        public void ShowCreateWindow()
        {
            var header = new FigletText("Okno tworzenia przedmiotu").Centered().Color(Color.Green3);
            AnsiConsole.Render(header);
        }
        public string EnterName()
        {
            string name;
            AnsiConsole.MarkupLine("[blue] Podaj nazwe przedmiotu: [/]");
            return Console.ReadLine();
        }
        public void ShowCreatedSubject(string name)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonego przedmiotu[/]");
            table.AddColumn("IdPrzedmiotu");
            table.AddColumn("Nazwa");

            table.AddRow("", name);

            AnsiConsole.Render(table);
        }
    }
}
