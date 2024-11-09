using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.MainViews
{
    public class ParentView
    {
        public ParentView() { }
        public int ShowMainMenu()
        {
            var options = new[]
            {
                "Edytuj swoje dane",
                "Przegladaj oceny swoich dzieci",
                "Otwórz maila",
                "Przeglądaj plany zajęć dzieci",
                "Wyloguj"
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
