namespace TMap.WPFCore.Commands.Settings;

public class RemoveRoadLayerCommand : ParameterizedCommandBase<Layer>
{
    private readonly RoadSettingsViewModel _viewModel;

    public RemoveRoadLayerCommand(RoadSettingsViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute(Layer parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter, nameof(parameter));

        _viewModel.Settings.Layers.Remove(parameter);
    }
}