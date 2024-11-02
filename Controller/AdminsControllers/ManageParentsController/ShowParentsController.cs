using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController
{
    public class ShowParentsController
    {
        MyDbContext db; ShowParentsView ShowParentsView;
        public ShowParentsController(MyDbContext db)
        { 
            this.db = db;
            ShowParentsView = new ShowParentsView(db);
        }
    }
}
