using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews
{
    public class ShowClassScheduleView
    {
        MyDbContext db;
        public ShowClassScheduleView(MyDbContext db) { this.db = db; }
        public void Show()
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Id");table.AddColumn("Id Klasy");

            var classes = db.ClassSchedules.ToList();

            foreach (var cla in classes)
            {
                table.AddRow(cla.classScheduleId.ToString(), cla.studentClassId.ToString());
            }

            AnsiConsole.Render(table);
        }
    }
}
