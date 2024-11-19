using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using Spectre.Console;

namespace ElektronicznyKonsolowy.Controller
{
    public static class StaticFunctions
    {
        public static bool LoginExists(string log, MyDbContext db)
        {
            List<Admin> admins = db.Admins.ToList();
            List<Student> students = db.Students.ToList();
            List<Parent> parents = db.Parents.ToList();
            List<Teacher> teachers = db.Teachers.ToList();
            foreach (Admin admin in admins)
            {
                if (Equals(admin.user.login, log)) { ViewsForStaticFunctions.LoginExists(); return true; }
            }
            foreach (Student student in students)
            {
                if (Equals(student.user.login, log)) { ViewsForStaticFunctions.LoginExists(); return true; }
            }
            foreach (Teacher teacher in teachers)
            {
                if (Equals(teacher.user.login, log)) { ViewsForStaticFunctions.LoginExists(); return true; }
            }
            foreach (Parent parent in parents)
            {
                if (Equals(parent.user.login, log)) { ViewsForStaticFunctions.LoginExists(); return true; }
            }
            return false;
        }
        public static bool ValueIsNull(string value)
        {
            if (value == null) { ViewsForStaticFunctions.ValueIsNull(); return true; }
            return false;
        }
    }
}
