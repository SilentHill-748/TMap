using Microsoft.EntityFrameworkCore;

using SimpleInjector;

using TMap.Configurations.DI.Extentions;
using TMap.Domain.Mapper;
using TMap.MapperProfiles;
using TMap.Persistence;

namespace TMap.Configurations.DI;

public static class ConfigureAppServices
{
    public static Container RegisterAppServices(this Container container)
    {
        container
            .RegisterMapper(config =>
            {
                config.AddProfile<AutomapperProfile>();
                config.AddProfile<MaterialModelProfile>();
            });

        container
            .RegisterViewModels()
            .RegisterModels()
            .RegisterStores()
            .RegisterServices()
            .RegisterRepositories()
            .RegisterDbContext(opt => opt.UseSqlite($"Data Source=tmap.db"))
            .RegisterWPFServices();

        container.Register<DataSeed>();

        container.Verify();

        return container;
    }
}