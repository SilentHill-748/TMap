using SimpleInjector;

using TMap.Application.Services.Material;
using TMap.Domain.Abstractions.Services.Material;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterServicesExtention
{
    public static Container RegisterServices(this Container container)
    {
        container.RegisterSingleton<IMaterialService, MaterialService>();

        return container;
    } 
}
