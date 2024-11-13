﻿using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.StudentViews
{
    public class ShowClassScheduleStudentView
    {
        MyDbContext db;
        public ShowClassScheduleStudentView(MyDbContext db) { this.db = db; }
        public void Show(Student student)
        {
            int id = (int)student.studentClassId;
            var CLA = db.ClassSchedules.FirstOrDefault(x => x.studentClassId == id);
            if (CLA == null)
            {
                AnsiConsole.MarkupLine("[red]Nie znaleziono planu lekcji![/]");
                return;
            }

            if (CLA.sessions == null || !CLA.sessions.Any())
            {
                AnsiConsole.MarkupLine("[red]Plan lekcji nie zawiera żadnych sesji![/]");
                return;
            }

            var table2 = new Table();
            table2.Border(TableBorder.Ascii);
            table2.AddColumn(new TableColumn(new Markup("[blue]Godzina[/]")));
            table2.AddColumn(new TableColumn(new Markup("[blue]Poniedziałek[/]")));
            table2.AddColumn(new TableColumn(new Markup("[blue]Wtorek[/]")));
            table2.AddColumn(new TableColumn(new Markup("[blue]Środa[/]")));
            table2.AddColumn(new TableColumn(new Markup("[blue]Czwartek[/]")));
            table2.AddColumn(new TableColumn(new Markup("[blue]Piątek[/]")));
            int i = 0;
            var lessonTimes = new List<string>
                {
                    "8:00 - 8:45", "8:55 - 9:40", "9:50 - 10:35",
                    "10:55 - 11:40", "11:50 - 12:35", "12:45 - 13:30",
                    "13:40 - 14:25", "14:35 - 15:20","15:30 - 16:15",
                    "16:25 - 17:10","17:20 - 18:05"
                };

            foreach (var time in lessonTimes)
            {
                if (i == 0)
                {
                    var style = new Style(Color.Blue);
                    table2.AddRow(
                        new Text(time, style),
                        new Text("", style),
                        new Text("", style),
                        new Text("", style),
                        new Text("", style),
                        new Text("", style)
                    );
                    i++;
                }
                else
                {
                    var style = new Style(Color.Purple);
                    table2.AddRow(
                        new Text(time, style),
                        new Text("", style),
                        new Text("", style),
                        new Text("", style),
                        new Text("", style),
                        new Text("", style)
                        );
                    i--;
                }

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
                        var styledText = $"{subjectName} {session.sala} ({teacherName} {teacherSurname})";
                        if (rowIndex % 2 == 0)
                            table2.UpdateCell(rowIndex, columnIndex, new Markup($"[blue]{styledText}[/]"));
                        else
                            table2.UpdateCell(rowIndex, columnIndex, new Markup($"[purple]{styledText}[/]"));
                    }
                }

            }
            AnsiConsole.Write(table2);
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
