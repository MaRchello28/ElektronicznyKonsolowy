using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View
{
    public class SendMailView
    {
        MyDbContext db;
        public SendMailView(MyDbContext db) 
        {
            this.db = db;
        }
        public void StartCreateNewMessage(int selectedIndex, string[] labels, string[] inputs)
        {
            AnsiConsole.Clear();

            AnsiConsole.MarkupLine("[bold blue]Tworzenie nowej wiadomości. Naciśnij Spacje, żeby wysłać[/]");
            for (int i = 0; i < labels.Length; i++)
            {
                if (i == selectedIndex)
                {
                    AnsiConsole.Markup("[bold yellow]> [/]");
                }
                else
                {
                    AnsiConsole.Markup("  ");
                }
                AnsiConsole.MarkupLine($"{labels[i]}[italic]{(inputs[i] ?? "")}[/]");
            }
        }
        public void PressEnter(int selectedIndex, string[] labels, string[] inputs)
        {
            AnsiConsole.MarkupLine($"[grey]Wpisz {labels[selectedIndex]}[/]");
            inputs[selectedIndex] = Console.ReadLine();
        }
    }
}
