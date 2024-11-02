using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageClassView
{
    public class AddClassView
    {
        public AddClassView() { }
        public void ShowCreateWindow()
        {
            var header = new FigletText("Okno tworzenia klasy").Centered().Color(Color.Green3);
            AnsiConsole.Render(header);
        }
        public string EnterNumber()
        {
            string name;
            AnsiConsole.MarkupLine("[blue] Podaj numer klasy: [/]");
            return Console.ReadLine();
        }
        public string EnterLetter()
        {
            string surname;
            AnsiConsole.MarkupLine("[blue] Podaj literę klasy: [/]");
            return Console.ReadLine();
        }
        public int EnterTeacherId()
        {
            int parentId;
            AnsiConsole.MarkupLine("[blue] Podaj id nauczyciela, który będzie wychowawcą: [/]");
            string value = Console.ReadLine();
            parentId = int.Parse(value);
            return parentId;
        }
        public void ShowCreatedStudent(string number, string letter, int teacherId)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonej klasy[/]");
            table.AddColumn("IdKlasy");
            table.AddColumn("Numer i Litera"); table.AddColumn("Wychowawca");
            string value = number + letter;
            table.AddRow("", value, teacherId.ToString());

            AnsiConsole.Render(table);
        }
    }
}
