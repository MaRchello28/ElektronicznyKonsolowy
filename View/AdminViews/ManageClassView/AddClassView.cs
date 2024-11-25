using ElektronicznyKonsolowy.Models;
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
            bool run=true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj id nauczyciela, który będzie wychowawcą: [/]");
                string value = Console.ReadLine();
                parentId = int.Parse(value);
                if (parentId == null) { AnsiConsole.MarkupLine("[red]Musisz podać id naczuczyciela[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.Parents.Any(c => c.parentId == parentId))
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
            while(run);
            return parentId;
        }
        public void ShowCreatedStudent(StudentClass sc)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonej klasy[/]");
            table.AddColumn("IdKlasy");
            table.AddColumn("Numer i Litera"); table.AddColumn("Wychowawca");
            string value = sc.number + sc.letter;
            table.AddRow(sc.studentClassId.ToString(), value, sc.teacherId.ToString());

            AnsiConsole.Render(table);
            AnsiConsole.MarkupLine("[grey]Naciśnij klawisz aby kontynuować...[/]");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
