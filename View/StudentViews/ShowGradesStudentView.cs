using ElektronicznyKonsolowy.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            var Subjects = db.Subjects.ToList();

            string[] options = new string[Subjects.Count()];
            int i = 0;
            foreach (var subject in Subjects)
            {
                options[i++] = subject.name+" "+subject.subjectId;
            }

            var selectedSubject = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[blue]Wybierz Przedmiot, aby zobaczyć oceny:[/]")
                .PageSize(10)
                .AddChoices(options)
            );

            int subjectId = int.Parse(selectedSubject.Split(' ').Last());

            var subjectGrades = Grades.Where(g => g.subjectId == subjectId).ToList();

            string[] gradeOptions = new string[subjectGrades.Count];
            int j = 0;
            foreach (var grade in subjectGrades)
            {
                gradeOptions[j++] = $"{grade.value} (data: {grade.time}) - {grade.description}";
            }
            if (gradeOptions.Length == 0)
            {
                AnsiConsole.MarkupLine("[red]Brak ocen dla wybranego przedmiotu![/]");
                return;
            }
            var selectedGrade = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Wybierz ocenę, aby zobaczyć szczegóły:[/]")
                    .PageSize(10)
                    .AddChoices(gradeOptions)
            );
            var chosenGrade = subjectGrades.FirstOrDefault(g =>
                selectedGrade.StartsWith(g.value.ToString()) && selectedGrade.Contains(g.description));
            var teacher = db.Teachers.FirstOrDefault(t => t.teacherId == chosenGrade.teacherId);
            string teacherFullName = teacher != null ? $"{teacher.user.name} {teacher.user.surname}" : "Nieznany nauczyciel";
            

            if (chosenGrade != null)
            {
                AnsiConsole.MarkupLine("[bold]Szczegóły oceny:[/]");
                AnsiConsole.MarkupLine($"[yellow]Wartość:[/] {chosenGrade.value}");
                AnsiConsole.MarkupLine($"[yellow]Waga:[/] {chosenGrade.wage}");
                AnsiConsole.MarkupLine($"[yellow]Nauczyciel wystawiający:[/] {teacherFullName}");
                AnsiConsole.MarkupLine($"[yellow]Opis:[/] {chosenGrade.description}");
                AnsiConsole.MarkupLine($"[yellow]Data wystawienia:[/] {chosenGrade.time}");
                
            }
        }
    }
}
