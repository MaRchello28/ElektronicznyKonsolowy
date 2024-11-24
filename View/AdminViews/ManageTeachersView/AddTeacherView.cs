using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdminViews.ManageTeachersView
{
    public class AddTeacherView
    {
        public AddTeacherView() { }
        public string EnterName()
        {
            string name;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj imie nauczyciela: [/]");
                name = (Console.ReadLine());
                if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj poprawne imię[/]"); }
            }
            while (run);
            return name;
        }
        public string EnterSurname()
        {
            string name;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue] Podaj nazwisko nauczyciela [/]");
                name = (Console.ReadLine());
                if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj poprawne nazwisko[/]"); }
            }
            while (run);
            return name;
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
                AnsiConsole.MarkupLine("[blue] Podaj email: [/]");
                name = Console.ReadLine();
                if (name == null || name == "") { AnsiConsole.MarkupLine("[red]Nie podano wartości![/]"); }
                else if (!(name.Contains('@') && name.Contains('.'))) { AnsiConsole.MarkupLine("[red]To nie jest adres email![/]"); }
                else { run = false; }
            }
            while (run);
            return name;
        }
        public void ShowCreatedStudent(string name, string surname, string login, string password, string email)
        {
            var table = new Table();
            table.Caption("[red]Dane utworzonego nauczyciela[/]");
            table.AddColumn("IdUcznia");
            table.AddColumn("Imie"); table.AddColumn("Nazwisko"); table.AddColumn("Login"); table.AddColumn("Haslo");
            table.AddColumn("email");

            table.AddRow("", name, surname, login, password, email);

            AnsiConsole.Render(table);
        }
    }
}
