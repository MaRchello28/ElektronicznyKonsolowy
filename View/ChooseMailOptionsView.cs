using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View
{
    public class ChooseMailOptionsView
    {
        public ChooseMailOptionsView() { }
        public int ShowOptions()
        {
            var choices = new[] { "Wyslij wiadomość", "Wyświetl wiadomości" };

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz, jedną z opcji: ")
                    .PageSize(10)
                    .AddChoices(choices));

            return Array.IndexOf(choices, selectedOption);
        }
    }
}
