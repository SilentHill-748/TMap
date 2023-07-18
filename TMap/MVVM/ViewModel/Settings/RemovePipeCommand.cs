namespace TMap.MVVM.ViewModel.Settings;

public class RemovePipeCommand : ParameterizedCommandBase<Pipe>
{
    private readonly PipeSettingsViewModel _viewModel;

    public RemovePipeCommand(PipeSettingsViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute(Pipe parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter, nameof(parameter));

        _viewModel.Settings.Channel.Pipes.Remove(parameter);
    }
}
