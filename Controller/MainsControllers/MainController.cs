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
        AdminController adminController = new AdminController(); StudentController studentController = new StudentController();
        TeacherController teacherController = new TeacherController(); ParentController parentController = new ParentController();
        public MainController(MyDbContext db, MainView mainView) { this.db = db; this.mainView = mainView; }
        public void Run()
        {
            bool run = true; int userType = 5;
            while (run)
            {
                userType = Login();
                switch (userType)
                {
                    case 1:
                        {
                            adminController.Run(); break;
                        }
                    case 2:
                        {
                            studentController.Run(); break;
                        }
                    case 3:
                        {
                            teacherController.Run(); break;
                        }
                    case 4:
                        {
                            parentController.Run(); break;
                        }
                }
            }
        }
        public int Login()
        {
            string login = mainView.GetLogin();
            string password = mainView.GetPassword();
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
