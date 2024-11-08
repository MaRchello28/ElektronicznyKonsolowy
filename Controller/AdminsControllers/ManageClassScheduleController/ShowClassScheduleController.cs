using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassScheduleController
{
    public class ShowClassScheduleController
    {
        MyDbContext db; ShowClassScheduleView view;
        ShowClassScheduleController(MyDbContext db) { this.db = db;this.view = new ShowClassScheduleView(db); }
    }
}
