using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageClassScheduleViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassScheduleController
{
    public class AddSessionToClassScheduleController
    {
        MyDbContext db; AddSessionToClassScheduleView view;
        public AddSessionToClassScheduleController(MyDbContext db) { this.db = db; view = new AddSessionToClassScheduleView(); }
        public void addSessionsToSchedule()
        {

        }
    }
}
