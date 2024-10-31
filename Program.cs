using ElektronicznyKonsolowy.Controller.MainsControllers;
using ElektronicznyKonsolowy.Models;
using System;
using Spectre.Console;
using ElektronicznyKonsolowy.View.MainViews;
class Program
{
    static void Main(string[] args)
    {
        MainView mainView = new MainView();

        MyDbContext db = new MyDbContext();

        var mainController = new MainController(db, mainView);

        mainController.Run();
    }
}