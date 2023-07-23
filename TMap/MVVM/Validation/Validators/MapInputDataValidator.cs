namespace TMap.MVVM.Validation.Validators;

public class MapInputDataValidator : AbstractValidator<MapInputDataViewModel>
{
    public MapInputDataValidator()
    {
        RuleFor(viewModel => viewModel.EnvTemperature)
            .InclusiveBetween(-70.00, 170.00)
            .WithMessage("Начальная температура материала внешней среды должна быть между -70.00 и +170.00 °С!");
    }
}
