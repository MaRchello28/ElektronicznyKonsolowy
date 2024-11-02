using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageClassView
{
    public class DeleteClassView1
    {
        MyDbContext db;
        public DeleteClassView1(MyDbContext db) { this.db = db; }
        public int StartDeleteWindow()
        {
            var options = new[] {
            "Usuń klasę, poprzez zaznaczanie odpowiednich indeksów",
            "Usun klasę, poprzez wpisanie jej Id",
            };
            var selectedOption = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
            .Title("W jaki sposób chcesz usunąć klasę: ")
            .PageSize(10)
            .AddChoices(options));

            return Array.IndexOf(options, selectedOption);
        }
        public List<string> FirstDeleteWindow()
        {
            List<StudentClass> classes = db.StudentClasses.ToList();

            var classNames = classes.Select(s => $"{s.studentClassId} {s.number+s.letter} {s.teacherId}").ToList();

            var selectClasses = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[red]Wybierz klasy, które chcesz usunąć: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(classNames));

            return selectClasses;
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
