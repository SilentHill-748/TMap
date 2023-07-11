using SimpleInjector;

using TMap.MVVM.Stores;

namespace TMap.Configurations.DI.Extentions;
public static class RegisterStoresExtention
{
    public static Container RegisterStores(this Container container)
    {
        container.Register<MaterialStore>();

        return container;
    }
}
