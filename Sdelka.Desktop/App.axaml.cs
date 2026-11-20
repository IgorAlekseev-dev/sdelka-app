using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Sdelka.Application;
using Sdelka.Desktop.ViewModels;
using Sdelka.Desktop.Views;
using Sdelka.Infrastructure;
using System;

namespace Sdelka.Desktop;

public partial class App : Avalonia.Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            string connectionString = "Host=localhost;Port=5432;Database=MyAvito_DB;Username=postgres;Password=postgres;";
            string? envConnection = Environment.GetEnvironmentVariable("SDELKA_DB_CONNECTION");
            if (string.IsNullOrEmpty(envConnection) == false)
            {
                connectionString = envConnection;
            }


            IDataAccess dataAccess = new DataAccess(connectionString);
            OrderController orderController = new OrderController(dataAccess);
            MainViewModel mainViewModel = new MainViewModel(dataAccess, orderController);

            MainWindow mainWindow = new MainWindow();
            mainWindow.DataContext = mainViewModel;
            desktop.MainWindow = mainWindow;
        }

        base.OnFrameworkInitializationCompleted();
    }
}