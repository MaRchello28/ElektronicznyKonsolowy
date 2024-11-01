using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace ElektronicznyKonsolowy.View.MainViews
{
    public class MainView
    {
        public MainView()
        {

        }
        public void OnProgramStart()
        {
            var header = new FigletText("E-Dziennik").Centered().Color(Color.Green3);
            AnsiConsole.Render(header);
            AnsiConsole.MarkupLine("[green]Podaj login i hasło, żeby się zalogować[/]");
        }
        public string GetLogin()
        {
            var login = AnsiConsole.Prompt(
            new TextPrompt<string>("Login:"));
            return login;
        }
        public string GetPassword()
        {
            var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Haslo:")
            .Secret('*'));
            return password;
        }
    }
}
