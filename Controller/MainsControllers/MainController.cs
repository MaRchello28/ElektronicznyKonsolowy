using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.MainViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.MainsControllers
{
    public class MainController
    {
        MyDbContext db; MainView mainView;
        AdminController adminController; StudentController studentController;
        TeacherController teacherController; ParentController parentController;
        public MainController(MyDbContext db, MainView mainView) { this.db = db; this.mainView = mainView; adminController = new AdminController(db);
        }
        public void Run()
        {
            bool run = true; int userType = 5;
            while (run)
            {
                mainView.OnProgramStart();
                string login = mainView.GetLogin();
                string password = mainView.GetPassword();
                userType = Login(login, password);
                switch (userType)
                {
                    case 1:
                        {
                            adminController.Run(); break;
                        }
                    case 2:
                        {
                            Student student = db.Students.FirstOrDefault(a => a.user.login == login);
                            studentController = new StudentController(student, db);
                            studentController.Run(student); break;
                        }
                    case 3:
                        {
                            Teacher teacher = db.Teachers.FirstOrDefault(a => a.user.login == login);
                            teacherController = new TeacherController(teacher, db);
                            teacherController.Run(teacher); break;
                        }
                    case 4:
                        {
                            Parent parent = db.Parents.FirstOrDefault(a => a.user.login == login);
                            parentController = new ParentController(parent, db);
                            parentController.Run(parent); break;
                        }
                }
            }
        }
        public int Login(string login, string password)
        {
            foreach (var u in db.Admins)
            {
                if (Equals(login, u.user.login) && Equals(password, u.user.password)) { SuccesAndErrorsView.ShowSuccesMessage("Witaj adminie"); return 1; }
            }
            foreach (var u in db.Students)
            {
                if (Equals(login, u.user.login) && Equals(password, u.user.password)) { SuccesAndErrorsView.ShowSuccesMessage("Witaj uczniu"); return 2; }
            }
            foreach (var u in db.Teachers)
            {
                if (Equals(login, u.user.login) && Equals(password, u.user.password)) { SuccesAndErrorsView.ShowSuccesMessage("Witaj nauczycielu"); return 3; }
            }
            foreach (var u in db.Parents)
            {
                if (Equals(login, u.user.login) && Equals(password, u.user.password)) { SuccesAndErrorsView.ShowSuccesMessage("Witaj rodzicu"); return 4; }
            }
            SuccesAndErrorsView.ShowErrorMessage("Cos poszlo nie tak :(");
            return 5;
        }
    }
}
