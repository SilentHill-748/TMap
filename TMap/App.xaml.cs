﻿using WpfApp = System.Windows.Application;
using Container = SimpleInjector.Container;

namespace TMap;

public partial class App : WpfApp
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
        AddAllViewToViewModelDataTemplates();

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

    private void AddAllViewToViewModelDataTemplates()
    {
        var templates = new ViewToViewModelDataTemplateGeneratorService()
            .GenerateTemplates();

        foreach (DataTemplate template in templates)
        {
            Resources.Add(template.DataTemplateKey, template);
        }
    }
}