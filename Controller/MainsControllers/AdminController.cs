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
        AdminView adminView = new AdminView();
        public AdminController()
        {

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
                            break;
                        }
                    case 1:
                        {
                            break;
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
