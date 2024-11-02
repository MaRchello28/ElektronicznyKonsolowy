using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.AdminViews.ManageClassView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassController
{
    public class EditClassController
    {
        MyDbContext db; EditClassView editClassView;
        public EditClassController(MyDbContext db) { this.db = db; editClassView = new EditClassView(); }
        public void Edit()
        {
            List<bool> choices; string value;
            int idClassToEdit = editClassView.ClassToEdit();
            var sTE = db.StudentClasses.FirstOrDefault(x => x.studentClassId == idClassToEdit);
            if (idClassToEdit == null)
            {
                SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono studenta!");
                return;
            }
            StudentClass editedClass = new StudentClass(sTE.number, sTE.letter, sTE.teacherId);
            choices = editClassView.ChooseOptionsToEdit();
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i])
                {
                    value = editClassView.EditOption(i);
                    if (i == 0) { sTE.number = value; }
                    else if (i == 1) { sTE.letter = value; }
                    else if (i == 2) { int.TryParse(value, out int newValue); sTE.teacherId = newValue; }
                }
            }
            editClassView.ShowDifference(editedClass, sTE);
            db.SaveChanges();
        }
    }
}
