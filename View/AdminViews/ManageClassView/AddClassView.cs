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
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj numer klasy: [/]");
                name = Console.ReadLine();
                if (name.Length<1) { AnsiConsole.MarkupLine("[red]Musisz podać numer klasy[/]"); }
            }
            while(name.Length<1);
            return Console.ReadLine();
        }
        public string EnterLetter()
        {
            string name;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj literę klasy: [/]");
                name = Console.ReadLine();
                if (name.Length > 1) { AnsiConsole.MarkupLine("[red]Musisz podać litere klasy[/]"); }
            }
            while(name.Length<1);
            return Console.ReadLine();
        }
        public int EnterTeacherId()
        {
            int parentId;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj id nauczyciela, który będzie wychowawcą: [/]");
                string value = Console.ReadLine();
                parentId = int.Parse(value);
                if(parentId==null) { AnsiConsole.MarkupLine("[red]Musisz podać id naczuczyciela[/]"); }
            }
            while(parentId==null);
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
