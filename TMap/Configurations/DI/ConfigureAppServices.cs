using Microsoft.EntityFrameworkCore;

using SimpleInjector;

using TMap.Configurations.DI.Extentions;
using TMap.Domain.Mapper;
using TMap.MapperProfiles;
using TMap.Persistence;

namespace TMap.Configurations.DI;

public static class ConfigureAppServices
{
    public static Container RegisterAppServices(this Container container, string databasePath)
    {
        container
            .RegisterMapper(config =>
            {
                config.AddProfile<AutomapperProfile>();
                config.AddProfile<MaterialModelProfile>();
            });

        container
            .RegisterDbContext(opt => opt.UseSqlite($"Data Source={databasePath}"))
            .RegisterRepositories()
            .RegisterServices()
            .RegisterViewModels()
            .RegisterModels()
            .RegisterWPFServices();

        container.Register<DataSeed>();

        return container;
    }
}