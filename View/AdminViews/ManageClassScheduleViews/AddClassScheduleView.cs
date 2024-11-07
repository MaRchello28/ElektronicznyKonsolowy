using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews
{
    public class AddClassScheduleView
    {
        MyDbContext db;
        public AddClassScheduleView(MyDbContext db) { this.db = db; }
        public void ShowCreateWindow()
        {
            var header = new FigletText("Okno tworzenia klasy").Centered().Color(Color.Green3);
            AnsiConsole.Render(header);
        }
        public int EnterClassId()
        {
            int classId;
            AnsiConsole.MarkupLine("[blue] Podaj id klasy, która będzie posiadała plan zajęć: [/]");
            string value = Console.ReadLine();
            classId = int.Parse(value);
            return classId;
        }
        public void ShowCreatedClassSchedule(int classId)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonej klasy[/]");
            table.AddColumn("IdPanuZajęć");
            table.AddColumn("IdKlasy");
            table.AddRow("",classId.ToString());

            AnsiConsole.Render(table);
        }
        public int AddSessions()
        {
            var options = new[] {
            "Tak",
            "Nie",
            };
            var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("Czy chcesz odrazu dodać lekcje do planu? : ")
            .PageSize(10)
            .AddChoices(options));

            return Array.IndexOf(options, selectedOption);
        }
    }
}
