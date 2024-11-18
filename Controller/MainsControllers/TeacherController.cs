using ElektronicznyKonsolowy.Controller.AdditionalOptionsController;
using ElektronicznyKonsolowy.Controller.TeachersController;
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
        MailController mailController; ChooseClassController chooseClassController;
        public TeacherController(Teacher teacher, MyDbContext db) { this.db = db; edit = new EditYourDataController(teacher, db);
            mailController = new MailController(db, teacher.user.login);
            chooseClassController = new ChooseClassController(db);
        }
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
                            chooseClassController.Run(teacher.teacherId); break;
                        }
                    case 2:
                        {
                            mailController.ChooseOption(); break;
                        }
                    default:
                        {
                            run = false; break;
                        }
                }
            }
        }
    }
}
