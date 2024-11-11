using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.ParentViews
{
    public class ShowGradesParentView
    {
        MyDbContext db;
        public ShowGradesParentView(MyDbContext db)
        {
            this.db = db;
        }
        public void Show(Parent parent)
        {
            int id = parent.parentId;

            Console.Write("Wybierz ID dziecka, aby zobaczyć plan lekcji: ");
            if (int.TryParse(Console.ReadLine(), out int selectedChildId))
            {
                var selectedChild = parent.children
                    .FirstOrDefault(c => c.studentId == selectedChildId);

                if (selectedChild == null)
                {
                    Console.WriteLine("Nie znaleziono dziecka o podanym ID.");
                    return;
                }
                var grades = db.Grades.Where(g => g.studentId == selectedChild.studentId).ToList();

                if (!grades.Any())
                {
                    Console.WriteLine("Brak ocen dla tego dziecka.");
                    return;
                }

                // Create a Spectre.Console table
                var table = new Table();
                table.Border(TableBorder.Ascii);
                table.AddColumn("Wartość");
                table.AddColumn("Waga");
                table.AddColumn("Opis");
                table.AddColumn("Nauczyciel ID");
                table.AddColumn("Data");
                table.AddColumns("Id sesji");
                bool useFirstColor = true;
                foreach (var grade in grades)
                {
                    var color = useFirstColor ? new Style(Color.Green) : new Style(Color.Purple);

                    table.AddRow(
                        new Markup($"[green]{grade.value}[/]", color),
                        new Markup($"{grade.wage}", color),
                        new Markup($"{grade.description}", color),
                        new Markup($"{grade.teacherId}", color),
                        new Markup($"{grade.time}", color),
                        new Markup($"{grade.sessionId}", color)
                    );

                    useFirstColor = !useFirstColor;
                }

                AnsiConsole.Write(table);
            }
        }
    }
}
