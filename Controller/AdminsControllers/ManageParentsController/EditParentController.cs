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
    public class EditParentController
    {
        MyDbContext db; EditParentView view; int idParentToEdit;
        User user = new User("", "", "", "");
        public EditParentController(MyDbContext db) { this.db = db; view = new EditParentView(); }
        public void Edit()
        {
            List<bool> choices; string value;
            idParentToEdit = view.ParentToEdit();
            var sTE = db.Parents.FirstOrDefault(x => x.parentId == idParentToEdit);
            if (idParentToEdit == null)
            {
                SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono studenta!");
                return;
            }
            Parent editedParent = new Parent(sTE.email, sTE.phoneNumber, new User(sTE.name, sTE.surname, sTE.login, sTE.password));
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
                    else if (i == 5) { sTE.phoneNumber = value; }
                }
            }
            view.ShowDifference(editedParent, sTE);
            db.SaveChanges();
        }
    }
}
