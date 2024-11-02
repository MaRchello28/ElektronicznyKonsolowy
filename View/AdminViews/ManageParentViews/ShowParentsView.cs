using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews
{
    public class ShowParentsView
    {
        MyDbContext db;
        public ShowParentsView(MyDbContext db) { this.db = db; }
        public void Show()
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Id"); table.AddColumn("email"); table.AddColumn("Numer Telofonu"); table.AddColumn("Name"); table.AddColumn("Surname");
            table.AddColumn("Login"); table.AddColumn("Password");

            var parents = db.Parents.ToList();

            foreach (var parent in parents)
            {
                table.AddRow(parent.parentId.ToString(), parent.email, parent.phoneNumber, parent.name, parent.surname, parent.login, parent.password);
            }

            AnsiConsole.Render(table);
        }
    }
}
