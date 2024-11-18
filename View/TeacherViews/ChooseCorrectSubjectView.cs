using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Data.Entity;
using ElektronicznyKonsolowy.Controller.TeachersController;

namespace ElektronicznyKonsolowy.View.TeacherViews
{
    public class ChooseCorrectSubjectView
    {
        MyDbContext db; ExistingGradeController egc;
        public ChooseCorrectSubjectView(MyDbContext db) 
        { 
            this.db = db; egc = new ExistingGradeController(db);
        }
        //Metody do wstawiania ocen
        public int Run(List<string> subjectNames)
        {
            string[] optionsInArray = new string[subjectNames.Count];
            for(int i=0; i< subjectNames.Count; i++)
            {
                optionsInArray[i] = subjectNames[i];
            }

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz przedmiot: ")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);

            return index;
        }
        public int SelectOption()
        {
            string[] optionsInArray = new string[3];
            optionsInArray[0] = "Przejdź do ocen";
            optionsInArray[1] = "Przejdź do obecności";
            optionsInArray[2] = "Powrót";

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Co chcesz wykonać?")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);

            return index;
        }
        public int ManageGrades(int selectedClass, int userId, int selectedSession)
        {
            string[] optionsInArray = new string[5];
            optionsInArray[0] = "Wyświetl oceny klasy";
            optionsInArray[1] = "Wstaw nowe oceny";
            optionsInArray[2] = "Edytuj istniejące oceny";
            optionsInArray[3] = "Usuń wszystkie oceny dla danego opisu";
            optionsInArray[4] = "Powrót";

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Co chcesz wykonać?")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);

            return index;
        }
        public void ShowGrades(Table table)
        {   
            AnsiConsole.Write(table);
        }
        public void AddNewGrade(List<string> studentsWithNewGrade, int userId, int selectedSession, Dictionary<string, DateTime> descriptionData, int subjectId)
        {
            List<string> studentIds = studentsWithNewGrade.Select(student => student.Split(' ')[0]).ToList();
            string[] studentNames = studentsWithNewGrade
            .Select(student =>
            {
                var parts = student.Trim().Split(' ');
                return parts[1] + " " + parts[2];
            })
            .ToArray();
            List<Grade> grades = new List<Grade>();
            AnsiConsole.MarkupLine("[green]Podaj za co ocena: [/]");
            string description = Console.ReadLine();
            foreach(var desc in descriptionData)
            {
                if(description.Equals(desc.Key, StringComparison.OrdinalIgnoreCase))
                {
                    AnsiConsole.MarkupLine("[red]Istnieje już taka ocena![/]");
                    return;
                }
            }
            AnsiConsole.MarkupLine("[green]Podaj wagę oceny: [/]");
            string value = Console.ReadLine();
            int wage = int.Parse(value);
            int i = 0;
            foreach (var student in studentsWithNewGrade)
            {
                if (Equals(student, "0 Cala klasa: "))
                {
                    i++;
                }
                else
                {
                    AnsiConsole.MarkupLine("[green]Wstawiasz ocenę dla " + string.Join(", ", studentNames[i]) + "[/]");

                    double mark = 0;
                    bool validGrade = false;

                    do
                    {
                        AnsiConsole.MarkupLine("Podaj ocenę (naciśnij Esc, aby przerwać): ");

                        if (Console.KeyAvailable)
                        {
                            var key = Console.ReadKey(intercept: true);
                            if (key.Key == ConsoleKey.Escape)
                            {
                                AnsiConsole.MarkupLine("[yellow]Proces wprowadzania ocen został przerwany.[/]");
                                return;
                            }
                        }

                        string markString = Console.ReadLine();

                        if (double.TryParse(markString, out mark))
                        {
                            if (egc.CheckGrade(mark))
                            {
                                validGrade = true;
                            }
                            else
                            {
                                AnsiConsole.MarkupLine("[red]Podana ocena nie istnieje![/]");
                            }
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Podana wartość nie jest liczbą![/]");
                        }
                    } while (!validGrade);

                    DateTime time = DateTime.Now;
                    int IdStudent = int.Parse(studentIds[i++]);
                    grades.Add(new Grade(mark, wage, description, IdStudent, userId, selectedSession, subjectId));
                }
            }
            foreach (var grade in grades)
            {
                db.Grades.Add(grade);
            }
            db.SaveChanges();
        }
        public List<string> ChooseStudentsForNewGrades(StudentClass studentClass)
        {
            string[] options = new string[studentClass.students.Count];
            int i = 0;
            foreach(var student in studentClass.students)
            {
                options[i++] = ""+student.studentId+" "+student.surname+" "+student.name;
            }
            var selectoptions = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[red]Wybierz uczniów, którym chcesz postawić oceny: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices("Cala klasa: ")
            .AddChoices(options));

            if (selectoptions.Contains("Cala klasa: "))
            {
                selectoptions.AddRange(options);
                selectoptions[0] = "0 Cala klasa: ";
            }

            return selectoptions;
        }
        public int EditGrade(Dictionary<string, DateTime> descriptionDates)
        {
            string[] options = new string[descriptionDates.Count+1];
            int i = 0;
            foreach(var desc in descriptionDates)
            {
                options[i++] = desc.Key.ToString();
            }
            options[descriptionDates.Count] = "Powrót";
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Co chcesz wykonać?")
                    .PageSize(10)
                    .AddChoices(options));
            int index = Array.IndexOf(options, selectedOption);

