using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews
{
    public class ShowClassScheduleView
    {
        MyDbContext db;
        public ShowClassScheduleView(MyDbContext db) { this.db = db; }
        public void Show(int opt)
        {
            if(opt == 0)
            {
                var table = new Table();
                table.Border(TableBorder.HeavyEdge);
                table.AddColumn("Id");table.AddColumn("Id Klasy");

                var classes = db.ClassSchedules.ToList();

                foreach (var cla in classes)
                {
                    table.AddRow(cla.classScheduleId.ToString(), cla.studentClassId.ToString());
                }
                AnsiConsole.Render(table);
            }
            else
            {
                int id;
                AnsiConsole.MarkupLine("[blue] Podaj id Planu lekcji: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                var CLA = db.ClassSchedules.FirstOrDefault(x => x.classScheduleId == id);
                if (CLA == null)
                {
                    AnsiConsole.MarkupLine("[red]Nie znaleziono planu lekcji![/]");
                    return;
                }
                var table2 = new Table();
                table2.Border(TableBorder.HeavyEdge);
                table2.AddColumn("Godzina");table2.AddColumn("Poniedziałek");table2.AddColumn("Wtorek");table2.AddColumn("Środa");table2.AddColumn("Czwartek");table2.AddColumn("Piątek");
                var lessonTimes = new List<string>
                {
                    "8:00 - 8:45", "8:55 - 9:40", "9:50 - 10:35",
                    "10:55 - 11:40", "11:50 - 12:35", "12:45 - 13:30",
                    "13:40 - 14:25", "14:35 - 15:20"
                };

                foreach (var time in lessonTimes)
                {
                    table2.AddRow(time, "", "", "", "", "");
                }

                foreach (var session in CLA.sessions)
                {
                    int rowIndex = lessonTimes.FindIndex(time =>
                    {
                        var parts = time.Split(" - ");
                        var startTime = TimeSpan.Parse(parts[0]);
                        return startTime == session.hourFrom;
                    });

                    if (rowIndex != -1)
                    {
                        int columnIndex = session.dayOfTheWeek switch
                        {
                            DayOfWeek.Monday => 1,
                            DayOfWeek.Tuesday => 2,
                            DayOfWeek.Wednesday => 3,
                            DayOfWeek.Thursday => 4,
                            DayOfWeek.Friday => 5,
                            _ => -1
                        };

                        if (columnIndex != -1)
                        {
                            string subjectName = GetSubjectNameById(session.subjectId); 
                            string teacherName = GetTeacherNameById(session.teacherId); 
                            string teacherSurname = GetTeacherSurnameById(session.teacherId);
                            table2.UpdateCell(rowIndex, columnIndex, $"{subjectName} ({teacherName}{teacherSurname })");
                        }
                    }
                    AnsiConsole.Write(table2);
                }
                string GetSubjectNameById(int subjectId)
                {
                    var subject = db.Subjects.FirstOrDefault(s => s.subjectId == subjectId);
                    return subject != null ? subject.name : "Nieznany przedmiot";
                }

                string GetTeacherNameById(int teacherId)
                {
                    var teacher = db.Teachers.FirstOrDefault(t => t.teacherId == teacherId);
                    return teacher != null ? teacher.user.name : "Nieznany nauczyciel";
                }
                string GetTeacherSurnameById(int teacherId)
                {
                    var teacher = db.Teachers.FirstOrDefault(t => t.teacherId == teacherId);
                    return teacher != null ? teacher.user.surname : "Nieznany nauczyciel";
                }

            }


        }
    }
}
