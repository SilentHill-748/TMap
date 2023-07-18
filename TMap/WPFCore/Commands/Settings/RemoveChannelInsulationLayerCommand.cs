namespace TMap.WPFCore.Commands.Settings;

public class RemoveChannelInsulationLayerCommand : ParameterizedCommandBase<ChannelInsulation>
{
    private readonly PipelineChannelSettingsViewModel _viewModel;

    public RemoveChannelInsulationLayerCommand(PipelineChannelSettingsViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute(ChannelInsulation parameter)
    {
        _viewModel.Settings.PipelineSettings.Channel.InsulationLayers.Remove(parameter);
    }
}
