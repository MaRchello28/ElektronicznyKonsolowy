using ElektronicznyKonsolowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.MainsControllers
{
    public class StudentController
    {
        StudentView studentView = new StudentView();
        public StudentController() { }
        public void Run()
        {
            bool run = true; int choose;
            while (run)
            {
                choose = studentView.ShowMainMenu();
            }
        }
    }
}
