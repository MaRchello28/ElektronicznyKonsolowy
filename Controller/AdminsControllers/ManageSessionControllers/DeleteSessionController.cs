using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews;
using ElektronicznyKonsolowy.View.AdminViews.ManageSessionViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers.ManageSessionControllers
{
    internal class DeleteSessionController
    {
        MyDbContext db; DeleteSessionView view;
        public DeleteSessionController(MyDbContext db)
        {
            this.db = db; this.view = new DeleteSessionView(db);
        }
        
        public void Run()
        {
            this.view = new DeleteSessionView(db);
            int idToDelete = view.PutIndex();
            foreach (var session in db.Sessions)
            {
                if (session.sessionId == idToDelete)
                {
                    if (view.Agree() == 0) { db.Sessions.Remove(session); }
                    break;
                }
            }
            db.SaveChanges();
        }
    }
}

