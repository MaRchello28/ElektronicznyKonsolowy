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
        public string EnterName()
        {
            string name;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj imie ucznia: [/]");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj poprawne imie[/]"); }
            } while (string.IsNullOrWhiteSpace(name));
            return name;
        }
        public string EnterSurname()
        {
            string name;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj nazwisko ucznia: [/]");
                name = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(name)) { AnsiConsole.MarkupLine("[red]Podaj poprawne nazwisko[/]"); }
            } while (string.IsNullOrWhiteSpace(name));
            return name;
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
            string value;
            bool run=true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj Id rodzica: [/]");
                value = Console.ReadLine();
                int id = int.Parse(value);
                if(id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                else
                {
                    using(var context = new MyDbContext())
                    {
                        if (!context.Parents.Any(c => c.parentId == id))
                        {
                            AnsiConsole.MarkupLine("[red]Podane id nie jest w bazie[/]");
                        }
                        else
                        {
                            run = false;
                        }
                    }
                }

            }
            while(run);
            return value;
        }
        public string EnterClassId()
        {
            string value;
            bool run = true;
            do
            {
                AnsiConsole.MarkupLine("[blue]Podaj Id Klasy: [/]");
                value = Console.ReadLine();
                int id = int.Parse(value);
                if (id <= 0) { AnsiConsole.MarkupLine("[red]Podaj poprawne id[/]"); }
                else
                {
                    using (var context = new MyDbContext())
                    {
                        if (!context.StudentClasses.Any(c => c.studentClassId == id))
                        {
                            AnsiConsole.MarkupLine("[red]Podane id nie jest w bazie[/]");
                        }
                        else
                        {
                            run = false;
                        }
                    }
                }

            }
            while (run);
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
