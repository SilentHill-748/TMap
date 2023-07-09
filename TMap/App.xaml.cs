using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Microsoft.EntityFrameworkCore;

using SimpleInjector;

using TMap.MVVM.View.Windows;
using TMap.Persistence;

namespace TMap;

public partial class App : System.Windows.Application
{
    private readonly Container _container = new();
    private readonly string _workPath;
    private readonly string _rootAppPath;
    private readonly string _databasePath;

    public App()
    {
        _workPath = Directory.GetCurrentDirectory();
        _rootAppPath = Path.GetFullPath(@"..\..\..\..\");
        _databasePath = Path.Combine(_workPath, "tmap.db");

        Environment.CurrentDirectory = _rootAppPath;

        DispatcherUnhandledException += App_DispatcherUnhandledException;
    }

    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(e.Exception.Message, "Unhandled exception!");
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        _container.RegisterAppServices(_databasePath);

        await SeedDataAsync();

        MainWindow = new MainWindow()
        {
            DataContext = _container.GetInstance<MainViewModel>()
        };

        MainWindow.Show();

        base.OnStartup(e);
    }

    private async Task SeedDataAsync()
    {
        var workDir = new DirectoryInfo(_workPath);

        var databaseFiles = workDir.GetFiles("*.db");

        if (!databaseFiles.Any())
        {
            _container.GetInstance<TMapDbContext>().Database.Migrate();
            await _container.GetInstance<DataSeed>().SeedAsync();
        }
    }
}
