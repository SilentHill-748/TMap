using SimpleInjector;

using TMap.Domain.Abstractions.Repositories;
using TMap.Persistence.Repositories;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterRepositoriesExtention
{
    public static Container RegisterRepositories(this Container container)
    {
        container.Register<IMaterialRepository, MaterialRepository>();

        return container;
    }
}
