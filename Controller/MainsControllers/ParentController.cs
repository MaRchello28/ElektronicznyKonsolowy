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
    public class ParentController
    {
        ParentView parentView = new ParentView(); EditYourDataController edit; MyDbContext db;
        public ParentController(Parent parent, MyDbContext db) { this.db = db; edit = new EditYourDataController(parent, db); }
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
                            break;
                        }
                    case 2:
                        {
                            break;
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
