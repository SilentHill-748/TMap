using Container = SimpleInjector.Container;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterStoresExtention
{
    public static Container RegisterStores(this Container container)
    {
        container.RegisterSingleton<MaterialStore>();

        return container;
    }
}
