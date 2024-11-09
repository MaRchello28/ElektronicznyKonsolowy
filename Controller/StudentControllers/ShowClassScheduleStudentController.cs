using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews;
using ElektronicznyKonsolowy.View.StudentViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.StudentControllers
{
    public class ShowClassScheduleStudentController
    {
        MyDbContext db; ShowClassScheduleStudentView view;
        public ShowClassScheduleStudentController(MyDbContext db) { this.db = db; this.view = new ShowClassScheduleStudentView(db); }
    }
}
