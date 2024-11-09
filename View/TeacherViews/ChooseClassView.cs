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
        public int selectClass()
        {
            var options = db.StudentClasses.ToList();
            string[] optionsInArray = new string[options.Count];
            int i = 0;
            foreach(var option in options)
            {
                optionsInArray[i] = option.number + option.letter;
                i++;
            }    

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Co chcesz wykonać?")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);
            int id = options[index].studentClassId;

            return id;
        }
    }
}
