using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.Controller
{
    public class AdminController
    {
        public AdminController() 
        { 
            
        }
        public void Run() 
        {
            bool run = true;
            while(run)
            {
                var fruit = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Co chcesz wykonać?")
                .PageSize(10)
                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                .AddChoices(new[] {
                    "Zarzadzanie kontami uczniow",
                    "Zarzadzanie kontami nauczycieli",
                    "Zarzadzanie kontami rodzicow",
                    "Zarzadzanie klasami",
                    "Przypisz uczniow do odpowieniej klasy",
                    "Zarzadzaj przedmiotami",
                    "Przypisz przedmioty do klasy"
                }));
            }
        }
        /*public Student CreateStudent()
        {

        }
        public Student UpdateStudent()
        {

        }
        public void DeleteStudent()
        {

        }
        public Teacher CreateTeacher()
        {

        }
        public Teacher UpdateTeacher()
        {

        }
        public void DeleteTeacher()
        {

        }
        public Parent CreateParent()
        {

        }
        public Parent UpdateParent()
        {

        }
        public void DeleteParent()
        {

        }*/
    }
}
