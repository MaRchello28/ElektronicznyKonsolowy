using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageClassView
{
    public class ShowClassView
    {
        MyDbContext db;
        public ShowClassView(MyDbContext db) { this.db = db; }
        public void Show()
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Id"); table.AddColumn("Number+Letter"); table.AddColumn("Id Wychowawcy");

            var classes = db.StudentClasses.ToList();

            foreach (var cla in classes)
            {
                table.AddRow(cla.studentClassId.ToString(), $"{cla.number}{cla.letter}", cla.teacherId.ToString());
            }

            AnsiConsole.Render(table);
        }
    }
}
