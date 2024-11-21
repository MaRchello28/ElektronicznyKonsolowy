using ElektronicznyKonsolowy.Controller.AdminsControllers;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.MainViews;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.MainsControllers
{
    public class AdminController
    {
        AdminView adminView = new AdminView(); MyDbContext db; Admin admin;
        manageAccountsController manageAccountsController; manageParentsController manageParentsController;
        manageTeacherAccountsController manageTeacherAccountsController; manageClassController manageClassController;
        AddStudentToClassController addStudentToClassController; manageSubjectController manageSubjectController;
        manageSessionsController manageSessionsController; manageClassScheduleController manageClassScheduleController;
        public AdminController(MyDbContext db) { }
        MailController mailController;
        public AdminController(Admin admin, MyDbContext db)
        {
            this.db = db; manageAccountsController = new manageAccountsController(db); this.admin = admin;
            manageParentsController = new manageParentsController(db); manageTeacherAccountsController = new manageTeacherAccountsController(db);
            manageClassController = new manageClassController(db); addStudentToClassController = new AddStudentToClassController(db);
            manageSubjectController = new manageSubjectController(db);
            mailController = new MailController(db, admin.user.login);
            manageSubjectController = new manageSubjectController(db);manageSessionsController = new manageSessionsController(db);
            manageClassScheduleController = new manageClassScheduleController(db);
        }
        public void Run()
        {
            bool run = true; int choose;
            while (run)
            {
                choose = adminView.ShowMainMenu();
                switch (choose)
                {
                    case 0:
                        {
                            manageAccountsController.ShowManageWindow(); break;
                        }
                    case 1:
                        {
                            manageTeacherAccountsController.Run(); break;
                        }
                    case 2:
                        {
                            manageParentsController.ShowManageWindow();
                            break;
                        }
                    case 3:
                        {
                            manageClassController.ShowManageWindow(); break;
                        }
                    case 4:
                        {
                            addStudentToClassController.Run(); break;
                        }
                    case 5:
                        {
                            manageSubjectController.ShowManageWindow();
                            break;
                        }
                    case 6:
                        {
                            manageSessionsController.ShowManageWindow();
                            break;
                        }
                    case 7:
                        {
                            mailController.ChooseOption(); break;
                        }
                    case 8:
                        {
                            manageClassScheduleController.ShowManageWindow(); break;
                        }
                    case 9:
                        {
                            run = false; break;

                        }
                }
            }
        }
    }
}
