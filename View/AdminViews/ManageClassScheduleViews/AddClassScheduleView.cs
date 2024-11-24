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

        public int EnterClassId()
        {
            int classId;
            bool run=true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj id klasy, która będzie posiadała plan zajęć: [/]");
                string value = Console.ReadLine();
                classId = int.Parse(value);
                if(classId <= 0) { AnsiConsole.MarkupLine("[red] Podaj porawne id klasy[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.StudentClasses.Any(c => c.studentClassId == classId))
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
            while(true);
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
    }
}
