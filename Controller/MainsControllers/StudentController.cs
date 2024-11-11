using ElektronicznyKonsolowy.Controller.AdditionalOptionsController;
using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassScheduleController;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews;
using ElektronicznyKonsolowy.View.MainViews;
using ElektronicznyKonsolowy.View.StudentViews;
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
        MailController mailController; ShowClassScheduleStudentView show;
        ShowGradesStudentView view;
        public StudentController(Student student, MyDbContext db) { this.db = db; edit = new EditYourDataController(student, db);
            mailController = new MailController(db, student.user.login); show = new ShowClassScheduleStudentView(db);
            view = new ShowGradesStudentView(db);
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
                            view.show(student);
                            break;
                        }
                    case 2:
                        {
                            mailController.ChooseOption(); break;
                        }
                    case 3:
                        {
                            show.Show(student);
                            break;
                        }
                    case 4:
                        {
                            run = false; break;
                        }
                }
            }
        }
    }
}
