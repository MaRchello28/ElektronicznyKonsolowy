using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews
{
    public class DeleteStudentView1
    {
        MyDbContext db;
        public DeleteStudentView1(MyDbContext db) { this.db = db; }
        public int StartDeleteWindow()
        {
            var options = new[] {
            "Usuń ucznia, poprzez zaznaczanie odpowiednich indeksów",
            "Usun ucznia, poprzez wpisanie jego Id",
            };
            var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("[red]W jaki sposób chcesz usunąć ucznia/ów[/]")
            .PageSize(10)
            .AddChoices(options));

            return Array.IndexOf(options, selectedOption);
        }
        public List<string> FirstDeleteWindow()
        {
            List<Student> students = db.Students.ToList();

            var studentNames = students.Select(s => $"{s.studentId} {s.name} {s.surname}").ToList();

            var selectStudents = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[red]Wybierz uczniów, których chcesz usunąć: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(studentNames));

            return selectStudents;
        }
        public int Agree()
        {
            var options = new[] {
            "Tak",
            "Nie",
            };
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Czy jesteś pewny, że chcesz usunąć tych użytkowników?")
                    .PageSize(10)
                    .AddChoices(options));

            return Array.IndexOf(options, selectedOption);
        }
    }
}
