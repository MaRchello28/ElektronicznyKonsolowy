using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageSessionViews
{
    public class DeleteSessionView
    {
        MyDbContext db;
        public DeleteSessionView(MyDbContext db) { this.db = db; }
        public int PutIndex()
        {
            int id;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj id do usunięcia: [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.Sessions.Any(c => c.sessionId == id))
                        {
                            AnsiConsole.MarkupLine("[red]Podane id nie jest w bazie[/]");
                        }
                        else
                        {
                            run = false;
                        }
                    }
                }

            }
            while (run);
            return id;
        }
        public int Agree()
        {
            var options = new[] {
            "Tak",
            "Nie",
            };
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Czy jesteś pewny, że chcesz usunąć tych użytkowników?")
                    .PageSize(10)
                    .AddChoices(options));

            return Array.IndexOf(options, selectedOption);
        }
    }
}
