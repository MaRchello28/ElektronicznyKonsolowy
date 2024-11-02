using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews
{
    public class manageClassView
    {
        public manageClassView() { }
        public int ShowManageWindow()
        {
            var options = new[] {
            "Stworz klase",
            "Usun kalse",
            "Edytuj klase",
            "Wyswietl klasy",
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
