using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.MainViews;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

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
        }

        public void Run()
        {
            bool run = true;
            var ses = db.Sessions.ToList();
            var classSchedules = new List<ClassSchedule>();
            classSchedules = db.ClassSchedules.ToList();
            foreach (var session in ses)
            {
                var classSchedule = classSchedules
                    .FirstOrDefault(cs => cs.classScheduleId == session.ClassScheduleId);

                if (classSchedule != null)
                {
                    classSchedule.sessions.Add(session);
                }
            }
            while (run)
            {
                mainView.OnProgramStart();
                string login = mainView.GetLogin();
                string password = mainView.GetPassword();
                int userType = Login(login, password);

                switch (userType)
                {
                    case AdminUserType:
                        var admin = db.Admins.FirstOrDefault(a => a.user.login == login);
                        if (admin != null)
                        {
                            adminController = new AdminController(admin, db);
                            adminController.Run();
                        }
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
            var admin = db.Admins.FirstOrDefault(a => Equals(a.user.login,login) && Equals(a.user.password,password));
            if (admin != null)
            {
                admin.user.messages = LoadMessages(login);
                SuccesAndErrorsView.ShowSuccesMessage("Witaj adminie");
                return AdminUserType;
            }

            var student = db.Students.FirstOrDefault(a => Equals(a.user.login, login) && Equals(a.user.password, password));
            if (student != null)
            {
                student.user.messages = LoadMessages(login);
                SuccesAndErrorsView.ShowSuccesMessage("Witaj uczniu");
                return StudentUserType;
            }

            var teacher = db.Teachers.FirstOrDefault(a => Equals(a.user.login, login) && Equals(a.user.password, password));
            if (teacher != null)
            {
                teacher.user.messages = LoadMessages(login);
                SuccesAndErrorsView.ShowSuccesMessage("Witaj nauczycielu");
                return TeacherUserType;
            }

            var parent = db.Parents.FirstOrDefault(a => Equals(a.user.login, login) && Equals(a.user.password, password));
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
