namespace TMap.MVVM.Validation.Validators;

public class CreatePipeInsulationValidator : AbstractValidator<CreatePipeInsulationViewModel>
{
    public CreatePipeInsulationValidator()
    {
        RuleFor(viewModel => viewModel.Thickness)
            .GreaterThanOrEqualTo(1)
            .WithMessage(ValidationErrors.PipelineSettingsErrors.InsulationErrors.ThicknessError);

        RuleFor(viewModel => viewModel.InitialTemperature)
            .InclusiveBetween(-70.00, 170.00)
            .WithMessage(ValidationErrors.MaterialErrors.InitTemperatureError);
        
        RuleFor(viewModel => viewModel.InsulationMaterial)
            .NotNull()
            .WithMessage(ValidationErrors.MaterialErrors.MaterialError);
    }
}
