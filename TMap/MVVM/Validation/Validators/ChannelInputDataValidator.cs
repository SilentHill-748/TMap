namespace TMap.MVVM.Validation.Validators;

public class ChannelInputDataValidator : AbstractValidator<ChannelInputDataViewModel>
{
    public ChannelInputDataValidator()
    {
        RuleFor(viewModel => viewModel.Thickness)
            .InclusiveBetween(5, 25)
            .WithMessage(ValidationErrors.PipelineSettingsErrors.PipelineChannelErrors.ThicknessError);

        RuleFor(viewModel => viewModel.ChannelDepth)
            .Must((viewModel, depth) => depth >= viewModel.Settings.RoadSettings.MaxDepth)
            .WithMessage(ValidationErrors.PipelineSettingsErrors.PipelineChannelErrors.ChannelDepthError);

        RuleFor(viewModel => viewModel.ChannelHeight)
            .Must(ChannelHeightPredicate)
            .WithMessage(viewModel => $"Высота коллектора должна быть между {viewModel.MinChannelHeightLayout} и {3000 - viewModel.MinChannelHeightLayout} см!");

        RuleFor(viewModel => viewModel.PipeCenterline)
            .Must((viewModel, value) => value > viewModel.MinCenterlinePosition && value < viewModel.MaxCenterlinePosition)
            .WithMessage(viewModel => $"Осевая линия труб должна быть между {viewModel.MinCenterlinePosition} и {viewModel.MaxCenterlinePosition} см!");

        RuleFor(viewModel => viewModel.InteraxalWidth)
            .InclusiveBetween(3, 10)
            .WithMessage(ValidationErrors.PipelineSettingsErrors.PipelineChannelErrors.InteraxalWidthError);
    }

    private bool ChannelHeightPredicate(ChannelInputDataViewModel viewModel, int value)
    {
        return  (value > viewModel.MinChannelHeightLayout) && 
                (value < 3000 - viewModel.MinChannelHeightLayout);
    }
}
