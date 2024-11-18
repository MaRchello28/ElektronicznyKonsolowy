using ElektronicznyKonsolowy.Controller.AdditionalOptionsController;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.MainViews;
using ElektronicznyKonsolowy.View.ParentViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.MainsControllers
{
    public class ParentController
    {
        ParentView parentView = new ParentView(); 
        EditYourDataController edit; 
        MyDbContext db; 
        MailController mailController;
        ShowClassScheduleParentView show;
        ShowGradesParentView view;
        ShowAttendanceParentView showAttendance;
        public ParentController(Parent parent, MyDbContext db) { this.db = db; edit = new EditYourDataController(parent, db); 
            mailController = new MailController(db, parent.user.login); show = new ShowClassScheduleParentView(db);
            view = new ShowGradesParentView(db); showAttendance = new ShowAttendanceParentView(db);
        }
        public void Run(Parent parent)
        {
            bool run = true; int choose;
            while (run)
            {
                choose = parentView.ShowMainMenu();
                switch(choose)
                {
                    case 0:
                        {
                            edit.EditUser(parent); break;
                        }
                    case 1:
                        {
                            view.Show(parent);
                            break;
                        }
                    case 2:
                        {
                            mailController.ChooseOption(); break;
                        }
                    case 3:
                        {
                            show.show(parent);break;
                        }
                    case 4:
                        {
                            showAttendance.Show(parent); break;
                        }
                    case 5:
                        {
                            run = false; break;
                        }
                }
            }
        }
    }
}
