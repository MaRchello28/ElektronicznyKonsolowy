using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews
{
    public class manageClassScheduleView
    {
        public manageClassScheduleView() { }
        public int ShowManageWindow()
        {
            var options = new[] {
            "Dodaj plan zajęć",
            "Usun plan zajęć",
            "Edytuj plan zajęć",
            "Wyswietl plany zajęć",
            "Wyświetl konkretny plan zajęć",
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
