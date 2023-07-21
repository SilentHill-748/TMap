namespace TMap.MVVM.Validation.Validators;

public class CreateRoadLayerValidator : AbstractValidator<CreateRoadLayerViewModel>
{
    public CreateRoadLayerValidator()
    {
        RuleFor(viewModel => viewModel.Thickness)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidationErrors.MaterialErrors.ThicknessError);

        RuleFor(viewModel => viewModel.Humidity)
            .GreaterThanOrEqualTo(0.00)
            .WithMessage(ValidationErrors.MaterialErrors.HumidityError);

        RuleFor(viewModel => viewModel.InitialTemperature)
            .InclusiveBetween(-70.00, 170.00)
            .WithMessage(ValidationErrors.MaterialErrors.InitTemperatureError);

        RuleFor(viewModel => viewModel.Material)
            .NotNull()
            .WithMessage(ValidationErrors.MaterialErrors.MaterialError);
    }
}
