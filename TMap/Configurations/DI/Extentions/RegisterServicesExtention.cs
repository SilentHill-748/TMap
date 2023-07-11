using SimpleInjector;

using TMap.Application.Services.Material;
using TMap.Domain.Abstractions.Services.Material;
using TMap.MVVM.Facades;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterServicesExtention
{
    public static Container RegisterServices(this Container container)
    {
        container.Register<IMaterialService, MaterialService>();
        container.Register<MaterialFacade>();

        return container;
    } 
}
