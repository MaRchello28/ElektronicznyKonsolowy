using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageSessionControllers;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageSessionViews;
using ElektronicznyKonsolowy.View.AdminViews;
using ElektronicznyKonsolowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassScheduleController;
using ElektronicznyKonsolowy.View.AdminViews.ManageCalendarViews;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers
{
    public class manageClassScheduleController
    {
        manageClassScheduleView manageClassschedule = new manageClassScheduleView(); MyDbContext db;
        ShowClassScheduleController show; ShowClassScheduleView view; AddClassScheduleController add;
        EditClassScheduleController edit; DeleteClassScheduleCotroller delete;
        public manageClassScheduleController(MyDbContext db)
        {
            this.db = db; show = new ShowClassScheduleController(db); view = new ShowClassScheduleView(db); add = new AddClassScheduleController(db);
            edit = new EditClassScheduleController(db); delete = new DeleteClassScheduleCotroller(db);
        }
        public void ShowManageWindow()
        {
            bool run = true; int choose = 5;
            while (run)
            {
                choose = manageClassschedule.ShowManageWindow();
                switch (choose)
                {
                    case 0:
                        {
                            add.CreateClassSchedule(); break;
                        }
                    case 1:
                        {
                            delete.Run(); break;
                        }
                    case 2:
                        {
                            edit.Edit(); break;
                        }
                    case 3:
                        {
                            view.Show(0); break;
                        }
                    case 4:
                        {
                            view.Show(1);break;
                        }
                    case 5:
                        {
                            run = false; break;
                        }
                    case 6:
                        {
                            SuccesAndErrorsView.ShowErrorMessage("Cos poszlo nie tak :("); break;
                        }
                }
            }
        }
    }
}
