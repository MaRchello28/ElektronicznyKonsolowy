using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController;
using ElektronicznyKonsolowy.View.AdminViews;
using ElektronicznyKonsolowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageClassController;
using ElektronicznyKonsolowy.View.AdminViews.ManageClassView;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers
{
    public class manageClassController
    {
        MyDbContext db; manageClassView view; AddClassController add;
        EditClassController edit; ShowClassView showClassView; DeleteClassController delete;
        public manageClassController(MyDbContext db) 
        { 
            this.db = db; view = new manageClassView();
            add = new AddClassController(db);
            edit = new EditClassController(db);
            showClassView = new ShowClassView(db);
            delete = new DeleteClassController(db);
        }
        public void ShowManageWindow()
        {
            bool run = true; int choose = 5;
            while (run)
            {
                choose =view.ShowManageWindow();
                switch (choose)
                {
                    case 0:
                        {
                            add.CreateClass(); break;
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
                            showClassView.Show(); break;
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
