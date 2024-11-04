using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.AdminViews.ManageSubjectViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageSubjectsController
{
    public class EditSubjectController
    {
        MyDbContext db; EditSubjectView view;
        public EditSubjectController(MyDbContext db) { this.db = db; view = new EditSubjectView(); }
        public void Edit()
        {
            string value;
            int idSubjectToEdit = view.SubjectToEdit();
            var sTE = db.Subjects.FirstOrDefault(x => x.subjectId == idSubjectToEdit);
            if (idSubjectToEdit == null)
            {
                SuccesAndErrorsView.ShowErrorMessage("Nie znaleziono przedmiotu!");
                return;
            }
            Subject editedSubject = new Subject(sTE.name);
            value = view.EditOption();
            sTE.name = value;
            view.ShowDifference(editedSubject, sTE);
            db.SaveChanges();
        }
    }
}
