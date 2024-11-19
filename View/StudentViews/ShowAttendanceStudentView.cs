using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.StudentViews
{
    public class ShowAttendanceStudentView
    {
        MyDbContext db;
        public ShowAttendanceStudentView(MyDbContext db)
        {
            this.db = db;
        }
        public void Show(Student student)
        {
            Console.Clear();
            while (true)
            {
                var subjects = db.Subjects.ToList();
                var sessions = db.Sessions.ToList();
                var lessons = db.Lessons.Where(l => l.attendances.Any(a => a.studentId == student.studentId)).ToList();

                var lessonNumbers = lessons.Select(l => l.nuberOfLesson).Distinct().OrderBy(n => n).ToList();

                var table = new Table().AddColumn("Przedmiot");
                foreach (var number in lessonNumbers)
                {
                    table.AddColumn($"Lekcja {number}");
                }

                foreach (var subject in subjects)
                {
                    var subjectSessions = sessions.Where(s => s.subjectId == subject.subjectId).ToList();
                    var subjectLessons = lessons.Where(l => subjectSessions.Any(s => s.sessionId == l.sessionId)).ToList();

                    var row = new List<string> { subject.name };

                    foreach (var number in lessonNumbers)
                    {
                        var lesson = subjectLessons.FirstOrDefault(l => l.nuberOfLesson == number);
                        var attendance = lesson?.attendances.FirstOrDefault(a => a.studentId == student.studentId);
                        string attendanceStatus = attendance?.isPresent switch
                        {
                            "O" => "[green]O[/]",
                            "N" => "[red]N[/]",
                            "U" => "[blue]U[/]",
                            _ => "-"
                        };
                        row.Add(attendanceStatus);
                    }

                    table.AddRow(row.ToArray());
                }

                AnsiConsole.Write(table);

                // Option to go back
                AnsiConsole.MarkupLine("[grey]Naciśnij klawisz, aby wrócić...[/]");
                Console.ReadKey();
                Console.Clear();
                return;
            }
        }
    }
}
