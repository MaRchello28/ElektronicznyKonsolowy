using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageTeachersView
{
    public class ShowTeachersView
    {
        MyDbContext db;
        public ShowTeachersView(MyDbContext db) { this.db = db; }
        public void Show()
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Id"); table.AddColumn("Imie"); table.AddColumn("Nazwisko"); table.AddColumn("Login"); table.AddColumn("Haslo");
            table.AddColumn("Email");

            var teachers = db.Teachers.ToList();

            foreach (var teacher in teachers)
            { 
                table.AddRow(teacher.teacherId.ToString(), teacher.user.name, teacher.user.surname, teacher.user.login, teacher.user.password, teacher.email);
            }

            AnsiConsole.Render(table);
        }
    }
}
