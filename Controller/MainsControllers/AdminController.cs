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
        manageAccountsController manageAccountsController;
        manageTeacherAccountsController mTAC;
        public AdminController(MyDbContext db)
        {
            this.db = db; manageAccountsController = new manageAccountsController(db);
            mTAC = new manageTeacherAccountsController(db);
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
                            mTAC.Run(); break;
                        }
                    case 2:
                        {
                            break;
                        }
                    case 3:
                        {
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    case 5:
                        {
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
