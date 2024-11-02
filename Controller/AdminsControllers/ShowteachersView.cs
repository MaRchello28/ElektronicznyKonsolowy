using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View.AdminViews.ManageTeachersView;

namespace ElektronicznyKonsolowy.Controller.AdminsControllers
{
    internal class ShowteachersView : ShowTeachersView
    {
        public ShowteachersView(MyDbContext db) : base(db)
        {
        }
    }
}