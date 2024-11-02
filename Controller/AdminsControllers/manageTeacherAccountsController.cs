using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageStudentsController;
using ElektronicznyKonsolowy.Controller.AdminsControllers.ManageTeachersControlle;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using ElektronicznyKonsolowy.View.AdminViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageTeachersView;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers
{
    public class manageTeacherAccountsController
    {
        MyDbContext db; manageTeacherAccountsView mTAV; AddTeacherController add; ShowTeachersView show;
        EditTeacherController edit; DeleteTeacherController delete;
        public manageTeacherAccountsController(MyDbContext db) 
        { 
            this.db = db; mTAV = new manageTeacherAccountsView(); 
            add = new AddTeacherController(db);
            edit = new EditTeacherController(db);
            show = new ShowTeachersView(db);
            delete = new DeleteTeacherController(db);
        }
        public void Run()
        {
            bool run = true; int choose = 5;
            while (run)
            {
                choose = mTAV.ShowOptions();
                switch (choose)
                {
                    case 0:
                        {
                            add.CreateTeacher(); break;
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
                            show.Show(); break;
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
