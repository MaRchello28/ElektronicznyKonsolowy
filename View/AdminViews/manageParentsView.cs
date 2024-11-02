using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews
{
    public class manageParentsView
    {
        public manageParentsView() { }
        public int ShowManageWindow()
        {
            var options = new[] {
            "Dodaj rodzica",
            "Usun rodzica",
            "Edytuj rodzica",
            "Wyswietl rodzicow",
            "Cofnij"
            };
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Co chcesz wykonać?")
                    .PageSize(10)
                    .AddChoices(options));

            return Array.IndexOf(options, selectedOption);
        }
    }
}
