using ElektronicznyKonsolowy.Models;
using Microsoft.Extensions.Options;
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
            var children = db.Students.Where(s => s.parentId == parent.parentId).ToList();
            if (!children.Any())
            {
                Console.WriteLine("Brak dzieci powiązanych z tym rodzicem.");
                return;
            }
            string[] options = children.Select(c => $"{c.user.name} {c.user.surname} (ID: {c.studentId})").ToArray();

            var selectedChildOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Wybierz dziecko, aby zobaczyć oceny:[/]")
                    .PageSize(10)
                    .AddChoices(options)
            );
            var selectedChildId = int.Parse(selectedChildOption.Split("ID: ").Last().Trim(')'));
            var selectedChild = children.FirstOrDefault(c => c.studentId == selectedChildId);
            var grades = db.Grades.Where(g => g.studentId == selectedChild.studentId).ToList();

            if (!grades.Any())
            {
                Console.WriteLine("Brak ocen dla tego dziecka.");
                return;
            }
            var Grades = db.Grades.Where(g => g.studentId == selectedChild.studentId).ToList();
            var Subjects = db.Subjects.ToList();

            string[] opcje = new string[Subjects.Count()];
            int i = 0;
            foreach (var subject in Subjects)
            {
                opcje[i++] = subject.name + " " + subject.subjectId;
            }

            var selectedSubject = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[blue]Wybierz Przedmiot, aby zobaczyć oceny:[/]")
                .PageSize(10)
                .AddChoices(opcje)
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
