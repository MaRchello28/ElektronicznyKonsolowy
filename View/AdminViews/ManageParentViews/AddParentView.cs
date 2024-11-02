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
    public class AddParentView
    {
        MyDbContext db;
        public AddParentView(MyDbContext db) { this.db = db; }
        public void ShowCreateWindow()
        {
            var header = new FigletText("Okno tworzenia uzytkownika").Centered().Color(Color.Green3);
            AnsiConsole.Render(header);
        }
        public string EnterName()
        {
            string name;
            AnsiConsole.MarkupLine("[blue] Podaj imię rodzica: [/]");
            return Console.ReadLine();
        }
        public string EnterSurname()
        {
            string surname;
            AnsiConsole.MarkupLine("[blue] Podaj nazwisko rodzica: [/]");
            return Console.ReadLine();
        }
        public string CreateDefaultLogin(string name, string surname)
        {
            Random random = new Random();
            string defaultLogin;
            int randomNumber1 = random.Next(0, 9);
            int randomNumber2 = random.Next(0, 9);
            int randomNumber3 = random.Next(0, 9);
            defaultLogin = (name.Substring(0, 3) + randomNumber1 + randomNumber2 + randomNumber3 + surname.Substring(0, 3)).ToString();
            return defaultLogin;
        }
        public string EnterDefaultPassword()
        {
            string defaultPassword = "";
            Random random = new Random();
            int value; char theValue;
            for (int i = 0; i < 10; i++)
            {
                if (i < 3)
                {
                    value = random.Next(48, 57);
                }
                else if (i < 6)
                {
                    value = random.Next(65, 90);
                }
                else
                {
                    value = random.Next(97, 122);
                }
                theValue = Convert.ToChar(value);
                defaultPassword = defaultPassword + theValue;
            }
            return defaultPassword;
        }
        public string EnterEmail()
        {
            AnsiConsole.MarkupLine("[blue] Podaj email rodzica: [/]");
            string email = Console.ReadLine();
            return email;
        }
        public string EnterPhoneNumber()
        {
            AnsiConsole.MarkupLine("[blue] Podaj numer telefonu rodzica: [/]");
            string phonenumber = Console.ReadLine();
            return phonenumber;
        }
        public void ShowCreatedParent(string name, string surname, string login, string password, string email, string phonenumber)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonego rodzica[/]");
            table.AddColumn("IdRodzica");
            table.AddColumn("Imie"); table.AddColumn("Nazwisko"); table.AddColumn("Login"); table.AddColumn("Haslo");
            table.AddColumn("Email"); table.AddColumn("PhoneNumber");

            table.AddRow("", name, surname, login, password, email, phonenumber);

            AnsiConsole.Render(table);
        }
    }
}
