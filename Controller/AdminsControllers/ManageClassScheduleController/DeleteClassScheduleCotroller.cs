using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageSubjectViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassScheduleController
{
    public class DeleteClassScheduleCotroller
    {
        MyDbContext db; DeleteClassScheduleView view;
        public DeleteClassScheduleCotroller(MyDbContext db)
        {
            this.db = db; this.view = new DeleteClassScheduleView(db);
        }
        public void Run()
        {
            int idToDelete = view.PutIndex();
            var classScheduleToDelete = db.ClassSchedules.FirstOrDefault(t => t.classScheduleId == idToDelete);

            if (classScheduleToDelete != null && view.Agree() == 0)
            {
                db.ClassSchedules.Remove(classScheduleToDelete);
            }
            db.SaveChanges();
        }
    }
}
