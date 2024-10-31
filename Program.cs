using ElektronicznyKonsolowy.Controller;
using ElektronicznyKonsolowy.Models;
using ElektronicznyKonsolowy.View;
using System;
using Spectre.Console;
class Program
{
    static void Main(string[] args)
    {
        MainView mainView = new MainView();
        MyDbContext db = new MyDbContext();
        var mainController = new MainController(db, mainView);
        mainController.Run();
        Console.WriteLine("Naciśnij dowolny klawisz, aby zakończyć.");
        Console.ReadKey();
    }
}