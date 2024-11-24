using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageTeachersView
{
    public class DeleteTeacherView2
    {
        public DeleteTeacherView2() { }
        public int PutIndex()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj id do usunięcia: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.Teachers.Any(c => c.teacherId == id))
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
            while (run);
            return id;
        }
    }
}
