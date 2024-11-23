using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.TeacherViews
{
    public class ManageLessonsView
    {
        MyDbContext db;
        public ManageLessonsView(MyDbContext db) { this.db = db; }
        public string GetLessonName()
        {
            AnsiConsole.MarkupLine("[aqua]Podaj nazwę lekcji: (naciśnij Enter, żeby anulować)[/]");
            do
            {
                string lessonName = Console.ReadLine();
                if(lessonName == null || lessonName == "")
                {
                    AnsiConsole.MarkupLine("[yellow]Opuszczasz tworzenie lekcji.[/]");
                    return "";
                }
                else
                {
                    return lessonName;
                }
            } while (true);
            
        }
        public string AddLessonDescription()
        {
            AnsiConsole.MarkupLine("[aqua]Wprowadź opis lekcji: [/]");
            AnsiConsole.MarkupLine("(Jeżeli nie chcesz podawać wciśnij Enter bez wprowadzonych zmiennych)");
            string lessonDesc = Console.ReadLine();
            if(lessonDesc == null) { lessonDesc = ""; }
            return lessonDesc;
        }
        public int GenerateLessonNumber(int sessionId)
        {
            var maxLessonNumber = db.Lessons
                             .Where(l => l.sessionId == sessionId)
                             .Max(l => (int?)l.nuberOfLesson) ?? 0;

            return maxLessonNumber + 1;
        }
        public bool CheckAttendenceNow()
        {
            AnsiConsole.MarkupLine("[aqua]Czy chcesz od razu sprawdzić obecność?[/]");
            var options = new[] {
                "Tak", 
                "Nie"
            };
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Co chcesz wykonać?")
                    .PageSize(10)
                    .AddChoices(options));
            int index = Array.IndexOf(options, selectedOption);
            if(index == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void CheckAttendence(int selectedClass, Lesson getLesson)
        {
            string isPresent;
            var students = db.Students.Where(s => s.studentClassId == selectedClass).ToList();
            AnsiConsole.MarkupLine("[darkcyan]Sprawdzasz obecność: O-obecny, N-nieobecny, U-usprawiedliwiona nieobecność[/]");
            AnsiConsole.MarkupLine("[darkcyan](Naciśnij Enter bez wpisanych danych, żeby anulować sprawdzanie obecności)[/]");
            foreach ( var student in students)
            {
                AnsiConsole.MarkupLine("[aqua]Czy " + student.surname + " " + student.name + " jest obecny?[/]");

                do
                {
                    isPresent = Console.ReadLine();
                    if (isPresent == null || isPresent == "")
                    {
                        AnsiConsole.MarkupLine("[yellow]Przerwałeś sprawdzanie obecności[/]");
                    }
                    else
                    {
                        if (isPresent.ToUpper() != "O" && isPresent.ToUpper() != "N" && isPresent.ToUpper() != "U")
                        {
                            AnsiConsole.MarkupLine("[red]Nieprawidłowa wartość![/]");
                        }
                        else
                        {
                            db.Attendances.Add(new Attendance(isPresent.ToUpper(), student.studentId, getLesson.lessonId));
                            break;
                        }
                    }
                } while (true);
            }
            db.SaveChanges();
        }
        public int ShowExistingLessons(int sessionId, List<Lesson> lessons)
        {
            string[] options = new string[lessons.Count+1];
            options[lessons.Count] = "Powrót";
            int i = 0;
            foreach(var lesson in lessons)
            {
                options[i++] = "NrLekcji: " + lesson.lessonId + " Nazwa lekcji: " + lesson.name + " Data lekcji: " + lesson.date.ToString("dd-MM-yyyy");
            }
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz lekcje: ")
                    .PageSize(10)
                    .AddChoices(options));
            int index = Array.IndexOf(options, selectedOption);

            return lessons[index].nuberOfLesson;
        }
        public void ShowAttendanceOfThisLesson(int lessonNumber, int sessionId)
        {
            var lesson = db.Lessons.FirstOrDefault(l => l.nuberOfLesson == lessonNumber && l.sessionId == sessionId);
            if (lesson == null)
            {
                AnsiConsole.MarkupLine("[darkviolet_1]Żadne lekcje nie zostały jeszcze utworzone![/]");
                return;
            }
            var attendances = db.Attendances.Where(a => a.lessonId == lesson.lessonId).ToList();

            var studentIds = attendances.Select(a => a.studentId).ToList();
            var students = db.Students.Where(s => studentIds.Contains(s.studentId)).OrderBy(s => s.user.surname).ToList();

            var table = new Table();
            table.AddColumn("Nazwisko");
            table.AddColumn("Imie");
            table.AddColumn("Obecny");

            foreach (var student in students)
            {
                var attendance = attendances.FirstOrDefault(a => a.studentId == student.studentId);
                if(attendance != null)
                {
                    table.AddRow(student.surname, student.name, attendance.isPresent);
                }
                else
                {
                    table.AddRow(student.surname, student.name, "");
                }
            }

            AnsiConsole.Render(table);
        }
        public void EditAttendance(int lessonNumber, int sessionId)
        {
            var lesson = db.Lessons.FirstOrDefault(l => l.nuberOfLesson == lessonNumber && l.sessionId == sessionId);
            if (lesson == null)
            {
                AnsiConsole.MarkupLine("[darkviolet_1]Żadne lekcje nie zostały jeszcze utworzone![/]");
                return;
            }

            var attendances = db.Attendances.Where(a => a.lessonId == lesson.lessonId).ToList();
            var studentIds = attendances.Select(a => a.studentId).ToList();
            var students = db.Students.Where(s => studentIds.Contains(s.studentId)).OrderBy(s => s.user.surname).ToList();

            string isPresent;

            AnsiConsole.MarkupLine("[darkcyan]Poprawiasz obecność: O-obecny, N-nieobecny, U-usprawiedliwiona nieobecność Enter, żeby zostawić jak było[/]");

            foreach (var student in students)
            {
                var currentAttendance = attendances.FirstOrDefault(a => a.studentId == student.studentId);

                if (currentAttendance != null)
                {
                    AnsiConsole.MarkupLine("[aqua]Czy " + student.surname + " " + student.name + " jest obecny? (Aktualna obecność: " + currentAttendance.isPresent + ")[/]");

                    isPresent = Console.ReadLine().Trim();

                    if (string.IsNullOrEmpty(isPresent))
                    {
                        AnsiConsole.MarkupLine("[green]Nie zmieniono obecności dla " + student.surname + " " + student.name + "[/]");
                        continue;
                    }
                    if (isPresent != "O" && isPresent != "N" && isPresent != "U")
                    {
                        AnsiConsole.MarkupLine("[red]Nieprawidłowa wartość![/]");
                        continue;
                    }
                    currentAttendance.isPresent = isPresent;
                    db.Entry(currentAttendance).State = System.Data.Entity.EntityState.Modified;
                }
                else
                {
                    AnsiConsole.MarkupLine("[aqua]Czy " + student.surname + " " + student.name + " jest obecny?[/]");
                    isPresent = Console.ReadLine().Trim();

                    if (isPresent != "O" && isPresent != "N" && isPresent != "U")
                    {
                        AnsiConsole.MarkupLine("[red]Nieprawidłowa wartość![/]");
                        continue;
                    }
                    db.Attendances.Add(new Attendance(isPresent, student.studentId, lesson.lessonId));
                }
            }
            db.SaveChanges();
            AnsiConsole.MarkupLine("[green]Zmiany zostały zapisane![/]");
        }
    }
}
