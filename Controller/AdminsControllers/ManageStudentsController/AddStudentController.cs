using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController
{
    public class AddStudentController
    {
        MyDbContext db; AddStudentView view;
        public AddStudentController(MyDbContext db) { this.db = db; view = new AddStudentView(db); }
        public void CreateStudent()
        {
            string name; string surname; string password; string login;
            int classId; int parentId;
            view.ShowCreateWindow();
            name = view.EnterName();
            surname = view.EnterSurname();
            login = view.CreateDefaultLogin(name, surname);
            password = view.EnterDefaultPassword();
            classId = view.EnterClassId();
            parentId = view.EnterParentId();
            User user = new User(name,surname,login,password);
            Student student = new Student(user, classId, parentId);
            db.Students.Add(student);
            db.SaveChanges();
            view.ShowCreatedStudent(name,surname,login,password,classId.ToString(),parentId.ToString());
        }
    }
}
