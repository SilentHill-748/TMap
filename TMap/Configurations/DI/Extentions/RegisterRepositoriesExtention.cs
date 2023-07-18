using Container = SimpleInjector.Container;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterRepositoriesExtention
{
    public static Container RegisterRepositories(this Container container)
    {
        container.RegisterSingleton<IMaterialRepository, MaterialRepository>();

        return container;
    }
}
