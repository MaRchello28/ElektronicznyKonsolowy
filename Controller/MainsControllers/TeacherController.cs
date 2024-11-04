using ElektronicznyKonsolowy.Controller.AdditionalOptionsController;
using ElektronicznyKonsolowy.Models;
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
        MyDbContext db;
        TeacherView teacherView = new TeacherView(); EditYourDataController edit;
        public TeacherController(Teacher teacher, MyDbContext db) { this.db = db; edit = new EditYourDataController(teacher, db); }
        public void Run(Teacher teacher) 
        {
            bool run = true; int choose;
            while(run)
            {
                choose = teacherView.ShowMainMenu();
                switch(choose)
                {
                    case 0:
                        {
                            edit.EditUser(teacher); break;
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
                            run = false; break;
                        }
                }
            }
        }
    }
}
