using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageLessonViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageLessonControllers
{
    public class AddLessonController
    {
        MyDbContext db; AddLessonView view;
        public AddLessonController(MyDbContext db) { this.db = db; view = new AddLessonView(db); }
        public void CreateLesson()
        {
            int number; string description;string name;
            view.ShowCreateWindow();
            name = view.EnterName();
            number = view.EnterNumberOfLesson();
            description = view.EnterDescription();
            Lesson lesson = new Lesson(name, description, number);
            db.Lessons.Add(lesson);
            db.SaveChanges();
            view.ShowCreatedLesson(name,description,number);
        }
    }
}
