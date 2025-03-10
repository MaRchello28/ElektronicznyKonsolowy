﻿using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.MainViews
{
    public class StudentView
    {
        public StudentView() { }
        public int ShowMainMenu()
        {
            var options = new[]
            {
                "Edytuj swoje dane",
                "Przegladaj swoje oceny",
                "Otwórz skrzynkę pocztową",
                "Zobacz plan zajęc",
                "Przeglądaj obecność",
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
