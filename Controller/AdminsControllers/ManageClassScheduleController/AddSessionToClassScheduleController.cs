using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.AdminViews.ManageClassScheduleViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassScheduleController
{
    public class AddSessionToClassScheduleController
    {
        MyDbContext db; AddSessionToClassScheduleView view;
        public AddSessionToClassScheduleController(MyDbContext db) { this.db = db; view = new AddSessionToClassScheduleView(); }
        public void addSessionsToSchedule(int id)
        {
            int sessonid;
            id = view.EnterID();
            sessonid = view.EnterSessionId();
            var sTE = db.Sessions.FirstOrDefault(x => x.sessionId == sessonid);
            if (sTE == null)
            {
                SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono sesjI!");
                return;
            }
            var CLA = db.ClassSchedules.FirstOrDefault(x =>x.classScheduleId == id);
            if(CLA == null)
            {
                SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono planu lekcji");
                return;
            }
            CLA.AddSession(sTE);
            db.SaveChanges();


        }
    }
}
