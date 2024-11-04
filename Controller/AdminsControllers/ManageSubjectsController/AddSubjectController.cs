using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageSubjectViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController
{
    public class AddSubjectController
    {
        MyDbContext db; AddSubjectView view;
        public AddSubjectController(MyDbContext db) { this.db = db; view = new AddSubjectView(db); }
        public void CreateStudent()
        {
            string name;
            view.ShowCreateWindow();
            name = view.EnterName();
            Subject subject = new Subject(name);
            db.Subjects.Add(subject);
            db.SaveChanges();
            view.ShowCreatedSubject(name);
        }
    }
}
