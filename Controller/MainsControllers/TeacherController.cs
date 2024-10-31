using ElektronicznyKonsolowy.View.MainViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ElektronicznyKonsolowy.Controller.MainsControllers
{
    public class TeacherController
    {
        TeacherView teacherView = new TeacherView();
        public TeacherController() { }
        public void Run() 
        {
            bool run = true; int choose;
            while(run)
            {
                choose = teacherView.ShowMainMenu();
            }
        }
    }
}
