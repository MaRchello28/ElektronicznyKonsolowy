using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageClassView
{
    public class DeleteClassView2
    {
        public int PutIndex()
        {
            string value;
            int id;
            bool run=true;
            do
            {
                AnsiConsole.Write("[red]Podaj index, który chcesz usunąć: [/]");
                value = Console.ReadLine();
                id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawny index[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.StudentClasses.Any(c => c.studentClassId == id))
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
