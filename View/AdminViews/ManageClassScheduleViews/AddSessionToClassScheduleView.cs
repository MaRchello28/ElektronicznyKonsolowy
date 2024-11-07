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
        

    }
}
