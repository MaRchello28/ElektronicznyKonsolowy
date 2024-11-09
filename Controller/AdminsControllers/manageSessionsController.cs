using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews;
using ElektronicznyKonsolowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageSessionControllers;
using ElektronicznyKonsolowy.View.AdminViews.ManageSessionViews;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers
{
    public class manageSessionsController
    {
        manageSessionsView manageSessionsView = new manageSessionsView(); MyDbContext db;
        ShowSessionController show; ShowSessionView view; AddSessionController addSessionController;
        EditSessionController editSessionController; DeleteSessionController deleteSessionController;
        public manageSessionsController(MyDbContext db)
        {
            this.db = db; show = new ShowSessionController(db); view = new ShowSessionView(db); addSessionController = new AddSessionController(db);
            editSessionController = new EditSessionController(db); deleteSessionController = new DeleteSessionController(db);
        }
        public void ShowManageWindow()
        {
            bool run = true; int choose = 5;
            while (run)
            {
                choose = manageSessionsView.ShowManageWindow();
                switch (choose)
                {
                    case 0:
                        {
                            addSessionController.CreateSession(); break;
                        }
                    case 1:
                        {
                            deleteSessionController.Run(); break;
                        }
                    case 2:
                        {
                            editSessionController.Edit(); break;
                        }
                    case 3:
                        {
                            view.Show(); break;
                        }
                    case 4:
                        {
                            run = false; break;
                        }
                    case 5:
                        {
                            SuccesAndErrorsView.ShowErrorMessage("Cos poszlo nie tak :("); break;
                        }
                }
            }
        }
    }
}
