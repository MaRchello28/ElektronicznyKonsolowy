using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews
{
    public class ShowStudentsView
    {
        public ShowStudentsView() { }
        public void Show()
        {
            var table = new Table();
            table.Border(TableBorder.HeavyEdge);
            table.AddColumn("Id"); table.AddColumn("ClassId"); table.AddColumn("ParentId"); table.AddColumn("Name"); table.AddColumn("Surname");
            table.AddColumn("Login"); table.AddColumn("Password");
        }
    }
}
