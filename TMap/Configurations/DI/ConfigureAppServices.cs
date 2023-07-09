using Microsoft.EntityFrameworkCore;

using SimpleInjector;

using TMap.Configurations.DI.Extentions;

namespace TMap.Configurations.DI;

public static class ConfigureAppServices
{
    public static Container RegisterServices(this Container container)
    {
        container.RegisterSingleton<MaterialHelper>();

        container
            .RegisterDbContext(opt => opt.UseSqlite("Data Source=tmap.db"))
            .RegisterRepositories()
            .RegisterServices()
            .RegisterModels()
            .RegisterViewModels()
            .RegisterWPFServices();

        return container;
    }
}