using Microsoft.EntityFrameworkCore;

using SimpleInjector;

using TMap.Configurations.DI.Extentions;
using TMap.Domain.Mapper;
using TMap.Persistence;

namespace TMap.Configurations.DI;

public static class ConfigureAppServices
{
    public static Container RegisterAppServices(this Container container, string databasePath)
    {
        container.RegisterSingleton<MaterialHelper>();

        container
            .RegisterMapper(config =>
            {
                config.AddProfile<AutomapperProfile>();
            });

        container
            .RegisterDbContext(opt => opt.UseSqlite($"Data Source={databasePath}"))
            .RegisterRepositories()
            .RegisterServices()
            .RegisterModels()
            .RegisterViewModels()
            .RegisterWPFServices();

        container.Register<DataSeed>();

        return container;
    }
}