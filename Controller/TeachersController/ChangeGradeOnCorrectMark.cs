using ElektronicznyKonsolowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller.TeachersController
{
    public class ChangeGradeOnCorrectMark
    {
        List<double> gradeNumber = new List<double>
        {
            1, 1.5, 1.75,
            2, 2.5, 2.75,
            3, 3.5, 3.75,
            4, 4.5, 4.75,
            5, 5.5, 5.75,
            6
        };
        List<string> gradeChar = new List<string>
        {
            "1", "1+", "2-",
            "2", "2+", "3-",
            "3", "3+", "4-",
            "4", "4+", "5-",
            "5", "5+", "6-",
            "6"
        };
        public ChangeGradeOnCorrectMark()
        {

        }
        public string ChangeNumberOnChar(double value)
        {
            for(int i=0; i<gradeNumber.Count; i++)
            {
                if(gradeNumber[i] == value) { return gradeChar[i]; }
            }
            SuccesAndErrorsView.ShowErrorMessage("Nieprawidlowa wartosc");
            return "";
        }
    }
}
