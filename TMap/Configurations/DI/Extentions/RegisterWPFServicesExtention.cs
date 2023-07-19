using Container = SimpleInjector.Container;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterWPFServicesExtention
{
    public static Container RegisterWPFServices(this Container container)
    {
        container.RegisterSingleton(() => new NavigationService(container));

        return container;
    }
}
