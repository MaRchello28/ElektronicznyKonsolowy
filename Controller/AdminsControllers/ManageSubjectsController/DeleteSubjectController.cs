using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageSubjectViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageSubjectsController
{
    public class DeleteSubjectController
    {
        MyDbContext db; DeleteSubjectView view;
        public DeleteSubjectController(MyDbContext db)
        {
            this.db = db; this.view = new DeleteSubjectView(db);
        }
        public void Run()
        {
            int idToDelete = view.PutIndex();
            var subjectToDelete = db.Subjects.FirstOrDefault(t => t.subjectId == idToDelete);

            if (subjectToDelete != null && view.Agree() == 0)
            {
                db.Subjects.Remove(subjectToDelete);
            }
            db.SaveChanges();
        }
    }
}
