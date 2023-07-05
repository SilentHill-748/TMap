using SimpleInjector;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterViewModelsExtention
{
    public static Container RegisterViewModels(this Container container)
    {
        container.RegisterSingleton<MapViewModel>();
        container.RegisterSingleton<MapSettingsViewModel>();
        container.RegisterSingleton<RoadSettingsViewModel>();
        container.RegisterSingleton<PipeSettingsViewModel>();
        container.RegisterSingleton<PipelineChannelSettingsViewModel>();
        container.Register<MainViewModel>();

        return container;
    }
}
