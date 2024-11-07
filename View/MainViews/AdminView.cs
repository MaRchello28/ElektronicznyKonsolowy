using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.MainViews
{
    public class AdminView
    {
        public AdminView()
        {

        }
        public int ShowMainMenu()
        {
            var options = new[] {
            "Zarządzanie kontami uczniów",
            "Zarządzanie kontami nauczycieli",
            "Zarządzanie kontami rodziców",
            "Zarządzanie klasami",
            "Przypisz uczniów do odpowiedniej klasy",
            "Zarządzaj przedmiotami",
            "Zardządzanie sesjami",
            "Przypisz plan zajeć do klasy",
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
