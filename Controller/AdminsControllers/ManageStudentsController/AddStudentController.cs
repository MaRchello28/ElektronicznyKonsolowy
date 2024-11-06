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
    public class AddStudentController
    {
        MyDbContext db; AddStudentView view;
        public AddStudentController(MyDbContext db) { this.db = db; view = new AddStudentView(db); }
        public void CreateStudent()
        {
            string name; string surname; string password; string login;
            int classId; int parentId; string value;
            view.ShowCreateWindow();
            name = view.EnterName();
            if (name == "") { ViewsForStaticFunctions.ValueIsNull(); return; }
            if (name.Length < 3) { ViewsForStaticFunctions.ErrorLength(); return; };
            surname = view.EnterSurname();
            if (surname == "") { ViewsForStaticFunctions.ValueIsNull(); return; }
            if (surname.Length < 3) { ViewsForStaticFunctions.ErrorLength(); return; };
            login = view.CreateDefaultLogin(name, surname);
            password = view.EnterDefaultPassword();
            value = view.EnterClassId();
            if (int.TryParse(value, out classId) == false) { ViewsForStaticFunctions.BadParse(value); return; }
            value = view.EnterParentId();
            if (int.TryParse(value, out parentId) == false) { ViewsForStaticFunctions.BadParse(value); return; }
            User user = new User(name,surname,login,password);
            Student student = new Student(user, classId, parentId);
            db.Students.Add(student);
            db.SaveChanges();
            view.ShowCreatedStudent(name,surname,login,password,classId.ToString(),parentId.ToString());
        }
    }
}
