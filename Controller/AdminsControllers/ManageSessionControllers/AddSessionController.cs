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
    public class AddSessionController
    {
        MyDbContext db; AddSessionView view;
        public AddSessionController(MyDbContext db) { this.db = db; view = new AddSessionView(db); }
        public void CreateSession()
        {
            int subjectid; int teacher; int dzien; int godzina;
            TimeSpan hour; TimeSpan hourStop; DayOfWeek dayOfWeek;
            view.ShowCreateWindow();
            subjectid = view.EnterSubject();
            teacher = view.EnterTeacher();
            dzien = view.EnterDzien();
            dzien++;
            dayOfWeek = (DayOfWeek)dzien;
            godzina = view.EnterHourFrom();
            switch(godzina)
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
                        hour = new TimeSpan(10 ,55, 0);
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
                default: {return;}
            }
            Session session = new Session(subjectid, teacher, dayOfWeek, hour, hourStop);
            db.Sessions.Add(session);
            db.SaveChanges();
            view.ShowCreatedSession(subjectid, teacher, dzien, hour, hourStop);
        }
    }
}
