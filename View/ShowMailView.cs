using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View
{
    public class ShowMailView
    {
        MyDbContext dbContext;
        public ShowMailView(MyDbContext db) 
        { 
            this.dbContext = db;
        }
        public void ShowMails(ICollection<Mail> messages)
        {
            string[] options = new string[messages.Count];
            int i = 0;
            foreach (var message in messages)
            {
                options[i] = "Od: " + message.from + " Do: " + message.to + " Temat: " + message.subject;
                i++;
            }

            if(options.Length > 0)
            {
                var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Wiadomości, które napisałeś lub otrzymałeś: ")
                .PageSize(10)
                .AddChoices(options)
                );
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Nie masz jeszcze żadnych wiadomości :([/]");
            }
        }
    }
}
