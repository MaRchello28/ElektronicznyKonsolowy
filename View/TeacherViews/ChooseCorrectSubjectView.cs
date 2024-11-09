using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.TeacherViews
{
    public class ChooseCorrectSubjectView
    {
        MyDbContext db;
        public ChooseCorrectSubjectView(MyDbContext db) 
        { 
            this.db = db;
        }
        public int Run(List<string> subjectNames)
        {
            string[] optionsInArray = new string[subjectNames.Count];
            for(int i=0; i< subjectNames.Count; i++)
            {
                optionsInArray[i] = subjectNames[i];
            }

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Wybierz przedmiot: ")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);

            return index;
        }
        public int SelectOption()
        {
            string[] optionsInArray = new string[3];
            optionsInArray[0] = "Wstaw oceny";
            optionsInArray[1] = "Lekcje";
            optionsInArray[2] = "Powrót";

            var selectedOption = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Co chcesz wykonać?")
                    .PageSize(10)
                    .AddChoices(optionsInArray));
            int index = Array.IndexOf(optionsInArray, selectedOption);

            return index;
        }
        public void ManageGrades(int selectedClass)
        {
            var grid = new Grid();

            var nameSurname = new Table();
            nameSurname.AddColumn("Imie i nazwisko ucznia");

            var grades = new Table();
            grades.AddColumn("1");
            grades.AddColumn("Is");
            grades.AddColumn("IIs");

            var students = db.Students.ToList().OrderBy(s => s.user.surname);
            foreach (var student in students)
            {
                if (selectedClass == student.studentClassId)
                {
                    nameSurname.AddRow(student.user.name + " " + student.surname);
                }
            }

            // Użycie grid do ustawienia tabel obok siebie
            grid.AddColumn();
            grid.AddColumn();

            // Umieszczamy tabele w grid
            grid.AddRow(nameSurname, grades);

            AnsiConsole.Render(grid);
            //Powinno wyświetlić wszystkie oceny w tabeli z kursorem na początku
            //Istnieje przycisk do stworzenia nowej tabeli z oceną
            //Po kliknięciu odpowiedniej oceny wyświetla się jej opis
        }
        public void ManageLessons()
        {

        }
    }
}