            return index;
        }
        public int ChooseGrade()
        {
            string[] optionsInArray = new string[2];
            optionsInArray[0] = "Nowa ocena";
            optionsInArray[1] = "Edytuj dostepne pole na oceny";
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz opcję: ")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);
            return index;
        }
        public Table AddRowWithGrade(Table tableSelectedDescription, Student s, Grade grade)
        {
            tableSelectedDescription.AddRow(s.user.surname + " " + s.user.name, grade.ToString());
            return tableSelectedDescription;
        }
        public Table AddRowWithoutGrade(Table tableSelectedDescription, Student s)
        {
            tableSelectedDescription.AddRow(s.user.surname + " " + s.user.name, "");
            return tableSelectedDescription;
        }
        public List<string> SelectStudentsToEditGrade(List<Student> students, string description, int selectedSession, List<int> subjectsId)
        {
            bool was = false; Grade grade;
            string[] options = new string[students.Count];
            int i = 0;
            foreach (var s in students)
            {
                for (int j = 0; j < subjectsId.Count; j++)
                {
                    if (s.grades.FirstOrDefault(g => g.sessionId == selectedSession && g.subjectId == subjectsId[j] && g.description == description)
                        != null)
                    {
                        grade = s.grades.FirstOrDefault(g => g.sessionId == selectedSession && g.subjectId == subjectsId[j] && g.description == description);
                        was = true;
                        options[i++] = s.studentId + " " + s.surname + " " + s.name + " Ocena teraz: " + grade.ToString();
                    }
                }
                if(was == false)
                {
                    options[i++] = s.studentId + " " + s.surname + " " + s.name + " Ocena teraz: " + "Brak";
                }
                was = false;
                
            }
            var selectoptions = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[red]Wybierz uczniów, którym chcesz postawić oceny: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(options));

            return selectoptions;
        }
        public int GetGradeWageByDescription(string description)
        {
            var grade = db.Set<Grade>().FirstOrDefault(g => g.description.Equals(description, StringComparison.OrdinalIgnoreCase));
            if (grade != null)
            {
                return grade.wage;
            }
            else
            {
                Console.WriteLine("Nie znaleziono oceny o podanym opisie.");
                return -1;
            }
        }
        public int GetSubjectIdByDescription(string description)
        {
            var subject = db.Set<Grade>().FirstOrDefault(s => s.description.Equals(description, StringComparison.OrdinalIgnoreCase));
            if (subject != null)
            {
                return subject.subjectId;
            }
            else
            {
                Console.WriteLine("Nie znaleziono przedmiotu o podanym opisie.");
                return -1;
            }
        }
        public void EditGradesSelectedStudents(List<string> studentsForNewGrades, int selectedSession, string description, List<int> subjectsId, int teacherId)
        {
            List<string> ids = new List<string>();
            List<string> nameAndSurname = new List<string>();
            List<string> grades = new List<string>();

            foreach (var student in studentsForNewGrades)
            {
                var parts = student.Split(' ');

                if (parts.Length == 6)  //[0]-id [1]-nazwisko [2]-imie [5]-obecna ocena
                {
                    ids.Add(parts[0]);
                    nameAndSurname.Add(parts[1] + " " + parts[2]);
                    grades.Add(parts[5]);
                }
                else
                {
                    Console.WriteLine("Błąd formatu: " + student);
                    return;
                }
            }
            int wage = GetGradeWageByDescription(description);
            int subjectId = GetSubjectIdByDescription(description);

            if(wage == -1 || subjectId == -1)
            {
                SuccesAndErrorsView.ShowErrorMessage("Najprawdopodobniej edytujesz typ oceny, który nie został stworzony");
            }
            for (int i = 0; i < grades.Count; i++)
            {
                AnsiConsole.MarkupLine("[aqua]Zmieniasz ocenę dla: " + nameAndSurname[i] + " o id: " + ids[i] + "[/]");
                AnsiConsole.MarkupLine("[red]Poprzednia ocena: " + grades[i] + "[/]");
                AnsiConsole.Markup("[green]Nowa ocena: [/]");
                string newValueString = Console.ReadLine();

                if (double.TryParse(newValueString, out double newValue))
                {
                    int studentId = int.Parse(ids[i]);

                    var gradeToUpdate = db.Set<Grade>().FirstOrDefault(g => g.sessionId == selectedSession 
                        && g.description == description && g.studentId == studentId);

                    if (gradeToUpdate != null)
                    {
                        gradeToUpdate.value = newValue;
                        gradeToUpdate.time = DateTime.Now;

                        db.SaveChanges();
                        AnsiConsole.MarkupLine("[green]Ocena została zaktualizowana pomyślnie![/]");
                    }
                    else
                    {
                        Grade grade = new Grade(newValue, wage, description, studentId, teacherId, selectedSession, subjectId);
                        db.Grades.Add(grade);
                        db.SaveChanges();
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]Wprowadzona wartość nie jest prawidłową liczbą![/]");
                }
            }
        }
        //Zarządzanie do sprawdzania obecności
        public int ManageLessons()
        {
            string[] optionsInArray = new string[4];
            optionsInArray[0] = "Stwórz lekcje";
            optionsInArray[1] = "Wybierz istniejącą lekcje";
            optionsInArray[2] = "Wyswietl obecność ze wszystkich lekcji";
            optionsInArray[3] = "Powrót";

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Co chcesz wykonać?")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);

            return index;
        }
    }
}
