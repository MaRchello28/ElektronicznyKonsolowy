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
            6, 0
        };
        List<string> gradeChar = new List<string>
        {
            "1", "1+", "2-",
            "2", "2+", "3-",
            "3", "3+", "4-",
            "4", "4+", "5-",
            "5", "5+", "6-",
            "6", ""
        };
        List<string> correctColor = new List<string>
        {
            "red3", "red", "red3_1",
            "orange4_1", "darkorange3", "orange3",
            "yellow", "darkolivegreen1_1", "greenyellow",
            "palegreen1_1", "greenyellow", "chartreuse1",
            "skyblue2", "slateblue1", "purple_1",
            "darkmagenta", "black"
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
        public string CorrectColorForGrade(double value)
        {
            for (int i = 0; i < gradeNumber.Count; i++)
            {
                if (gradeNumber[i] == value) { return correctColor[i]; }
            }
            SuccesAndErrorsView.ShowErrorMessage("Nieprawidlowa wartosc");
            return "";
        }
    }
}
