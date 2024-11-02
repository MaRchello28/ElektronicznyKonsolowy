using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageTeachersView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageTeachersControlle
{
    public class DeleteTeacherController
    {
        MyDbContext db; DeleteTeacherView1 view1; DeleteTeacherView2 view2;
        public DeleteTeacherController(MyDbContext db) 
        { 
            this.db = db; view1 = new DeleteTeacherView1(db);
        }
        public void Run()
        {
            int choose = view1.StartDeleteWindow();
            switch (choose)
            {
                case 0:
                    {
                        var selectedTeachers = view1.FirstDeleteWindow();
                        if (view1.Agree() == 0)
                        {
                            foreach (var displayString in selectedTeachers)
                            {
                                var teacherId = displayString.Split(' ')[0];
                                var teacherToDelete = db.Teachers.FirstOrDefault(s => s.teacherId.ToString() == teacherId);

                                if (teacherToDelete != null)
                                {
                                    db.Teachers.Remove(teacherToDelete);
                                }
                            }
                            db.SaveChanges();
                        }
                        break;
                    }
                case 1:
                    {
                        this.view2 = new DeleteTeacherView2();
                        int idToDelete = view2.PutIndex();
                        var teacherToDelete = db.Teachers.FirstOrDefault(t => t.teacherId == idToDelete);

                        if (teacherToDelete != null && view1.Agree() == 0)
                        {
                            db.Teachers.Remove(teacherToDelete);
                        }
                        db.SaveChanges();
                        break;
                    }
            }
        }
    }
}
