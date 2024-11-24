using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews
{
    public class ShowStudentsView
    {
        MyDbContext db;
        public ShowStudentsView(MyDbContext db) { this.db = db; }
        public void Show()
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Id"); table.AddColumn("ClassId"); table.AddColumn("ParentId"); table.AddColumn("Name"); table.AddColumn("Surname");
            table.AddColumn("Login"); table.AddColumn("Password");

            var students = db.Students.ToList();

            foreach (var student in students)
            {
                table.AddRow(student.studentId.ToString(),student.studentClassId.ToString(),student.studentClassId.ToString(),student.name, student.surname, student.login, student.password);
            }

            AnsiConsole.Render(table);
            AnsiConsole.MarkupLine("[grey]Naciśnij klawisz aby kontynuować...[/]");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
