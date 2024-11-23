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
            do
            {
                AnsiConsole.Write("[red]Podaj index, który chcesz usunąć: [/]");
                value = Console.ReadLine();
                id = int.Parse(value);
                if (id <=0) { AnsiConsole.MarkupLine("[red]Podaj poprawny index[/]"); }
            }
            while (id<=0);
            return id;
        }
    }
}
