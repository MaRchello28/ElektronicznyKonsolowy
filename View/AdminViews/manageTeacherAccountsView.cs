using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews
{
    public class manageTeacherAccountsView
    {
        public manageTeacherAccountsView() { }
        public int ShowOptions()
        {
            var options = new[] {
            "Dodaj nauczyciela",
            "Usun nauczyciela",
            "Edytuj nauczyciela",
            "Wyswietl nauczycieli",
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
