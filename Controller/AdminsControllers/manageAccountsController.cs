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
    public class manageAccountsController
    {
        manageAccountsView manageAccountsView = new manageAccountsView(); MyDbContext db;
        ShowStudentsController show; ShowStudentsView view; AddStudentController addStudentController;
        EditStudentController EditStudentController; DeleteStudentController deleteStudentController;
        public manageAccountsController(MyDbContext db) 
        { 
            this.db = db; show = new ShowStudentsController(db); view = new ShowStudentsView(db); addStudentController = new AddStudentController(db);
            EditStudentController = new EditStudentController(db); deleteStudentController = new DeleteStudentController(db);
        }
        public void ShowManageWindow()
        {
            bool run = true; int choose = 5;
            while(run)
            {
                choose = manageAccountsView.ShowManageWindow();
                switch (choose)
                {
                    case 0:
                        {
                            addStudentController.CreateStudent(); break;
                        }
                    case 1:
                        {
                            deleteStudentController.Run(); break;
                        }
                    case 2:
                        {
                            EditStudentController.Edit(); break;
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
