namespace TMap.MVVM.Validation.Validators;

public class PipeInputDataValidator : AbstractValidator<PipeInputDataViewModel>
{
    public PipeInputDataValidator()
    {
        RuleFor(viewModel => viewModel.Radius)
            .InclusiveBetween(6, 27)
            .WithMessage(ValidationErrors.PipelineSettingsErrors.PipelinePipeErrors.CreatePipeErrors.RadiusError);

        RuleFor(viewModel => viewModel.MaterialTemperature)
            .InclusiveBetween(-70.00, 170.00)
            .WithMessage(ValidationErrors.PipelineSettingsErrors.PipelinePipeErrors.CreatePipeErrors.PipeMaterialTemperatureError);

        RuleFor(viewModel => viewModel.CoolantTemperature)
            .InclusiveBetween(-10.00, 400.00)
            .WithMessage(ValidationErrors.PipelineSettingsErrors.PipelinePipeErrors.CreatePipeErrors.TemperatureError);

        RuleFor(viewModel => viewModel.Thickness)
            .InclusiveBetween(1, 3)
            .WithMessage(ValidationErrors.PipelineSettingsErrors.PipelinePipeErrors.CreatePipeErrors.ThicknessError);

        RuleFor(viewModel => viewModel.PipeType)
            .NotNull()
            .WithMessage(ValidationErrors.PipelineSettingsErrors.PipelinePipeErrors.CreatePipeErrors.PipeMaterialError);
    }
}
