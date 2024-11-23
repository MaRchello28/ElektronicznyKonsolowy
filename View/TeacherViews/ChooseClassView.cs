using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.TeacherViews
{
    public class ChooseClassView
    {
        MyDbContext db;
        public ChooseClassView(MyDbContext db) 
        { 
            this.db = db;
        }
        public int SelectClass(int teacherId, string[] optionsInArray, List<StudentClass> options)
        {
            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Co chcesz wykonać?")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);
            if(index == optionsInArray.Count()-1)
            {
                index = -1;
                return index;
            }
            var clas = options.FirstOrDefault(o => o.number+o.letter == optionsInArray[0]);
            if(clas != null)
            {
                return clas.studentClassId;
            }
            else
            {
                throw new Exception("Niepoprawna konwertacja idKlasy na odpowiedni jej obiekt");
            }
        }
    }
}
