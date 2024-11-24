using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj imię rodzica: [/]");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj poprawne imie[/]"); }
            } while (string.IsNullOrWhiteSpace(name));
            return Console.ReadLine();
        }
        public string EnterSurname()
        {
            string name;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj nazwisko rodzica: [/]");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj poprawne nazwisko[/]"); }
            } while (string.IsNullOrWhiteSpace(name));
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
            string name; bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj nowy email: [/]");
                name = Console.ReadLine();
                if (name == null || name == "") { AnsiConsole.MarkupLine("[red]Nie podano wartości![/]"); }
                else if (!(name.Contains('@') && name.Contains('.'))) { AnsiConsole.MarkupLine("[red]To nie jest adres email![/]"); }
                else { run = false; }
            }
            while (run);
            return name;
        }
        public string EnterPhoneNumber()
        {
            string name; bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj nowy Nr Telefonu: [/]");
                name = Console.ReadLine();
                if (name == null || name == "") { AnsiConsole.MarkupLine("[red]Nie podano wartości![/]"); }
                else if (name.Length != 9) { AnsiConsole.MarkupLine("[red]To nie jest numer telefonu![/]"); }
                else { run = false; }
            }
            while (run);
            return name;
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
