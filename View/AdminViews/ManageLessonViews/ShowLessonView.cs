using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageLessonViews
{
    public class ShowLessonView
    {
        MyDbContext db;
        public ShowLessonView(MyDbContext db) { this.db = db; }
        public void Show()
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Id"); table.AddColumn("Numer lekcji"); table.AddColumn("Temat"); table.AddColumn("Opis");

            var lessons = db.Lessons.ToList();

            foreach (var lesson in lessons)
            {
                table.AddRow(lesson.lessonId.ToString(), lesson.nuberOfLesson.ToString(), lesson.name, lesson.description);
            }

            AnsiConsole.Render(table);
        }
    }
}
