using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageTeachersView
{
    public class DeleteTeacherView1
    {
        MyDbContext db;
        public DeleteTeacherView1 (MyDbContext db) 
        { 
            this.db = db;
        }
        public int StartDeleteWindow()
        {
            var options = new[] {
            "Usuń nauczyciela, poprzez zaznaczanie odpowiednich indeksów",
            "Usun nauczyciela, poprzez wpisanie jego Id",
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
            List<Teacher> teachers = db.Teachers.ToList();

            var studentNames = teachers.Select(s => $"{s.teacherId} {s.user.name} {s.user.surname}").ToList();

            var selectTeachers = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[red]Wybierz nauczycieli, których chcesz usunąć: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(studentNames));

            return selectTeachers;
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
