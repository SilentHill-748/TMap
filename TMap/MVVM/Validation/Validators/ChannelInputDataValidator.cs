namespace TMap.MVVM.Validation.Validators;

public class ChannelInputDataValidator : AbstractValidator<ChannelInputDataViewModel>
{
    public ChannelInputDataValidator()
    {
        RuleFor(viewModel => viewModel.Thickness)
            .InclusiveBetween(5, 25)
            .WithMessage("Толщина стенок коллектора должна быть между 5 и 25 см!");

        RuleFor(viewModel => viewModel.ChannelDepth)
            .Must((viewModel, depth) => depth >= viewModel.Settings.RoadSettings.MaxDepth)
            .WithMessage(viewModel =>
            {
                var depth = viewModel.Settings.RoadSettings.MaxDepth;
                return $"Глубина заложения коллектора должна быть на уровне дорожной конструкции или глубже ({depth} см)!";
            });

        RuleFor(viewModel => viewModel.ChannelHeight)
            .Must(ChannelHeightPredicate)
            .WithMessage(viewModel =>
            {
                var minHeight = viewModel.Data.MinChannelHeightLayout;
                var maxHeight = viewModel.Data.MaxChannelHeightLayout;

                return $"Высота коллектора должна быть между {minHeight} и {maxHeight} см!";
            });

        RuleFor(viewModel => viewModel.PipeCenterline)
            .Must(ChannelPipeCenterlinePredicate)
            .WithMessage(viewModel =>
            {
                var minCenterline = viewModel.Data.MinCenterlinePosition;
                var maxCenterline = viewModel.Data.MaxCenterlinePosition;

                return $"Осевая линия труб должна быть между {minCenterline} и {maxCenterline} см!";
            });

        RuleFor(viewModel => viewModel.InteraxalWidth)
            .InclusiveBetween(3, 10)
            .WithMessage("Расстояние между труб должно быть между 3 и 10 см!");
    }

    private bool ChannelHeightPredicate(ChannelInputDataViewModel viewModel, int value)
    {
        return  (value > viewModel.Data.MinChannelHeightLayout) && 
                (value < 3000 - viewModel.Data.MinChannelHeightLayout);
    }

    private bool ChannelPipeCenterlinePredicate(ChannelInputDataViewModel viewModel, int value)
    {
        return  value > viewModel.Data.MinCenterlinePosition && 
                value < viewModel.Data.MaxCenterlinePosition;
    }
}
