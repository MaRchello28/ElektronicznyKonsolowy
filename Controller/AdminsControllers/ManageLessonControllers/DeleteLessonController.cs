using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageLessonViews;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageLessonControllers
{
    public class DeleteLessonController
    {
        MyDbContext db; DeleteLessonView view;
        public DeleteLessonController(MyDbContext db) {  this.db = db; this.view = new DeleteLessonView(db); }
        public void Run()
        {
            int idToDelete = view.PutIndex();
            foreach (var lesson in db.Lessons)
            {
                if (lesson.lessonId == idToDelete)
                {
                    if (view.Agree() == 0) { db.Lessons.Remove(lesson); }
                    break;
                }
            }
            db.SaveChanges();
        }
    }
}
