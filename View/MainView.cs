using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Spectre.Console;
namespace ElektronicznyKonsolowy.View
{
    public class MainView
    {
        public void ProgramRun()
        {

        }
        public string GetLogin()
        {
            Console.Write("Podaj login: ");
            return Console.ReadLine();
        }
        public string GetPassword()
        {
            Console.Write("Podaj haslo: ");
            return Console.ReadLine();
        }
    }
}
