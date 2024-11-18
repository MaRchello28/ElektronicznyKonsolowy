using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdditionalOptionsView
{
    public class EditYourDataView
    {
        public EditYourDataView() { }

        public List<bool> ChooseWhatToEdit(UserType userType)
        {
            var allOptions = new List<string> {"Login", "Haslo", "Email", "Numer Telefonu" };

            List<string> availableOptions = userType switch
            {
                UserType.Admin => new List<string> { "Login", "Haslo" },
                UserType.Student => new List<string> { "Login", "Haslo" },
                UserType.Teacher => new List<string> { "Login", "Haslo", "Email" },
                UserType.Parent => new List<string> { "Login", "Haslo", "Email", "Numer Telefonu" },
                _ => throw new ArgumentOutOfRangeException(nameof(userType), $"Nieznany typ użytkownika: {userType}")
            };

            List<bool> result = new List<bool> { false, false, false, false };

            var options = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("[green]Wybierz opcje do edycji: [/]")
                    .NotRequired()
                    .PageSize(10)
                    .InstructionsText(
                        "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
                        "[green]<enter>[/], żeby zaakceptować)[/]")
                    .AddChoices(availableOptions)
            );

            for (int i = 0; i < allOptions.Count; i++)
            {
                result[i] = options.Contains(allOptions[i]) && availableOptions.Contains(allOptions[i]);
            }

            return result;
        }
        public string EditOption(int i)
        {
            if (i == 0)
            {
                string name;
                do
                {
                    name = Console.ReadLine();
                    AnsiConsole.MarkupLine("[blue] Podaj nowy login: [/]");
                    if (name.Length < 3) { AnsiConsole.MarkupLine("[red]Login nie może być krótszy niż 4 znaki[/]"); }
                }
                while (name.Length < 4);
                return name;
            }
            else if (i == 1)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowe hasło: [/]");
                return Console.ReadLine();
            }
            else if (i == 2)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy email: [/]");
                return Console.ReadLine();
            }
            else if (i == 3)
            {
                string name;
                AnsiConsole.MarkupLine("[blue] Podaj nowy Nr Telefonu: [/]");
                return Console.ReadLine();
            }
            return "";
        }
    }
}
