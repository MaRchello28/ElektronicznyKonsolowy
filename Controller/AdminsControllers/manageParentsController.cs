using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.AdminViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.MainViews;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers
{
    public class manageParentsController
    {
        manageParentsView manageParentsView = new manageParentsView(); MyDbContext db;
        ShowParentsController show; ShowParentsView view; AddParentController addParentController;
        EditParentController EditParentController; DeleteParentController deleteParentController;
        public manageParentsController(MyDbContext db)
        {
            this.db = db; show = new ShowParentsController(db); view = new ShowParentsView(db); addParentController = new AddParentController(db);
            EditParentController = new EditParentController(db); addParentController = new AddParentController(db);
        }
        public void ShowManageWindow()
        {
            bool run = true; int choose = 5;
            while (run)
            {
                choose = manageParentsView.ShowManageWindow();
                switch (choose)
                {
                    case 0:
                        {
                            addParentController.CreateParent(); break;
                        }
                    case 1:
                        {
                            deleteParentController.Run(); break;
                        }
                    case 2:
                        {
                            EditParentController.Edit(); break;
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
