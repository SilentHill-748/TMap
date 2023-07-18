namespace TMap.WPFCore.Commands.Modeling;

public class RunModelCommand : CommandBase
{
    private readonly MapViewModel _viewModel;

    public RunModelCommand(MapViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute()
    {
        _viewModel.MathModel?.Run();
    }

    public override bool CanExecute()
    {
        return _viewModel.MathModel is { };
    }
}
