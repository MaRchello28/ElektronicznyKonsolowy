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
        public string GetLogin()
        {
            var login = AnsiConsole.Prompt(
            new TextPrompt<string>("Podaj login:"));
            return login;
        }
        public string GetPassword()
        {
            var password = AnsiConsole.Prompt(
            new TextPrompt<string>("Podaj haslo:")
            .Secret('*'));
            return password;
        }
    }
}
