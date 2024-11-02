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
            AnsiConsole.Write("[red]Podaj index, który chcesz usunąć: [/]");
            string value = Console.ReadLine();
            int.TryParse(value, out int id); return id;
        }
    }
}
