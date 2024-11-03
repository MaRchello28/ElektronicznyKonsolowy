using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews
{
    public class AddStudentToClassView
    {
        MyDbContext db;
        public AddStudentToClassView(MyDbContext db) { this.db = db; }
        public int ChooseClass()
        {
            List<StudentClass> classes = db.StudentClasses.ToList();

            var classOptions = classes.ToDictionary(
                c => c.number.ToString() + c.letter, 
                c => c.studentClassId                 
            );

            var selectedClassDisplay = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz klasę, do której będziesz przydzielał uczniów: ")
                    .PageSize(10)
                    .AddChoices(classOptions.Keys)
            );
            int selectedClassId = classOptions[selectedClassDisplay];
            AnsiConsole.MarkupLine($"[yellow]Edytowana klasa: {selectedClassDisplay}[/]");

            return selectedClassId;
        }
        public List<Student> ChooseStudents()
        {
            List<Student> students = db.Students.ToList();
            students = students.OrderBy(s => s.surname).ToList();

            var studentOptions = students.ToDictionary(
                s => $"{s.studentId} {s.name} {s.surname}", s=> s
                );

            var options = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("Wybierz uczniów, których chcesz przypisać do odpowiedniej klasy: ")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [blue]<space>[/] żeby zaznaczyć ucznia, " +
            "[green]<enter>[/] żeby zaakceptować)[/]")
            .AddChoices(students.Select(s => s.studentId + " " + s.name + " " + s.surname).ToList()
            ));
            var selectedStudents = options.Select(options => studentOptions[options]).ToList();
            return selectedStudents;
        }
    }
}
