namespace TMap.MVVM.Validation.Validators;

public class RoadInputDataValidator : AbstractValidator<RoadInputDataViewModel>
{
    public RoadInputDataValidator()
    {
        RuleFor(viewModel => viewModel.Width)
            .GreaterThanOrEqualTo(700)
            .WithMessage(ValidationErrors.RoadSettingsErrors.InputRoadSettingsErrors.RoadWidthError);

        RuleFor(viewModel => viewModel.MoundWidth)
            .InclusiveBetween(50, 100)
            .When(viewModel => viewModel.HasMound)
            .WithMessage(ValidationErrors.RoadSettingsErrors.InputRoadSettingsErrors.MoundWidthError);

        RuleFor(viewModel => viewModel.MoundHeight)
            .InclusiveBetween(20, 100)
            .When(viewModel => viewModel.HasMound)
            .WithMessage(ValidationErrors.RoadSettingsErrors.InputRoadSettingsErrors.MoundHeightError);

        RuleFor(viewModel => viewModel.RoadsideWidth)
            .InclusiveBetween(25, 50)
            .WithMessage(ValidationErrors.RoadSettingsErrors.InputRoadSettingsErrors.RoadsideWidthError);

        RuleFor(viewModel => viewModel.EdgeWidth)
            .GreaterThanOrEqualTo(50)
            .WithMessage(ValidationErrors.RoadSettingsErrors.InputRoadSettingsErrors.EdgeWidthError);
    }
}
