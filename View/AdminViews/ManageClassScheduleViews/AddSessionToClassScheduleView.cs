using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageClassScheduleViews
{
    public class AddSessionToClassScheduleView
    {
        
        public AddSessionToClassScheduleView() { }
        public void addSessionsToSchedule()
        {
            var header = new FigletText("Okno dodawania lekcji").Centered().Color(Color.Green3);
            AnsiConsole.Render(header);
        }
        public int EnterID()
        {
            int id;
            bool run=true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj ID planu lekcji : [/]");
                string value = Console.ReadLine();
                id = int.Parse(value);
                if(id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.ClassSchedules.Any(c => c.classScheduleId == id))
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
            while(run);
            return id;
        }
        public int EnterSessionId()
        {
            int sessionsid;
            bool run=true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj ID sesji: [/]");
                string value = Console.ReadLine();
                sessionsid = int.Parse(value);
                if(sessionsid <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.Sessions.Any(c => c.sessionId == sessionsid))
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
            return sessionsid;
        }


    }
}
