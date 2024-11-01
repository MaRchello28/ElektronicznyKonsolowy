using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController
{
    public class ShowStudentsController
    {
        MyDbContext db; ShowStudentsView ShowStudentsView;
        public ShowStudentsController(MyDbContext db)
        { 
            this.db = db;
            ShowStudentsView = new ShowStudentsView(db);
        }
    }
}
