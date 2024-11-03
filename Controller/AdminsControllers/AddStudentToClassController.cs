using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers
{
    public class AddStudentToClassController
    {
        MyDbContext db; AddStudentToClassView view;
        public AddStudentToClassController(MyDbContext db) { this.db = db; view = new AddStudentToClassView(db); }
        public void Run()
        {
            int classId = view.ChooseClass();
            StudentClass studentClass = db.StudentClasses.SingleOrDefault(c => c.studentClassId == classId);
            var selectedStudents = view.ChooseStudents();
            foreach ( var student in selectedStudents )
            {
                student.studentClassId = classId;
            }
            db.SaveChanges();
        }
    }
}
