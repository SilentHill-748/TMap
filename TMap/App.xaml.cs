using System.IO;
using System.Windows;

using SimpleInjector;

using TMap.MVVM.View.Windows;

namespace TMap;

public partial class App : System.Windows.Application
{
    private readonly Container _container = new();

    public App()
    {
        SetNewCurrentDirectory();

        DispatcherUnhandledException += App_DispatcherUnhandledException;
    }

    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(e.Exception.Message, "Unhandled exception!");
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        _container.RegisterAppServices();

        MainWindow = new MainWindow()
        {
            DataContext = _container.GetInstance<MainViewModel>()
        };

        MainWindow.Show();

        base.OnStartup(e);
    }

    private static void SetNewCurrentDirectory()
    {
        string rootPath = new DirectoryInfo(Directory.GetCurrentDirectory()).Parent!.Parent!.Parent!.FullName;

        Directory.SetCurrentDirectory(rootPath);
    }
}
