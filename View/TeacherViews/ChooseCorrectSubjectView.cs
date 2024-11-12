using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace ElektronicznyKonsolowy.View.TeacherViews
{
    public class ChooseCorrectSubjectView
    {
        MyDbContext db;
        public ChooseCorrectSubjectView(MyDbContext db) 
        { 
            this.db = db;
        }
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
            string[] optionsInArray = new string[4];
            optionsInArray[0] = "Wyświetl oceny klasy";
            optionsInArray[1] = "Wstaw nowe oceny";
            optionsInArray[2] = "Edytuj istniejące oceny";
            optionsInArray[3] = "Powrót";

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
            List<string> studentNames = studentsWithNewGrade.Select(student => student.Split(' ')[1] + " " + student.Split(' ')[2]).ToList();
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
            foreach(var student in studentsWithNewGrade)
            {
                AnsiConsole.MarkupLine("[green]Wstawiasz ocenę dla "+string.Join(", ", studentNames)+"[/]");
                AnsiConsole.MarkupLine("Podaj ocene: ");
                string markString = Console.ReadLine();
                double mark = int.Parse(markString);
                DateTime time = DateTime.Now;
                grades.Add(new Grade(mark, wage, description, int.Parse(studentIds[i++]), userId, selectedSession, subjectId));
            }
            foreach(var grade in grades)
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
                options[i++] = student.studentId+" "+student.surname+" "+student.name;
            }
            var selectoptions = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[red]Wybierz uczniów, którym chcesz postawić oceny: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices("Cała klasa: ")
            .AddChoices(options));

            return selectoptions;
        }
        public int EditGrade(Dictionary<string, DateTime> descriptionDates)
        {
            string[] options = new string[descriptionDates.Count];
            int i = 0;
            foreach(var desc in descriptionDates)
            {
                options[i++] = desc.Key.ToString();
            }

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
        public void ManageLessons()
        {

        }
    }
}
