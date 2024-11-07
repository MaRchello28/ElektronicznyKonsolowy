using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageSessionViews
{
    public class ShowSessionView
    {
        MyDbContext db;
        public ShowSessionView(MyDbContext db) { this.db = db; }
        public void Show()
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Id"); table.AddColumn("Lekcja"); table.AddColumn("Nauczyciel Id"); table.AddColumn("Dzień"); table.AddColumn("Godzina od");
            table.AddColumn("Godzina do");

            var sessions = db.Sessions.ToList();

            foreach (var session in sessions)
            {
                table.AddRow(session.sessionId.ToString(),session.subjectId.ToString(), session.teacherId.ToString(), session.dayOfTheWeek.ToString(), session.hourFrom.ToString(), session.hourTo.ToString());
            }

            AnsiConsole.Render(table);
        }
    }
}
