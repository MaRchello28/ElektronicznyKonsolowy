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
    public class StudentController
    {
        MyDbContext db;
        StudentView studentView = new StudentView(); EditYourDataController edit;
        MailController mailController;
        public StudentController(Student student, MyDbContext db) { this.db = db; edit = new EditYourDataController(student, db);
            mailController = new MailController(db, student.user.login);
        }
        public void Run(Student student)
        {
            bool run = true; int choose;
            while (run)
            {
                choose = studentView.ShowMainMenu();
                switch(choose)
                {
                    case 0:
                        {
                            edit.EditUser(student); break;
                        }
                    case 1:
                        {
                            break;
                        }
                    case 2:
                        {
                            mailController.ChooseOption(); break;
                        }
                    case 3:
                        {
                            run = false; break;
                        }
                }
            }
        }
    }
}
