using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.TeacherViews
{
    public class ShowAttendanceWholeClassView
    {
        MyDbContext db;
        public ShowAttendanceWholeClassView(MyDbContext db)
        {
            this.db = db;
        }
        public void Run(int selectedSession)
        {
            var lessons = db.Lessons
                            .Where(l => l.sessionId == selectedSession)
                            .OrderBy(l => l.nuberOfLesson)
                            .ToList();

            if (!lessons.Any())
            {
                AnsiConsole.MarkupLine("[red]Nie znaleziono lekcji dla wybranej sesji![/]");

                AnsiConsole.MarkupLine("[purple]Naciśnij dowolny przycisk, żeby wyjść[/]");
                Console.ReadKey();
                Console.Clear();

                return;
            }

            var lessonIds = lessons.Select(l => l.lessonId).ToList();
            var attendances = db.Attendances
                                 .Where(a => lessonIds.Contains(a.lessonId))
                                 .ToList();

            var studentIds = attendances.Select(a => a.studentId).Distinct().ToList();
            var students = db.Students
                              .Where(s => studentIds.Contains(s.studentId))
                              .OrderBy(s => s.user.surname)
                              .ToList();

            var table = new Table();
            table.AddColumn("Nazwisko");
            table.AddColumn("Imię");

            foreach (var lesson in lessons)
            {
                table.AddColumn($"{lesson.nuberOfLesson}");
            }

            foreach (var student in students)
            {
                var row = new List<string>
        {
            student.user.surname,
            student.user.name
        };

                foreach (var lesson in lessons)
                {
                    var attendance = attendances
                                     .FirstOrDefault(a => a.studentId == student.studentId && a.lessonId == lesson.lessonId);

                    if (attendance == null)
                    {
                        row.Add("[grey]-[/]");
                    }
                    else
                    {
                        string statusMarkup = attendance.isPresent switch
                        {
                            "O" => "[white]O[/]",  // Obecny na biało
                            "N" => "[red]N[/]",    // Nieobecny na czerwono
                            "U" => "[blue]U[/]",   // Usprawiedliwiony na niebiesko
                            _ => "[grey]-[/]"      // Dla nieznanych statusów
                        };
                        row.Add(statusMarkup);
                    }
                }

                table.AddRow(row.ToArray());
            }

            AnsiConsole.Write(table);

            AnsiConsole.MarkupLine("[purple]Naciśnij dowolny przycisk, żeby wyjść[/]");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
