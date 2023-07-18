using Container = SimpleInjector.Container;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterServicesExtention
{
    public static Container RegisterServices(this Container container)
    {
        container.RegisterSingleton<IMaterialService, MaterialService>();

        return container;
    } 
}
