using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews
{
    public class EditClassScheduleView
    {
        public EditClassScheduleView() { }
        public int ClassScheduleToEdit()
        {
            int id;
            AnsiConsole.WriteLine("[blue]Podaj idPlanu zajęć do edycji: [/]");
            string value = Console.ReadLine();
            id = int.Parse(value);
            return id;
        }
        public List<bool> ChooseOptionsToEdit()
        {
            List<bool> result = new List<bool> { false, false, false};
            var options = AnsiConsole.Prompt(
            new MultiSelectionPrompt<string>()
            .Title("[green]Wybierz opcje do edycji: [/]")
            .NotRequired()
            .PageSize(10)
            .InstructionsText(
            "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
            "[green]<enter>[/], żeby zaakceptować)[/]")
            .AddChoices(new[] {
            "ClassId", "Dodaj zajęcia", "Usuń zajęcia"}));

            for (int i = 0; i < result.Count; i++)
            {
                result[i] = options.Contains(new[] { "ClassId", "Dodaj zajęcia", "Usuń zajęcia" }[i]);
            }

            return result;
        }
        public int EditOption(int i)
        {
            if (i == 0)
            {
                int opt;
                AnsiConsole.MarkupLine("[blue] Podaj nowe Id Klasy: [/]");
                string name = Console.ReadLine();
                opt = int.Parse(name);
                return opt;
            }
            else if (i == 1)
            {
                AnsiConsole.MarkupLine("[blue] Podaj id sesji do dodania do planu lekcji: [/]");
                string name = Console.ReadLine();
                int opt;
                opt = int.Parse(name);
                return opt;
            }
            else if (i == 2)
            {
                AnsiConsole.MarkupLine("[blue] Podaj id sesji do usuniecia z planu lekcji: [/]");
                string name = Console.ReadLine();
                int opt;
                opt = int.Parse(name);
                return opt;
            }
            return -1;
        }
    }
}
