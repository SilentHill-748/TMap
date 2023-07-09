using Microsoft.EntityFrameworkCore;

using SimpleInjector;

using TMap.Configurations.DI.Extentions;
using TMap.Domain.Mapper;

namespace TMap.Configurations.DI;

public static class ConfigureAppServices
{
    public static Container RegisterServices(this Container container)
    {
        container.RegisterSingleton<MaterialHelper>();

        container
            .RegisterMapper(config =>
            {
                config.AddProfile<AutomapperProfile>();
            });

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