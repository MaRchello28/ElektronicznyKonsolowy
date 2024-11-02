using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.AdminViews.ManageTeachersView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageTeachersControlle
{
    public class EditTeacherController
    {
        MyDbContext db; EditTeacherView view;
        public EditTeacherController(MyDbContext db) { this.db = db; view = new EditTeacherView(); }
        public void Edit()
        {
            List<bool> choices; string value;
            int idTeacherToEdit = view.StudentToEdit();
            var sTE = db.Teachers.FirstOrDefault(x => x.teacherId == idTeacherToEdit);
            if (idTeacherToEdit == null)
            {
                SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono studenta!");
                return;
            }
            Teacher editedStudent = new Teacher(sTE.email, new User(sTE.user.name, sTE.user.surname, sTE.user.login, sTE.user.password));
            choices = view.ChooseOptionsToEdit();
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i])
                {
                    value = view.EditOption(i);
                    if (i == 0) { sTE.user.name = value; }
                    else if (i == 1) { sTE.user.surname = value; }
                    else if (i == 2) { sTE.user.login = value; }
                    else if (i == 3) { sTE.user.password = value; }
                    else if (i == 4) { sTE.email = value; }
                }
            }
            view.ShowDifference(editedStudent, sTE);
            db.SaveChanges();
        }
    }
}
