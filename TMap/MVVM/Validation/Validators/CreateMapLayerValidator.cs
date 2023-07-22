namespace TMap.MVVM.Validation.Validators;

public sealed class CreateMapLayerValidator : AbstractValidator<CreateMapLayerViewModel>
{
    public CreateMapLayerValidator()
    {
        RuleFor(viewModel => viewModel.Thickness)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Толщина слоя должна быть не меньше 1 см!");

        RuleFor(viewModel => viewModel.Humidity)
            .GreaterThanOrEqualTo(0.00)
            .WithMessage("Влажность должна быть не ниже 0 гр/м³!");

        RuleFor(viewModel => viewModel.InitTemperature)
            .InclusiveBetween(-70.00, 170.00)
            .WithMessage("Начальная температура материала грунта должна быть между -70.00 и +170.00 °С!");

        RuleFor(viewModel => viewModel.Material)
            .NotNull()
            .WithMessage("Не выбран материал грунта!");
    }
}
