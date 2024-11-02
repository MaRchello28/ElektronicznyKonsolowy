using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController
{
    public class DeleteParentController
    {
        MyDbContext db; DeleteParentView1 view1; DeleteParentView2 view2;
        public DeleteParentController(MyDbContext db)
        {
            this.db = db; this.view1 = new DeleteParentView1(db);
        }
        public void Run()
        {
            int choose = view1.StartDeleteWindow();
            switch (choose)
            {
                case 0:
                    {
                        var selectedStudents = view1.FirstDeleteWindow();
                        if (view1.Agree() == 0)
                        {
                            foreach (var displayString in selectedStudents)
                            {
                                var parentId = displayString.Split(' ')[0];

                                var parentToDelete = db.Parents.FirstOrDefault(s => s.parentId.ToString() == parentId);

                                if (parentToDelete != null)
                                {
                                    db.Parents.Remove(parentToDelete);
                                }
                            }
                            db.SaveChanges();
                        }
                        break;
                    }
                case 1:
                    {
                        this.view2 = new DeleteParentView2(db);
                        int idToDelete = view2.PutIndex();
                        foreach (var parent in db.Parents)
                        {
                            if (parent.parentId == idToDelete)
                            {
                                if (view1.Agree() == 0) { db.Parents.Remove(parent); }
                                break;
                            }
                        }
                        db.SaveChanges();
                        break;
                    }
            }
        }
    }
}
