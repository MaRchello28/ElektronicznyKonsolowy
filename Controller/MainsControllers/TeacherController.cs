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
                Console.Clear();
                choose = teacherView.ShowMainMenu();
                switch (choose)
                {
                    case 0:
                        {
                            Console.Clear();
                            edit.EditUser(teacher); break;
                        }
                    case 1:
                        {
                            Console.Clear();
                            chooseClassController.Run(teacher.teacherId); break;
                        }
                    case 2:
                        {
                            Console.Clear();
                            mailController.ChooseOption(); break;
                        }
                    default:
                        {
                            Console.Clear();
                            run = false; break;
                        }
                }
            }
        }
    }
}
