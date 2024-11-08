using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageClassView;
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
        public void Edit()
        {
            List<bool> choices; string value;
            int idClassSchedule = view.ClassScheduleToEdit();
            var sTE = db.ClassSchedules.FirstOrDefault(x => x.classScheduleId == idClassSchedule);
            if (idClassSchedule == null)
            {
                SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono studenta!");
                return;
            }
            ClassSchedule editedClassSchedule = new ClassSchedule(sTE.classScheduleId);
            choices = view.ChooseOptionsToEdit();
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i])
                {
                    value = view.EditOption(i);
                    if (i == 0) { sTE.studentClassId = int.Parse(value); }
                    else if (i == 1) {
                        var session = db.Sessions.FirstOrDefault(x => x.sessionId == int.Parse(value));
                        sTE.AddSession(session); }
                    else if (i == 2) {
                        var session = db.Sessions.FirstOrDefault(x => x.sessionId == int.Parse(value));
                        sTE.RemoveSession(session);
                    }
                }
            }
            db.SaveChanges();
        }

    }
}
