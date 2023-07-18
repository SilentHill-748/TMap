namespace TMap.WPFCore.Commands.Settings.PipelineChannel;

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
