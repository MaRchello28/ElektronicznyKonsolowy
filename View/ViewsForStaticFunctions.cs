using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElektronicznyKonsolowy.View
{
    public static class ViewsForStaticFunctions
    {
        public static void LoginExists()
        {
            AnsiConsole.MarkupLine("[maroon]Istnieje już taki login[/]");
            AnsiConsole.MarkupLine("[maroon]Naciśnij dowolny przycisk, żeby przejść dalej[/]");
        }
        public static void ValueIsNull()
        {
            AnsiConsole.MarkupLine("[maroon]Imie nie może być null[/]");
        }
        public static void BadParse(string value)
        {
            AnsiConsole.MarkupLine("[maroon]Zostala podana zla wartość: " + value + "[/]");
        }
        public static void ErrorLength()
        {
            AnsiConsole.MarkupLine("[maroon]Zbyt mała/duża liczba znaków[/]");
        }
    }
}
