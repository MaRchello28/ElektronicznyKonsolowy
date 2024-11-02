using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageClassView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassController
{
    public class DeleteClassController
    {
        MyDbContext db; DeleteClassView1 view1; DeleteClassView2 view2;
        public DeleteClassController(MyDbContext db) { this.db = db; view1 = new DeleteClassView1(db); }
        public void Run()
        {
            int choose = view1.StartDeleteWindow();
            switch (choose)
            {
                case 0:
                    {
                        var selectedClasses = view1.FirstDeleteWindow();
                        if (view1.Agree() == 0)
                        {
                            foreach (var displayString in selectedClasses)
                            {
                                var classId = displayString.Split(' ')[0];

                                var classToDelete = db.StudentClasses.FirstOrDefault(s => s.studentClassId.ToString() == classId);

                                if (classToDelete != null)
                                {
                                    db.StudentClasses.Remove(classToDelete);
                                }
                            }
                            db.SaveChanges();
                        }
                        break;
                    }
                case 1:
                    {
                        this.view2 = new DeleteClassView2();
                        int idToDelete = view2.PutIndex();
                        foreach (var cla in db.StudentClasses)
                        {
                            if (cla.studentClassId == idToDelete)
                            {
                                if (view1.Agree() == 0) { db.StudentClasses.Remove(cla); }
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
