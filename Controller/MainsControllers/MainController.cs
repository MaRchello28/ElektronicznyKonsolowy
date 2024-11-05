using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.MainViews;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ElektronicznyKonsolowy.Controller.MainsControllers
{
    public class MainController
    {
        private readonly MyDbContext db;
        private readonly MainView mainView;
        private AdminController adminController;
        private StudentController studentController;
        private TeacherController teacherController;
        private ParentController parentController;

        private const int AdminUserType = 1;
        private const int StudentUserType = 2;
        private const int TeacherUserType = 3;
        private const int ParentUserType = 4;

        public MainController(MyDbContext db, MainView mainView)
        {
            this.db = db;
            this.mainView = mainView;
            this.adminController = new AdminController(db);
        }

        public void Run()
        {
            bool run = true;

            while (run)
            {
                mainView.OnProgramStart();
                string login = mainView.GetLogin();
                string password = mainView.GetPassword();
                int userType = Login(login, password);

                switch (userType)
                {
                    case AdminUserType:
                        adminController.Run();
                        break;
                    case StudentUserType:
                        var student = db.Students.FirstOrDefault(a => a.user.login == login);
                        if (student != null)
                        {
                            studentController = new StudentController(student, db);
                            studentController.Run(student);
                        }
                        break;
                    case TeacherUserType:
                        var teacher = db.Teachers.FirstOrDefault(a => a.user.login == login);
                        if (teacher != null)
                        {
                            teacherController = new TeacherController(teacher, db);
                            teacherController.Run(teacher);
                        }
                        break;
                    case ParentUserType:
                        var parent = db.Parents.FirstOrDefault(a => a.user.login == login);
                        if (parent != null)
                        {
                            parentController = new ParentController(parent, db);
                            parentController.Run(parent);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public int Login(string login, string password)
        {
            var admin = db.Admins.FirstOrDefault(a => a.user.login == login && a.user.password == password);
            if (admin != null)
            {
                admin.user.messages = LoadMessages(login);
                SuccesAndErrorsView.ShowSuccesMessage("Witaj adminie");
                return AdminUserType;
            }

            var student = db.Students.FirstOrDefault(s => s.user.login == login && s.user.password == password);
            if (student != null)
            {
                student.user.messages = LoadMessages(login);
                SuccesAndErrorsView.ShowSuccesMessage("Witaj uczniu");
                return StudentUserType;
            }

            var teacher = db.Teachers.FirstOrDefault(t => t.user.login == login && t.user.password == password);
            if (teacher != null)
            {
                teacher.user.messages = LoadMessages(login);
                SuccesAndErrorsView.ShowSuccesMessage("Witaj nauczycielu");
                return TeacherUserType;
            }

            var parent = db.Parents.FirstOrDefault(p => p.user.login == login && p.user.password == password);
            if (parent != null)
            {
                parent.user.messages = LoadMessages(login);
                SuccesAndErrorsView.ShowSuccesMessage("Witaj rodzicu");
                return ParentUserType;
            }

            SuccesAndErrorsView.ShowErrorMessage("Coś poszło nie tak :(");
            return 0;
        }

        public ICollection<Mail> LoadMessages(string login)
        {
            return db.Mails.Where(m => m.from == login || m.to == login).ToList();
        }
    }
}
