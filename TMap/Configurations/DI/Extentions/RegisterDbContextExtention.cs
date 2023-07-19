﻿using Container = SimpleInjector.Container;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterDbContextExtention
{
    public static Container RegisterDbContext(this Container container, Action<DbContextOptionsBuilder> opt)
    {
        var optionsBuilder = new DbContextOptionsBuilder();

        opt(optionsBuilder);

        container.RegisterSingleton(() => new TMapDbContext(optionsBuilder.Options));

        return container;
    }
}
