using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController
{
    public class AddParentController
    {
        MyDbContext db; AddParentView view;
        public AddParentController(MyDbContext db) { this.db = db; view = new AddParentView(db); }
        public void CreateParent()
        {
            string name; string surname; string password; string login;
            string phoneNumber; string email;
            name = view.EnterName();
            surname = view.EnterSurname();
            login = view.CreateDefaultLogin(name, surname);
            password = view.EnterDefaultPassword();
            phoneNumber = view.EnterPhoneNumber();
            email = view.EnterEmail();
            User user = new User(name, surname, login, password);
            Parent parent = new Parent(email, phoneNumber, user);
            db.Parents.Add(parent);
            db.SaveChanges();
            view.ShowCreatedParent(name, surname, login, password, email, phoneNumber);
        }
    }
}
