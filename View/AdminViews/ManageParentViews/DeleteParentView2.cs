using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews
{
    public class DeleteParentView2
    {
        MyDbContext db;
        public DeleteParentView2(MyDbContext db) { this.db = db; }
        public int PutIndex()
        {
            string value;
            int id;
            do
            {
                AnsiConsole.Write("[red]Podaj index, który chcesz usunąć: [/]");
                value = Console.ReadLine();
                id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawny index[/]"); }
            }
            while (id <= 0);
            return id;
        }
    }
}
