using Container = SimpleInjector.Container;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterAllValidatorsExtention
{
    public static Container RegisterAllValidators(this Container container)
    {
        var targetServiceType = typeof(AbstractValidator<>);
        var assembly = Assembly.GetExecutingAssembly();

        var validators = container.GetTypesToRegister(targetServiceType, assembly);

        foreach (Type validatorType in validators)
        {
            container.RegisterSingleton(validatorType, validatorType);
        }

        return container;
    }
}
