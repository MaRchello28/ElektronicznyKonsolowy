using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageLessonViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageLessonControllers
{
    public class ShowLessonController
    {
        MyDbContext db; ShowLessonView view;
        public ShowLessonController(MyDbContext db)
        {
            this.db = db;
            view = new ShowLessonView(db);
        }
    }
}
