using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller
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
            while(run)
            {
                choose = adminView.ShowMainMenu();
            }
        }
    }
}
