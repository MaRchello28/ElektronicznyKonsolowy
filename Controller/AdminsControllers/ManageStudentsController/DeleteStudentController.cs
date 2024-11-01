using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController
{
    public class DeleteStudentController
    {
        MyDbContext db; DeleteStudentView1 view1; DeleteStudentView2 view2;
        public DeleteStudentController(MyDbContext db)
        {
            this.db = db; this.view1 = new DeleteStudentView1(db);
        }
        public void Run()
        {
            int choose = view1.StartDeleteWindow();
            switch(choose)
            {
                case 0:
                    {
                        var selectedStudents = view1.FirstDeleteWindow();
                        if(view1.Agree() == 0)
                        {
                            foreach (var displayString in selectedStudents)
                            {
                                var studentId = displayString.Split(' ')[0];

                                var studentToDelete = db.Students.FirstOrDefault(s => s.studentId.ToString() == studentId);

                                if (studentToDelete != null)
                                {
                                    db.Students.Remove(studentToDelete);
                                }
                            }
                            db.SaveChanges();
                        }
                        break;
                    }
                case 1:
                    {
                        this.view2 = new DeleteStudentView2(); break;
                    }
            }
        }
    }
}
