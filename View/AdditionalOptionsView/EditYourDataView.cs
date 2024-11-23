using ElektronicznyKonsolowy.Models;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View.AdditionalOptionsView
{
    public class EditYourDataView
    {
        public EditYourDataView() { }
        public void ShowUser(Student student)
        {
            var table = new Table();
            table.Title = new TableTitle("[green]Dane użytkownika[/]");
            table.AddColumn("Id studenta");
            table.AddColumn("Imię");
            table.AddColumn("Nazwisko");
            table.AddColumn("Login");
            table.AddColumn("Hasło");
            table.AddRow(student.studentId.ToString(), student.user.name.ToString(), student.user.surname.ToString(), student.user.login.ToString(), student.user.password.ToString());

            AnsiConsole.Render(table);
        }
        public void ShowUser(Teacher teacher)
        {
            var table = new Table();
            table.Title = new TableTitle("[green]Dane użytkownika[/]");
            table.AddColumn("Id nauczyciela");
            table.AddColumn("Imię");
            table.AddColumn("Nazwisko");
            table.AddColumn("Login");
            table.AddColumn("Hasło");
            table.AddColumn("Email");
            table.AddRow(teacher.teacherId.ToString(), teacher.user.name.ToString(), teacher.user.surname.ToString(), teacher.user.login.ToString(), teacher.user.password.ToString(),
                teacher.email);

            AnsiConsole.Render(table);
        }
        public void ShowUser(Parent parent)
        {
            var table = new Table();
            table.Title = new TableTitle("[green]Dane użytkownika[/]");
            table.AddColumn("Id rodzica");
            table.AddColumn("Imię");
            table.AddColumn("Nazwisko");
            table.AddColumn("Login");
            table.AddColumn("Hasło");
            table.AddColumn("Email");
            table.AddColumn("Nr telefonu");
            table.AddRow(parent.parentId.ToString(), parent.user.name.ToString(), parent.user.surname.ToString(), parent.user.login.ToString(), parent.user.password.ToString(),
                parent.email, parent.phoneNumber);

            AnsiConsole.Render(table);
        }

        public List<bool> ChooseWhatToEdit(UserType userType)
        {
            var allOptions = new List<string> {"Login", "Haslo", "Email", "Numer Telefonu" };

            List<string> availableOptions = userType switch
            {
                UserType.Admin => new List<string> { "Login", "Haslo" },
                UserType.Student => new List<string> { "Login", "Haslo" },
                UserType.Teacher => new List<string> { "Login", "Haslo", "Email" },
                UserType.Parent => new List<string> { "Login", "Haslo", "Email", "Numer Telefonu" },
                _ => throw new ArgumentOutOfRangeException(nameof(userType), $"Nieznany typ użytkownika: {userType}")
            };

            List<bool> result = new List<bool> { false, false, false, false };

            var options = AnsiConsole.Prompt(
                new MultiSelectionPrompt<string>()
                    .Title("[green]Wybierz opcje do edycji: [/]")
                    .NotRequired()
                    .PageSize(10)
                    .InstructionsText(
                        "[grey](Naciśnij [red]<space>[/], żeby zaznaczyć zmienną, a " +
                        "[green]<enter>[/], żeby zaakceptować)[/]")
                    .AddChoices(availableOptions)
            );

            for (int i = 0; i < allOptions.Count; i++)
            {
                result[i] = options.Contains(allOptions[i]) && availableOptions.Contains(allOptions[i]);
            }

            return result;
        }
        public string EditOption(int i)
        {
            if (i == 0)
            {
                string name;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowy login: [/]");
                    name = Console.ReadLine();
                    if (name.Length <= 3) { AnsiConsole.MarkupLine("[red]Login nie może być krótszy niż 4 znaki[/]"); }
                }
                while (name.Length <= 3);
                return name;
            }
            else if (i == 1)
            {
                string name;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowe hasło: [/]");
                    name = Console.ReadLine();
                    if (name.Length <= 6) { AnsiConsole.MarkupLine("[red]Hasło nie może być krótsze niż 6 znaków[/]"); }
                }
                while (name.Length <= 6);
                return name;
            }
            else if (i == 2)
            {
                string name; bool run = true;
                do
                {
                    AnsiConsole.MarkupLine("[blue] Podaj nowy email: [/]");
                    name = Console.ReadLine();
                    if(name == null || name == "") { AnsiConsole.MarkupLine("[red]Nie podano wartości![/]"); }
                    else if (!(name.Contains('@') && name.Contains('.'))) { AnsiConsole.MarkupLine("[red]To nie jest adres email![/]"); }
                    else { run = false; }
                }
                while (run);
                return name;
            }
            else if (i == 3)
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
            return "";
        }
    }
}
