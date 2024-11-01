using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews
{
    public class DeleteStudentView2
    {
        MyDbContext db;
        public DeleteStudentView2(MyDbContext db) { this.db = db; }
        public int PutIndex()
        {
            AnsiConsole.Write("[red]Podaj index, który chcesz usunąć: [/]");
            string value = Console.ReadLine();
            int.TryParse(value, out int id); return id;
        }
    }
}
