using ElektronicznyKonsolowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.TeachersController
{
    public class ExistingGradeController
    {
        MyDbContext db;
        public ExistingGradeController(MyDbContext db)
        {
            this.db = db;
        }
        public bool CheckGrade(double value)
        {
            List<double> grades = new List<double>
            {
                1, 1.5, 1.75,
                2, 2.5, 2.75,
                3, 3.5, 3.75,
                4, 4.5, 4.75,
                5, 5.5, 5.75,
                6
            };
            if(grades.Contains(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
