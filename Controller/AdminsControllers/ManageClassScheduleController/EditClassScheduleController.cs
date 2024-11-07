using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassScheduleController
{
    public class EditClassScheduleController
    {
        MyDbContext db; EditClassScheduleView view;
        public EditClassScheduleController(MyDbContext db) { this.db = db; view = new EditClassScheduleView(); }
        
    }
}
