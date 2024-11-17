using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;

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
            while (true)
            {
                // Pobieranie dzieci powiązanych z rodzicem
                var children = db.Students.Where(s => s.parentId == parent.parentId).ToList();
                if (!children.Any())
                {
                    AnsiConsole.MarkupLine("[red]Brak dzieci powiązanych z tym rodzicem.[/]");
                    return;
                }

                // Wyświetlenie listy dzieci do wyboru
                var childOptions = children.Select(c => $"{c.studentId}. {c.name} {c.surname}").ToArray();
                var selectedChildOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]Wybierz dziecko, aby zobaczyć oceny:[/]")
                        .PageSize(10)
                        .AddChoices(childOptions.Append("Cofnij"))
                );

                if (selectedChildOption == "Cofnij")
                {
                    Console.Clear();
                    return;
                }

                // Parsowanie wybranego dziecka
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

                var selectedChild = children.FirstOrDefault(c => c.studentId == childId);
                if (selectedChild == null) continue;

                // Pobieranie ocen dla wybranego dziecka
                var grades = db.Grades.Where(g => g.studentId == selectedChild.studentId).ToList();
                if (!grades.Any())
                {
                    AnsiConsole.MarkupLine("[red]Brak ocen dla tego dziecka.[/]");
                    return;
                }

                // Tworzenie tabeli z ocenami
                var table = new Table()
                    .Border(TableBorder.Ascii)
                    .AddColumn(new TableColumn(new Markup("[blue]Przedmiot[/]")))
                    .AddColumn(new TableColumn(new Markup("[blue]Oceny[/]")));

                int rowIndex = 0;
                var subjects = db.Subjects.ToList();
                foreach (var subject in subjects)
                {
                    var subjectGrades = grades.Where(g => g.subjectId == subject.subjectId).ToList();
                    string gradesString = subjectGrades.Any()
                        ? string.Join(", ", subjectGrades.Select(g => g.value.ToString()))
                        : "";

                    // Zmiana kolorów wierszy (naprzemiennie niebieski i fioletowy)
                    var rowStyle = rowIndex % 2 == 0 ? new Style(Color.Blue) : new Style(Color.Purple);
                    table.AddRow(
                        new Text(subject.name, rowStyle),
                        new Text(gradesString, rowStyle)
                    );

                    rowIndex++;
                }

                // Wyświetlanie tabeli z ocenami
                AnsiConsole.Write(table);

                // Zapytanie o wybór przedmiotu
                var subjectOptions = subjects.Select(s => $"{s.subjectId}. {s.name}").ToArray();
                subjectOptions = subjectOptions.Append("Wróć").ToArray(); // Dodanie opcji "Wróć"

                var selectedSubjectOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]Wybierz przedmiot, aby zobaczyć szczegóły:[/]")
                        .PageSize(10)
                        .AddChoices(subjectOptions)
                );

                if (selectedSubjectOption == "Wróć")
                {
                    Console.Clear();
                    return; // Powrót do poprzedniego menu
                }

                // Parsowanie wybranego przedmiotu
                int subjectId;
                try
                {
                    subjectId = int.Parse(selectedSubjectOption.Split(".").First());
                }
                catch (FormatException)
                {
                    AnsiConsole.MarkupLine("[red]Nieprawidłowy format ID przedmiotu![/]");
                    continue;
                }

                // Pobieranie ocen dla wybranego przedmiotu
                var selectedSubjectGrades = grades.Where(g => g.subjectId == subjectId).ToList();
                if (!selectedSubjectGrades.Any())
                {
                    AnsiConsole.MarkupLine("[red]Brak ocen dla wybranego przedmiotu![/]");
                    continue;
                }

                // Wyświetlanie ocen dla wybranego przedmiotu
                var gradeOptions = selectedSubjectGrades.Select(g => $"{g.value} - {g.time}").ToArray();
                gradeOptions = gradeOptions.Append("Wróć").ToArray(); // Dodanie opcji "Wróć"

                var selectedGradeOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]Wybierz ocenę, aby zobaczyć szczegóły:[/]")
                        .PageSize(10)
                        .AddChoices(gradeOptions)
                );

                if (selectedGradeOption == "Wróć")
                {
                    Console.Clear();
                    continue; // Powrót do poprzedniego menu (wybór przedmiotu)
                }

                // Parsowanie wybranej oceny i wyświetlanie szczegółów
                var selectedGrade = selectedSubjectGrades
                    .FirstOrDefault(g => $"{g.value} - {g.time}" == selectedGradeOption);

                if (selectedGrade != null)
                {
                    var teacher = db.Teachers.FirstOrDefault(t => t.teacherId == selectedGrade.teacherId);
                    string teacherName = teacher != null ? $"{teacher.user.name} {teacher.user.surname}" : "Nieznany nauczyciel";

                    // Wyświetlanie szczegółów oceny
                    AnsiConsole.MarkupLine("[bold]Szczegóły oceny:[/]");
                    AnsiConsole.MarkupLine($"[yellow]Wartość:[/] {selectedGrade.value}");
                    AnsiConsole.MarkupLine($"[yellow]Waga:[/] {selectedGrade.wage}");
                    AnsiConsole.MarkupLine($"[yellow]Nauczyciel wystawiający:[/] {teacherName}");
                    AnsiConsole.MarkupLine($"[yellow]Opis:[/] {selectedGrade.description}");
                    AnsiConsole.MarkupLine($"[yellow]Data wystawienia:[/] {selectedGrade.time}");
                }

                AnsiConsole.MarkupLine("[grey]Naciśnij klawisz, aby wrócić...[/]");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }
}
