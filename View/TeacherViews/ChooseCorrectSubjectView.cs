using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
            optionsInArray[0] = "Wstaw oceny";
            optionsInArray[1] = "Lekcje";
            optionsInArray[2] = "Powrót";

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Co chcesz wykonać?")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);

            return index;
        }
        public void ManageGrades(int selectedClass)
        {
            var studentClass = db.StudentClasses
            .Include(sc => sc.students)
            .FirstOrDefault(sc => sc.studentClassId == selectedClass);

            if (studentClass != null)
            {
                studentClass.students = studentClass.students.OrderBy(s => s.surname).ToList();
            }

            if (studentClass == null || studentClass.students == null || !studentClass.students.Any())
            {
                Console.WriteLine("Brak danych dla wybranej klasy.");
                return;
            }

            foreach (var student in studentClass.students)
            {
                db.Entry(student)
                    .Collection(s => s.grades)
                    .Load();
            }

            var table = new Table();
            table.AddColumn("Nazwisko i Imię");

            var descriptions = studentClass.students
                .SelectMany(s => s.grades)
                .Where(g => g != null)
                .Select(g => g.description)
                .Distinct()
                .ToList();

            int number = 1;
            foreach (var description in descriptions)
            {
                table.AddColumn(number++.ToString());
            }

            foreach (var student in studentClass.students)
            {
                var fullName = $"{student.surname} {student.name}";
                var row = new List<string> { fullName };

                foreach (var description in descriptions)
                {
                    var grade = student.grades.FirstOrDefault(g => g.description == description);
                    row.Add(grade != null ? grade.value.ToString() : "");
                }

                table.AddRow(row.ToArray());
            }
            AnsiConsole.Write(table);
        }
        
        public void ManageLessons()
        {

        }
    }
}
