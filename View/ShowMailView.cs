using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View
{
    public class ShowMailView
    {
        MyDbContext db;
        public ShowMailView(MyDbContext db) 
        { 
            this.db = db;
        }
        public int ShowMails(ICollection<Mail> messages, string loginFrom)
        {
            var sortedMessages = messages.OrderByDescending(m => m.send).ToList();

            string[] options = new string[messages.Count + 1];
            int i = 0;

            foreach (var message in sortedMessages)
            {
                string prefix = (message.read == false && loginFrom == message.to) ? "*" : " ";

                options[i] = prefix + "[red]Od: [/]" + message.from +
                             "[red] Do: [/]" + message.to +
                             "[red] Temat: [/]" + message.subject +
                             "[red] Data wysłania: [/]" + message.send;
                i++;
            }

            options[messages.Count] = "Powrót";

            if (options.Length > 0)
            {
                var selection = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Cofnij lub wybierz wiadomość, którą napisałeś lub otrzymałeś: ")
                .PageSize(10)
                .AddChoices(options)
                );

                int index = Array.IndexOf(options, selection);

                //Te poniżej zapisuje do bazy w widoku a powinno być w kontrolerze
                //Tu działą tam nie :(

                if (index >= 0 && index < messages.Count)
                {
                    var selectedMessage = sortedMessages[index];
                    if (!selectedMessage.read)
                    {
                        selectedMessage.read = true;

                        db.SaveChanges();
                    }
                }
                return index;
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Nie masz jeszcze żadnych wiadomości :([/]");
                return -1;
            }
        }
        public void ShowMailBody(Mail message)
        {
            if (message == null)
            {
                Console.WriteLine("Wiadomość jest null");
                return;
            }
            var From = new Table();
            var Data = new Table();
            var Subject = new Table();
            var Body = new Table();

            From.AddColumn("[red]Od: [/]" + message.from + "[red] Do: [/]" + message.to).Width(50);
            Data.AddColumn("Data wysłania: " + message.send.ToString("G")).Width(50);
            Subject.AddColumn("Temat: " + message.subject).Width(50);
            Body.AddColumn("Treść: " + message.body).Width(50);

            AnsiConsole.Render(From);
            AnsiConsole.Render(Data);
            AnsiConsole.Render(Subject);
            AnsiConsole.Render(Body);
            //ElżbietaElżbietaElżbietaElżbieta
        }
    }
}
