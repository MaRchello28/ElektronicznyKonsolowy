using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElektronicznyKonsolowy.View.AdminViews.ManageLessonViews;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageLessonControllers
{
    //public class EditLessonController
    //{
    //    MyDbContext db; EditLessonView view; int idLessonToEdit;
    //    Lesson lesson = new Lesson("", "", 0, 0);
    //    public EditLessonController(MyDbContext db) { this.db = db; view = new EditLessonView(); }
    //    public void Edit()
    //    {
    //        List<bool> choices; string value;
    //        idLessonToEdit = view.LessonToEdit();
    //        var sTE = db.Lessons.FirstOrDefault(x => x.lessonId == idLessonToEdit);
    //        if (idLessonToEdit == null)
    //        {
    //            SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono studenta!");
    //            return;
    //        }
    //        Lesson editedLesson = new Lesson(sTE.name, sTE.description,sTE.nuberOfLesson);
    //        choices = view.ChooseOptionsToEdit();
    //        for (int i = 0; i < choices.Count; i++)
    //        {
    //            if (choices[i])
    //            {
    //                value = view.EditOption(i);
    //                if (i == 0) { sTE.name = value; }
    //                else if (i == 1) { sTE.description = value; }
    //                else if (i == 2) { sTE.nuberOfLesson = int.Parse(value); }
    //            }
    //        }
    //        view.ShowDifference(editedLesson, sTE);
    //        db.SaveChanges();
    //    }
    //}
}
