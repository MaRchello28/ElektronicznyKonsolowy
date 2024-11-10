using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.StudentViews
{
    public class ShowGradesStudentView
    {
        MyDbContext db;
        public ShowGradesStudentView(MyDbContext db) { this.db = db; }
        public void show(Student student)
        {
            var Grades = db.Grades.Where(g => g.studentId == student.studentId).ToList();
            var table = new Table();
            table.Border(TableBorder.Ascii);
            table.AddColumn("Wartość");
            table.AddColumn("Waga");
            table.AddColumn("Opis");
            table.AddColumn("Nauczyciel ID");
            table.AddColumn("Data");
            table.AddColumns("Id Sesji");
            bool useFirstColor = true;
            foreach (var grade in Grades)
            {
                var color = useFirstColor ? new Style(Color.Green) : new Style(Color.Purple);
                table.AddRow(
                    new Markup($"[green]{grade.value}[/]", color),
                    new Markup($"{grade.wage}", color),
                    new Markup($"{grade.description}", color),
                    new Markup($"{grade.teacherId}", color),
                    new Markup($"{grade.time}", color),
                    new Markup($"{grade.sessionId}",color)
                );

                // Alternate the color for the next row
                useFirstColor = !useFirstColor;
            }
            AnsiConsole.Write(table);
        }
    }
}
