using ElektronicznyKonsolowy.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Data;
using ElektronicznyKonsolowy.Controller.TeachersController;

namespace ElektronicznyKonsolowy.View.StudentViews
{
    public class ShowGradesStudentView
    {
        MyDbContext db;
        public ShowGradesStudentView(MyDbContext db) { this.db = db; }
        public void show(Student student)
        {
            var changeGrade = new ChangeGradeOnCorrectMark();
            while(true)
            {
                var Grades = db.Grades.Where(g => g.studentId == student.studentId).ToList();
                var Subjects = db.Subjects.ToList();

                var table = new Table()
                .AddColumn("Przedmiot")
                .AddColumn("Oceny");
                int i = 0;
                foreach (var subject in Subjects)
                {
                    var subgrade = Grades.Where(g => g.subjectId == subject.subjectId).ToList();
                    string gradesString = subgrade.Any()
                        ? string.Join(", ", subgrade.Select(g => changeGrade.ChangeNumberOnChar(g.value)))
                        : "";
                    if (i == 0)
                    {
                        table.AddRow(
                                new Text(subject.name, Color.Blue),
                                new Text(gradesString, Color.Blue)
                                );
                        i++;
                    }

                    else
                    {
                        table.AddRow(
                                new Text(subject.name, Color.Purple),
                                new Text(gradesString, Color.Purple)
                                );
                        i--;
                    }

                }
                AnsiConsole.Write(table);
                string[] options = new string[Subjects.Count()];
                i = 0;
                foreach (var subject in Subjects)
                {
                    options[i++] = subject.subjectId + ". " + subject.name;
                }
                options = options.Append("Cofnij").ToArray();
                var selectedSubject = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Wybierz Przedmiot, aby zobaczyć oceny:[/]")
                    .PageSize(10)
                    .AddChoices(options)
                );
                if(selectedSubject == "Cofnij")
                {
                    Console.Clear();
                    return;
                }
                int subjectId;
                try
                {
                    subjectId = int.Parse(selectedSubject.Split(". ").First());
                }
                catch (FormatException)
                {
                    AnsiConsole.MarkupLine("[red]Nieprawidłowy format przedmiotu![/]");
                    continue;  // Jeśli parsowanie się nie powiedzie, przejdź do kolejnej iteracji
                }
                var subjectGrades = Grades.Where(g => g.subjectId == subjectId).ToList();

                string[] gradeOptions = new string[subjectGrades.Count];
                int j = 0;
                foreach (var grade in subjectGrades)
                {
                    string gradeChar = changeGrade.ChangeNumberOnChar(grade.value);
                    gradeOptions[j++] = $"{gradeChar} (data: {grade.time}) - {grade.description}";
                }
                if (gradeOptions.Length == 0)
                {
                    AnsiConsole.MarkupLine("[red]Brak ocen dla wybranego przedmiotu![/]\n");

                }
                else
                {
                    var selectedGrade = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]Wybierz ocenę, aby zobaczyć szczegóły:[/]")
                        .PageSize(10)
                        .AddChoices(gradeOptions)
                    );
                    var chosenGrade = subjectGrades.FirstOrDefault(g =>
                    selectedGrade.StartsWith(changeGrade.ChangeNumberOnChar(g.value)) &&
                    selectedGrade.Contains(g.description));
                    var teacher = db.Teachers.FirstOrDefault(t => t.teacherId == chosenGrade.teacherId);
                    string teacherFullName = teacher != null ? $"{teacher.user.name} {teacher.user.surname}" : "Nieznany nauczyciel";


                    if (chosenGrade != null)
                    {
                        AnsiConsole.MarkupLine("[bold]Szczegóły oceny:[/]");
                        AnsiConsole.MarkupLine($"[yellow]Wartość:[/] {changeGrade.ChangeNumberOnChar(chosenGrade.value)}");
                        AnsiConsole.MarkupLine($"[yellow]Waga:[/] {chosenGrade.wage}");
                        AnsiConsole.MarkupLine($"[yellow]Nauczyciel wystawiający:[/] {teacherFullName}");
                        AnsiConsole.MarkupLine($"[yellow]Opis:[/] {chosenGrade.description}");
                        AnsiConsole.MarkupLine($"[yellow]Data wystawienia:[/] {chosenGrade.time}\n");

                    }
                }
                AnsiConsole.MarkupLine("[grey]Naciśnij klawisz aby kontynuować...[/]");
                Console.ReadKey();
                Console.Clear();

            }
            
            
        }
    }
}
