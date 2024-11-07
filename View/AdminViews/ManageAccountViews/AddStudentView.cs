using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageAccountViews
{
    public class AddStudentView
    {
        MyDbContext db;
        public AddStudentView(MyDbContext db) { this.db = db; }
        public void ShowCreateWindow()
        {
            var header = new FigletText("Okno tworzenia ucznia").Centered().Color(Color.Green3);
            AnsiConsole.Render(header);
        }
        public string EnterName()
        {
            string name;
            AnsiConsole.MarkupLine("[blue] Podaj imię ucznia: [/]");
            return Console.ReadLine();
        }
        public string EnterSurname()
        {
            string surname;
            AnsiConsole.MarkupLine("[blue] Podaj nazwisko ucznia: [/]");
            return Console.ReadLine();
        }
        public string CreateDefaultLogin(string name, string surname)
        {
            Random random = new Random();
            string defaultLogin;
            int randomNumber1 = random.Next(0, 9);
            int randomNumber2 = random.Next(0, 9);
            int randomNumber3 = random.Next(0, 9);
            defaultLogin = (name.Substring(0,3) + randomNumber1 + randomNumber2 + randomNumber3 + surname.Substring(0,3) ).ToString();
            return defaultLogin;
        }
        public string EnterDefaultPassword()
        {
            string defaultPassword = "";
            Random random = new Random();
            int value; char theValue;
            for(int i=0; i<10; i++)
            {
                if(i < 3)
                {
                    value = random.Next(48,57);
                }
                else if(i < 6)
                {
                    value = random.Next(65,90);
                }
                else
                {
                    value = random.Next(97,122);
                }
                theValue = Convert.ToChar(value);
                defaultPassword = defaultPassword + theValue;
            }
            return defaultPassword;
        }
        public string EnterParentId()
        {
            AnsiConsole.MarkupLine("[blue] Podaj id rodzica: [/]");
            string value = Console.ReadLine();
            return value;
        }
        public string EnterClassId()
        {
            AnsiConsole.MarkupLine("[blue] Podaj id klasy ucznia, do której chcesz go przypisać: [/]");
            string value = Console.ReadLine();
            return value;
        }
        public void ShowCreatedStudent(string name, string surname, string login, string password, string idKlasy, string idRodzica)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonego ucznia[/]");
            table.AddColumn("IdUcznia");
            table.AddColumn("Imie"); table.AddColumn("Nazwisko"); table.AddColumn("Login"); table.AddColumn("Haslo");
            table.AddColumn("IdKlasy"); table.AddColumn("IdRodzica");

            table.AddRow("", name, surname, login, password, idKlasy, idRodzica);

            AnsiConsole.Render(table);
        }
    }
}
