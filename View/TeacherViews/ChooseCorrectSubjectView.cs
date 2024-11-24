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
        ChangeGradeOnCorrectMark cgocm;
        public ChooseCorrectSubjectView(MyDbContext db) 
        { 
            this.db = db; egc = new ExistingGradeController(db);
            cgocm = new ChangeGradeOnCorrectMark();
        }
        //Metody do wstawiania ocen
        public int Run(List<string> subjectNames)
        {
            string[] optionsInArray = new string[subjectNames.Count+1];
            for(int i=0; i< subjectNames.Count; i++)
            {
                optionsInArray[i] = subjectNames[i];
            }
            optionsInArray[subjectNames.Count] = "Powrót";

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

            string description;

            do
            {
                int c = 0;

                AnsiConsole.MarkupLine("[green]Podaj za co ocena (naciśnij Enter bez wprowadzonych wartości, aby przerwać): [/]");

                description = Console.ReadLine();
                Console.Clear();
                if (string.IsNullOrEmpty(description))
                {
                    AnsiConsole.MarkupLine("[yellow]Proces został przerwany przez użytkownika.[/]");
                    return;
                }

                foreach (var desc in descriptionData)
                {
                    if (description.Equals(desc.Key, StringComparison.OrdinalIgnoreCase))
                    {
                        AnsiConsole.MarkupLine("[red]Istnieje już taka ocena![/]");
                        c++;
                    }
                }
                if (c == 0)
                {
                    break;
                }
                c = 0;
            } while (true);

            int wage = 1;
            try
            {
                do
                {
                    AnsiConsole.MarkupLine("[green]Podaj wagę oceny (naciśnij Enter bez wprowadzonych wartości, aby przerwać): [/]");
                    string value = Console.ReadLine();
                    Console.Clear();
                    if(string.IsNullOrEmpty(value))
                    {
                        AnsiConsole.MarkupLine("[yellow]Proces został przerwany przez użytkownika.[/]");
                        return;
                    }
                    if (int.TryParse(value, out wage) && wage > 0)
                    {
                        break;
                    }
                    else
                    {
                        if (!int.TryParse(value, out wage))
                        {
                            AnsiConsole.MarkupLine("[red]Podano błędną wagę oceny! Waga musi być liczbą całkowitą.[/]");
                        }
                        else if (wage <= 0)
                        {
                            AnsiConsole.MarkupLine("[red]Waga musi być dodatnią liczbą całkowitą![/]");
                        }
                    }
                } while (true);
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine("[red]Wystąpił nieoczekiwany błąd: {0}[/]", ex.Message);
            }

            int i = 0;
            foreach (var student in studentsWithNewGrade)
            {
                Console.Clear();
                if (Equals(student, "0 Cala klasa: "))
                {
                    i++;
                }
                else
                {
                    AnsiConsole.MarkupLine("[green]Wstawiasz ocenę dla " + string.Join(", ", studentNames[i]) + "[/]");

                    double mark = 0;
                    bool validGrade = false;

                    AnsiConsole.MarkupLine("Podaj ocenę (naciśnij Enter bez wprowadzonych wartości, aby przerwać, 0 żeby przejść do następnej osoby): ");
                    do
                    {
                        string markString = Console.ReadLine();

                        if(string.IsNullOrEmpty(markString))
                        {
                            if(!Equals(student, studentsWithNewGrade[1]))
                            {
                                int choose = SaveChangesChoose();
                                if (choose == 0)
                                {
                                    db.SaveChanges();
                                    AnsiConsole.MarkupLine("[yellow]Oceny zostały zapisane.[/]");
                                }
                            }
                            
                            AnsiConsole.MarkupLine("[yellow]Proces wprowadzania ocen został przerwany.[/]");
                            return;
                        }
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
        public int SaveChangesChoose()
        {
            string[] optionsInArray = new string[3];
            optionsInArray[0] = "Tak";
            optionsInArray[1] = "Nie";

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Czy chcesz zapisać wystawione oceny?")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);

            return index;
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
        public int EditGrade(List<string> descriptions)
        {
            string[] options = new string[descriptions.Count+1];
            int i = 0;
            foreach(var desc in descriptions)
            {
                options[i] = descriptions[i];
                i++;
            }
            options[descriptions.Count] = "Powrót";
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz ocenę, którą chcesz edytować")
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
            string color = cgocm.CorrectColorForGrade(grade.value);
            string gradeChar = cgocm.ChangeNumberOnChar(grade.value);

            if (string.IsNullOrWhiteSpace(color))
            {
                SuccesAndErrorsView.ShowErrorMessage("Nieprawidłowe dane");
                return new Table();
            }

            tableSelectedDescription.AddRow(
                $"{s.user.surname} {s.user.name}",
                $"[{color}]{gradeChar}[/]"
            );

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
                        if(grade.ToString() == "0")
                        {
                            options[i++] = s.studentId + " " + s.surname + " " + s.name + " Ocena teraz: " + "Brak";
                        }
                        else
                        {
                            was = true;
                            options[i++] = s.studentId + " " + s.surname + " " + s.name + " Ocena teraz: " + grade.ToString();
                        }
                    }
                }
                //if(was == false)
                //{
                //    options[i++] = s.studentId + " " + s.surname + " " + s.name + " Ocena teraz: " + "Brak";
                //}
                //was = false;
                
            }

            var selectoptions = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[red]Wybierz uczniów, którym chcesz edytować oceny: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(options));
            Console.Clear();
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
            Console.Clear();
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
                Console.Clear();
                do
                {
                    string color;
                    if (grades[i]=="Brak")
                    {
                        color = cgocm.CorrectColorForGrade(0);
                    }
                    else
                    {
                        color = cgocm.CorrectColorForGrade(double.Parse(grades[i]));
                    }
                    AnsiConsole.MarkupLine("[aqua]Zmieniasz ocenę dla: " + nameAndSurname[i] + " o id: " + ids[i] + "[/]");
                    if (grades[i] == "Brak")
                    {
                        AnsiConsole.MarkupLine($"[red]Poprzednia ocena: [/][{color}]Brak[/]");
                    }
                    else
                    {
                        AnsiConsole.MarkupLine($"[red]Poprzednia ocena: [/][{color}]{cgocm.ChangeNumberOnChar(double.Parse(grades[i]))}[/]");
                    }
                    AnsiConsole.MarkupLine("[green]Nowa ocena: (Naciśnij Enter, żeby anulować edytowanie ocen)[/]");
                    string newValueString = Console.ReadLine();
                    Console.Clear();
                    if (string.IsNullOrEmpty(newValueString))
                    {
                        if (i != 0)
                        {
                            var choose = SaveChangesChoose();
                            if (choose == 0)
                            {
                                db.SaveChanges();
                                AnsiConsole.MarkupLine("[yellow]Dane zostały zapisane.[/]");
                            }
                        }
                        AnsiConsole.MarkupLine("[yellow]Przerwano edytowanie ocen[/]");
                        return;
                    }

                    if (double.TryParse(newValueString, out double newValue))
                    {

                        if(egc.CheckGrade(newValue))
                        {
                            int studentId = int.Parse(ids[i]);

                            var gradeToUpdate = db.Set<Grade>().FirstOrDefault(g => g.sessionId == selectedSession
                                && g.description == description && g.studentId == studentId);

                            gradeToUpdate.value = newValue;
                            gradeToUpdate.time = DateTime.Now;
                            AnsiConsole.MarkupLine("[green]Ocena została zaktualizowana pomyślnie![/]");

                            break;
                        }
                        else
                        {
                            AnsiConsole.MarkupLine("[red]Wybrana ocena nie istnieje![/]");
                        }
                    }
                    else
                    {
                        AnsiConsole.MarkupLine("[red]Wprowadzona wartość nie jest prawidłową liczbą![/]");
                    }
                } while (true);
            }
            db.SaveChanges();
        }
        public void ShowGradesDescription(List<string> sortedDescriptions)
        {
            int descNumber = 1;
            AnsiConsole.MarkupLine("[green]Opis ocen:[/]");
            if(sortedDescriptions.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Ta klasa nie ma żadnych ocen[/]");
            }
            else
            {
                foreach (var description in sortedDescriptions)
                {
                    AnsiConsole.MarkupLine($"{descNumber++} - {description}");
                }
            }

            AnsiConsole.MarkupLine("[purple]Naciśnij dowolny przycisk, żeby przejść dalej[/]");
            Console.ReadLine();
        }
        public void AgreeDeleteGrade()
        {
            AnsiConsole.MarkupLine("Czy na pewno chcesz usunąć tą ocenę?");
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
