using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController;
using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageSubjectsController;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.AdminViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageSubjectViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers
{
    public class manageSubjectController
    {
        manageSubjectView manageSubjectView = new manageSubjectView() ;MyDbContext db;
        ShowSubjectsView view; AddSubjectController addSubjectController;
        EditSubjectController editSubjectController; DeleteSubjectController deleteSubjectController;
        public manageSubjectController(MyDbContext db)
        {
            this.db = db; view = new ShowSubjectsView(db); addSubjectController = new AddSubjectController(db);
            editSubjectController = new EditSubjectController(db); deleteSubjectController = new DeleteSubjectController(db);
        }
        public void ShowManageWindow()
        {
            bool run = true; int choose = 5;
            while (run)
            {
                choose = manageSubjectView.ShowManageWindow();
                switch (choose)
                {
                    case 0:
                        {
                            addSubjectController.CreateStudent(); break;
                        }
                    case 1:
                        {
                            deleteSubjectController.Run(); break;
                        }
                    case 2:
                        {
                            editSubjectController.Edit(); break;
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
