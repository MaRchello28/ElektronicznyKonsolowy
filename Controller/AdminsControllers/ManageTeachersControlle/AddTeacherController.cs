using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageTeachersView;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageTeachersControlle
{
    public class AddTeacherController
    {
        MyDbContext db; AddTeacherView ATV;
        public AddTeacherController(MyDbContext db) 
        { 
            this.db = db; ATV = new AddTeacherView();
        }
        public void CreateTeacher()
        {
            string name; string surname; string password; string login; string email;
            name = ATV.EnterName();
            surname = ATV.EnterSurname();
            login = ATV.CreateDefaultLogin(name, surname);
            password = ATV.EnterDefaultPassword();
            email = ATV.EnterEmail();
            User user = new User(name, surname, login, password);
            Teacher teacher = new Teacher(email, user);
            db.Teachers.Add(teacher);
            db.SaveChanges();
            ATV.ShowCreatedStudent(teacher);
        }
    }
}
