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
        AdminView adminView = new AdminView(); MyDbContext db;
        manageAccountsController manageAccountsController; manageParentsController manageParentsController;
        manageTeacherAccountsController manageTeacherAccountsController; manageClassController manageClassController;
        AddStudentToClassController addStudentToClassController; manageSubjectController manageSubjectController;
        public AdminController(MyDbContext db)
        {
            this.db = db; manageAccountsController = new manageAccountsController(db);
            manageParentsController = new manageParentsController(db); manageTeacherAccountsController = new manageTeacherAccountsController(db);
            manageClassController = new manageClassController(db); addStudentToClassController = new AddStudentToClassController(db);
            manageSubjectController = new manageSubjectController(db);
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
                            break;
                        }
                    case 7:
                        {
                            run = false; break;
                        }
                }
            }
        }
    }
}
