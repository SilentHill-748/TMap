namespace TMap.MVVM.Validation.Validators;

public sealed class CreateMapLayerValidator : AbstractValidator<CreateMapLayerViewModel>
{
    public CreateMapLayerValidator()
    {
        RuleFor(viewModel => viewModel.Thickness)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidationErrors.MaterialErrors.ThicknessError);

        RuleFor(viewModel => viewModel.Humidity)
            .GreaterThanOrEqualTo(0.00)
            .WithMessage(ValidationErrors.MaterialErrors.HumidityError);

        RuleFor(viewModel => viewModel.InitTemperature)
            .InclusiveBetween(-70.00, 170.00)
            .WithMessage(ValidationErrors.MaterialErrors.InitTemperatureError);

        RuleFor(viewModel => viewModel.Material)
            .NotNull()
            .WithMessage(ValidationErrors.MaterialErrors.MaterialError);
    }
}
