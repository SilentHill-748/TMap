using SimpleInjector;

using TMap.Configurations.DI.Extentions;

namespace TMap.Configurations.DI;

public static class ConfigureAppServices
{
    public static Container RegisterServices(this Container container)
    {
        container.RegisterSingleton<MaterialHelper>();

        container
            .RegisterModels()
            .RegisterViewModels()
            .RegisterWPFServices();

        return container;
    }
}