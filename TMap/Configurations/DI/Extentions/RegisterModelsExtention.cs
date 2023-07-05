using SimpleInjector;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterModelsExtention
{
    public static Container RegisterModels(this Container container)
    {
        container.RegisterSingleton<MapSettingsModel>();
        container.RegisterSingleton<RoadSettingsModel>();
        container.RegisterSingleton<PipelineSettingsModel>();
        container.RegisterSingleton<SettingsModel>();

        return container;
    }
}
