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
            AnsiConsole.MarkupLine("[blue] Podaj ID planu lekcji : [/]");
            string value = Console.ReadLine();
            id = int.Parse(value);
            return id;
        }
        public int EnterSessionId()
        {
            int sessionsid;
            AnsiConsole.MarkupLine("[blue] Podaj ID sesji: [/]");
            string value = Console.ReadLine();
            sessionsid = int.Parse(value);
            return sessionsid;
        }


    }
}
