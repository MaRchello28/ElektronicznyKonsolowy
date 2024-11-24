using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageClassView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassController
{
    public class AddClassController
    {
        MyDbContext db; AddClassView view;
        public AddClassController(MyDbContext db) { this.db = db; view = new AddClassView(); }
        public void CreateClass()
        {
            string letter; string number; int teacherId;            number = view.EnterNumber();
            letter = view.EnterLetter();
            teacherId = view.EnterTeacherId();
            StudentClass sc = new StudentClass(number, letter, teacherId);
            db.StudentClasses.Add(sc);
            db.SaveChanges();
            view.ShowCreatedStudent(number, letter, teacherId);
        }
    }

}
