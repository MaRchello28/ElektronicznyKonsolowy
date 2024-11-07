using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageSessionViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageSessionControllers
{
    public class ShowSessionController
    {
        MyDbContext db; ShowSessionView view;
        public ShowSessionController(MyDbContext db)
        {
            this.db = db;
            view = new ShowSessionView(db);
        }
    }
}
