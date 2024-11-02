using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews
{
    public class DeleteParentView1
    {
        MyDbContext db;
        public DeleteParentView1(MyDbContext db) { this.db = db; }
        public int StartDeleteWindow()
        {
            var options = new[] {
            "Usuń rodzica, poprzez zaznaczanie odpowiednich indeksów",
            "Usun rodzica, poprzez wpisanie jego Id",
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
            List<Parent> parents = db.Parents.ToList();

            var parentNames = parents.Select(s => $"{s.parentId} {s.name} {s.surname}").ToList();

            var selectParents = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[red]Wybierz rodziców, których chcesz usunąć: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(parentNames));

            return selectParents;
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
