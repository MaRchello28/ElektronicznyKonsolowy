using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassScheduleController
{
    public class AddClassScheduleController
    {
        MyDbContext db; AddClassScheduleView view; AddSessionToClassScheduleController add;
        public AddClassScheduleController(MyDbContext db) { this.db = db; view = new AddClassScheduleView(db); }
        public void CreateClassSchedule()
        {
            int id;
            view.ShowCreateWindow();
            id = view.EnterClassId();
            ClassSchedule classSchedule = new ClassSchedule(id);
            db.ClassSchedules.Add(classSchedule);
            db.SaveChanges();
            view.ShowCreatedClassSchedule(id);
            int opt = view.AddSessions();
            if (opt == 0)
            {
                add.addSessionsToSchedule(opt,id);
            }

        }
        
    }
}
