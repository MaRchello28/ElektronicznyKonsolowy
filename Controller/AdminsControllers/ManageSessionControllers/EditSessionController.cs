using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElektronicznyKonsolowy.View.AdminViews.ManageSessionViews;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageSessionControllers
{
    public class EditSessionController
    {
        MyDbContext db; EditSessionView view; int idSessionToEdit;
        public EditSessionController(MyDbContext db) { this.db = db; view = new EditSessionView(); }
        public void Edit()
        {
            TimeSpan hour, hourStop;
            List<bool> choices; string value;
            idSessionToEdit = view.SessionToEdit();
            var sTE = db.Sessions.FirstOrDefault(x => x.sessionId == idSessionToEdit);
            if (idSessionToEdit == null)
            {
                SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono sesji!");
                return;
            }
            Session editedSession = new Session(sTE.subjectId, sTE.teacherId, sTE.dayOfTheWeek, sTE.hourFrom, sTE.hourTo);
            choices = view.ChooseOptionsToEdit();
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i])
                {
                    value = view.EditOption(i);
                    if (i == 0) { sTE.subjectId = int.Parse(value); }
                    else if (i == 1) { sTE.teacherId = int.Parse(value); }
                    else if (i == 2) { sTE.dayOfTheWeek = (DayOfWeek)int.Parse(value+1); }
                    else if (i == 3) {
                        switch (int.Parse(value))
                        {
                            case 0:
                                {
                                    hour = new TimeSpan(8, 0, 0);
                                    hourStop = new TimeSpan(8, 45, 0);
                                    break;
                                }
                            case 1:
                                {
                                    hour = new TimeSpan(8, 55, 0);
                                    hourStop = new TimeSpan(9, 40, 0);
                                    break;
                                }
                            case 2:
                                {
                                    hour = new TimeSpan(9, 50, 0);
                                    hourStop = new TimeSpan(10, 35, 0);
                                    break;
                                }
                            case 3:
                                {
                                    hour = new TimeSpan(10, 55, 0);
                                    hourStop = new TimeSpan(11, 40, 0);
                                    break;
                                }
                            case 4:
                                {
                                    hour = new TimeSpan(11, 50, 0);
                                    hourStop = new TimeSpan(12, 35, 0);
                                    break;
                                }
                            case 5:
                                {
                                    hour = new TimeSpan(12, 45, 0);
                                    hourStop = new TimeSpan(13, 30, 0);
                                    break;
                                }
                            case 6:
                                {
                                    hour = new TimeSpan(13, 40, 0);
                                    hourStop = new TimeSpan(14, 25, 0);
                                    break;
                                }
                            case 7:
                                {
                                    hour = new TimeSpan(14, 35, 0);
                                    hourStop = new TimeSpan(15, 20, 0);
                                    break;
                                }
                            case 8:
                                {
                                    hour = new TimeSpan(15, 30, 0);
                                    hourStop = new TimeSpan(16, 15, 0);
                                    break;
                                }
                            case 9:
                                {
                                    hour = new TimeSpan(16, 25, 0);
                                    hourStop = new TimeSpan(17, 20, 0);
                                    break;
                                }
                            default: { return; }
                        }
                        sTE.hourFrom = hour;
                        sTE.hourTo = hourStop;
                    }
                }
            }
            view.ShowDifference(editedSession, sTE);
            db.SaveChanges();
        }
    }
}
