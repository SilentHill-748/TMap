using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

using SimpleInjector;

using TMap.MVVM.Stores;
using TMap.MVVM.View.Windows;
using TMap.Persistence;

namespace TMap;

public partial class App : System.Windows.Application
{
    private readonly Container _container = new();
    private readonly string _rootAppPath;

    public App()
    {
        _rootAppPath = Path.GetFullPath(@"..\..\..\..\");

        DispatcherUnhandledException += App_DispatcherUnhandledException;
    }

    private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show(e.Exception.Message, "Unhandled exception!");
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await InitializeServicesAsync();

        MainWindow = new MainWindow()
        {
            DataContext = _container.GetInstance<MainViewModel>()
        };

        MainWindow.Show();

        base.OnStartup(e);
    }

    private async Task InitializeServicesAsync()
    {
        _container.RegisterAppServices();

        await SeedDataAsync();

        ConfigurePipelineSettingsModel();
    }

    private async Task SeedDataAsync()
    {
        await _container.GetInstance<DataSeed>().SeedAsync(_rootAppPath);
        
        _container.GetInstance<MaterialStore>().Load();
    }

    private void ConfigurePipelineSettingsModel()
    {
        var pipelineSM = _container.GetInstance<PipelineSettingsModel>();

        var channelMaterial = _container
            .GetInstance<MaterialStore>()
            .GetMaterial("Железобетон");

        pipelineSM.Channel.Material = channelMaterial;
    }
}