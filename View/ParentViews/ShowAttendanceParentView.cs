using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.ParentViews
{
    public class ShowAttendanceParentView
    {
        MyDbContext db;
        public ShowAttendanceParentView(MyDbContext db)
        {
            this.db = db;
        }
        public void Show(Parent parent)
        {
            while (true)
            {
                // Fetch children of the parent
                var children = db.Students.Where(s => s.parentId == parent.parentId).ToList();
                if (!children.Any())
                {
                    AnsiConsole.MarkupLine("[red]Brak dzieci powiązanych z tym rodzicem.[/]");
                    return;
                }

                // List children for selection
                var childOptions = children.Select(c => $"{c.studentId}. {c.name} {c.surname}").ToArray();
                var selectedChildOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]Wybierz dziecko, aby zobaczyć obecność:[/]")
                        .PageSize(10)
                        .AddChoices(childOptions.Append("Cofnij"))
                );

                if (selectedChildOption == "Cofnij")
                {
                    Console.Clear();
                    return;
                }

                // Parse selected child ID
                int childId;
                try
                {
                    childId = int.Parse(selectedChildOption.Split(". ").First());
                }
                catch (FormatException)
                {
                    AnsiConsole.MarkupLine("[red]Nieprawidłowy format ID dziecka![/]");
                    continue;
                }

                var student = children.FirstOrDefault(c => c.studentId == childId);
                if (student == null) continue;

                // Fetch attendance data for the selected child
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

                AnsiConsole.MarkupLine("[grey]Naciśnij klawisz, aby wrócić...[/]");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
