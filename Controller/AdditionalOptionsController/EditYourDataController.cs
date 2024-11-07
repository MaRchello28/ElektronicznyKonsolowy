using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdditionalOptionsView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdditionalOptionsController
{
    public class EditYourDataController
    {
        MyDbContext db; Parent parent; Student student; Teacher teacher;
        private string login, password, name, surname, email, phoneNumber;
        private UserType userType;
        private EditYourDataView view = new EditYourDataView();
        string value;

        public EditYourDataController(Student student, MyDbContext db)
        {
            this.login = student.user.login;
            this.password = student.user.password;
            this.userType = UserType.Student;
            this.db = db;
        }

        public EditYourDataController(Teacher teacher, MyDbContext db)
        {
            this.login = teacher.user.login;
            this.password = teacher.user.password;
            this.email = teacher.email;
            this.userType = UserType.Teacher;
            this.db = db;
        }

        public EditYourDataController(Parent parent, MyDbContext db)
        {
            this.login = parent.user.login;
            this.password = parent.user.password;
            this.email = parent.email;
            this.phoneNumber = parent.phoneNumber;
            this.userType = UserType.Parent;
            this.db = db;
            this.parent = parent;
        }

        public void EditUser(Student student)
        {
            List<bool> choices = view.ChooseWhatToEdit(userType);
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i])
                {
                    value = view.EditOption(i);
                    if (i == 0) 
                    {
                        if (StaticFunctions.LoginExists(value, db)) { return; }
                        student.user.login = value;
                    }
                    else if (i == 1) { student.user.password = value; }
                }
            }
            db.SaveChanges();
        }
        public void EditUser(Parent parent)
        {
            List<bool> choices = view.ChooseWhatToEdit(userType);
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i])
                {
                    value = view.EditOption(i);
                    if (i == 0) { if (StaticFunctions.LoginExists(value, db)) { return; } parent.user.login = value; }
                    else if (i == 1) { parent.user.password = value; }
                    else if (i == 2) { parent.email = value; }
                    else if (i == 3) { parent.phoneNumber = value; }
                }
            }
            db.SaveChanges();
        }
        public void EditUser(Teacher teacher)
        {
            List<bool> choices = view.ChooseWhatToEdit(userType);
            for (int i = 0; i < choices.Count; i++)
            {
                if (choices[i])
                {
                    value = view.EditOption(i);
                    if (i == 0) { if (StaticFunctions.LoginExists(value, db)) { return; } teacher.user.login = value; }
                    else if (i == 1) { teacher.user.password = value; }
                    else if (i == 2) { teacher.email = value; }
                }
            }
            db.SaveChanges();
        }
    }
}
