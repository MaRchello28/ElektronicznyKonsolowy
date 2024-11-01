using ElektronicznyKonsolowy.Migrations;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController
{
    public class EditStudentController
    {
        MyDbContext db; EditStudentView view; int idStudentToEdit;
        User user = new User("","","","");
        public EditStudentController(MyDbContext db) { this.db = db; view = new EditStudentView(); }
        public void Edit()
        {
            List<bool> choices; string value;
            idStudentToEdit = view.StudentToEdit();
            var sTE = db.Students.FirstOrDefault(x => x.studentId == idStudentToEdit);
            if (idStudentToEdit == null)
            {
                SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono studenta!");
                return;
            }
            Student editedStudent = new Student(new User(sTE.name, sTE.surname, sTE.login, sTE.password), sTE.studentClassId, sTE.parentId);
            choices = view.ChooseOptionsToEdit();
            for(int i=0; i<choices.Count; i++)
            {
                if(choices[i])
                {
                    value = view.EditOption(i);
                    if (i == 0) { sTE.user.name = value; }
                    else if( i == 1) { sTE.user.surname = value; }
                    else if( i == 2) { sTE.user.login = value; }
                    else if( i == 3) { sTE.user.password = value; }
                    else if( i == 4) { int.TryParse(value, out int newValue); sTE.studentClassId = newValue; }
                    else if (i == 5) { int.TryParse(value, out int newValue); sTE.parentId = newValue; }
                }
            }
            view.ShowDifference(editedStudent, sTE);
            db.SaveChanges();
        }
    }
}
