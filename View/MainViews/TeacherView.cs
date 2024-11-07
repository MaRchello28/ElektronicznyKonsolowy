using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.MainViews
{
    public class TeacherView
    {
        public TeacherView() { }
        public int ShowMainMenu()
        {
            var options = new[]
            {
                "Edytuj swoje dane",
                "Wstaw oceny",
                "Otwórz skrzynkę pocztową",
                "Dodawanie plikow do poszczegolnych lekcji",
                "Tworzenie lekcji z tresciami ksztalcenia",
                "Tworzenie quizu",
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
