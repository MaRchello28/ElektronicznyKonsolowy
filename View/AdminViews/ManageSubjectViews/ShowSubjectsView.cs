using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageSubjectViews
{
    public class ShowSubjectsView
    {
        MyDbContext db;
        public ShowSubjectsView(MyDbContext db) { this.db = db; }
        public void Show()
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Id"); table.AddColumn("Nazwa");

            var subjects = db.Subjects.ToList();

            foreach (var subject in subjects)
            {
                table.AddRow(subject.subjectId.ToString(), subject.name);
            }

            AnsiConsole.Render(table);
        }
    }
}
