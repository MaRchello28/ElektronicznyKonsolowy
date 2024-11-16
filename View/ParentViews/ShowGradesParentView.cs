using ElektronicznyKonsolowy.Models;
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
            bool run = true;
            while (true)
            {
                // Wybór dziecka
                var children = db.Students.Where(s => s.parentId == parent.parentId).ToList();
                if (!children.Any())
                {
                    AnsiConsole.MarkupLine("[red]Brak dzieci powiązanych z tym rodzicem.[/]");
                    return;
                }

                // Wyświetlenie listy dzieci
                string[] options = children.Select(c => $"{c.user.name} {c.user.surname} (ID: {c.studentId})").ToArray();
                options = options.Append("Wróć").ToArray(); // Dodanie opcji "Wróć"

                var selectedChildOption = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[blue]Wybierz dziecko, aby zobaczyć oceny:[/]")
                        .PageSize(10)
                        .AddChoices(options)
                );

                if (selectedChildOption == "Wróć")
                {
                    Console.Clear();
                    return; // Zakończenie przeglądania ocen, powrót do menu rodzica
                }

                var selectedChildId = int.Parse(selectedChildOption.Split("ID: ").Last().Trim(')'));
                var selectedChild = children.FirstOrDefault(c => c.studentId == selectedChildId);

                // Pobranie ocen dziecka
                var grades = db.Grades.Where(g => g.studentId == selectedChild.studentId).ToList();
                if (!grades.Any())
                {
                    AnsiConsole.MarkupLine("[red]Brak ocen dla tego dziecka.[/]");
                    AnsiConsole.MarkupLine("[grey]Naciśnij klawisz aby kontynuować...[/]");
                    Console.ReadKey();
                    Console.Clear();
                    continue; // Powrót do wyboru dziecka, jeśli brak ocen
                }

                var Subjects = db.Subjects.ToList();
                var table = new Table()
                    .AddColumn("Przedmiot")
                    .AddColumn("Oceny");

                int rowIndex = 0;
                foreach (var subject in Subjects)
                {
                    var subgrade = grades.Where(g => g.subjectId == subject.subjectId).ToList();
                    string gradesString = subgrade.Any()
                        ? string.Join(", ", subgrade.Select(g => g.value.ToString()))
                        : "";

                    // Dodanie naprzemiennej kolorystyki wierszy
                    if (rowIndex % 2 == 0)
                    {
                        table.AddRow(
                            new Text(subject.name, Color.Blue),
                            new Text(gradesString, Color.Blue)
                        );
                    }
                    else
                    {
                        table.AddRow(
                            new Text(subject.name, Color.Purple),
                            new Text(gradesString, Color.Purple)
                        );
                    }
                    rowIndex++;
                }

                // Wyświetlenie tabeli
                AnsiConsole.Write(table);

                // Wybór przedmiotu
                while(true)
                {
                    var selectedSubjectId = SelectSubject(Subjects, grades);
                    if (selectedSubjectId == null) break;

                    // Pobranie ocen dla wybranego przedmiotu
                    var subjectGrades = grades.Where(g => g.subjectId == selectedSubjectId.Value).ToList();

                    if (!subjectGrades.Any())
                    {
                        AnsiConsole.MarkupLine("[red]Brak ocen dla wybranego przedmiotu![/]");
                        AnsiConsole.MarkupLine("[grey]Naciśnij klawisz aby kontynuować...[/]");
                        Console.ReadKey();
                        Console.Clear();
                        continue;
                    }
                    while(true)
                    {
                        // Wybór oceny
                        int? i = SelectGrade(subjectGrades);

                        if(i == null) break;
                        // Czekanie na kontynuację
                        AnsiConsole.MarkupLine("[grey]Naciśnij klawisz, aby kontynuować...[/]");
                        Console.ReadKey();
                        Console.Clear();
                    }

                }
                
            }
        }

        // Funkcja do wyboru przedmiotu
        private int? SelectSubject(List<Subject> subjects, List<Grade> grades)
        {
            string[] subjectOptions = subjects.Select(s => $"{s.subjectId}. {s.name}").ToArray();
            subjectOptions = subjectOptions.Append("Wróć").ToArray(); // Dodanie opcji "Wróć"

            var selectedSubject = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Wybierz Przedmiot, aby zobaczyć oceny:[/]")
                    .PageSize(10)
                    .AddChoices(subjectOptions)
            );

            if (selectedSubject == "Wróć")
            {
                Console.Clear();
                return null; // Powrót do wyboru dziecka
            }

            int subjectId = int.Parse(selectedSubject.Split(".").First());
            return subjectId;
        }

        // Funkcja do wyboru oceny
        private int? SelectGrade(List<Grade> subjectGrades)
        {
            string[] gradeOptions = subjectGrades.Select(g => $"{g.value} (data: {g.time}) - {g.description}").ToArray();
            gradeOptions = gradeOptions.Append("Wróć").ToArray();

            var selectedGrade = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[blue]Wybierz ocenę, aby zobaczyć szczegóły:[/]")
                    .PageSize(10)
                    .AddChoices(gradeOptions)
            );

            if (selectedGrade == "Wróć")
            {
                Console.Clear();
                return null; // Powrót do wyboru przedmiotu
            }

            // Pobranie szczegółów oceny
            var chosenGrade = subjectGrades.FirstOrDefault(g => selectedGrade.StartsWith(g.value.ToString()) && selectedGrade.Contains(g.description));
            var teacher = db.Teachers.FirstOrDefault(t => t.teacherId == chosenGrade.teacherId);
            string teacherFullName = teacher != null ? $"{teacher.user.name} {teacher.user.surname}" : "Nieznany nauczyciel";

            // Wyświetlanie szczegółów oceny
            if (chosenGrade != null)
            {
                AnsiConsole.MarkupLine("[bold]Szczegóły oceny:[/]");
                AnsiConsole.MarkupLine($"[yellow]Wartość:[/] {chosenGrade.value}");
                AnsiConsole.MarkupLine($"[yellow]Waga:[/] {chosenGrade.wage}");
                AnsiConsole.MarkupLine($"[yellow]Nauczyciel wystawiający:[/] {teacherFullName}");
                AnsiConsole.MarkupLine($"[yellow]Opis:[/] {chosenGrade.description}");
                AnsiConsole.MarkupLine($"[yellow]Data wystawienia:[/] {chosenGrade.time}");
            }
            return 1;
        }
    }
}
