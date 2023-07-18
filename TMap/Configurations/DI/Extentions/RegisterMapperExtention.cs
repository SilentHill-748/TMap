using Container = SimpleInjector.Container;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterMapperExtention
{
    public static Container RegisterMapper(this Container container, Action<IMapperConfigurationExpression> configuration)
    {
        container.RegisterSingleton<IMapper>(() => new Mapper(new MapperConfiguration(configuration)));

        return container;
    }
}
