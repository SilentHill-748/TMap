using TMap.Exceptions;

namespace TMap.WPFCore.Commands.Settings.Pipeline;

public class CreatePipeCommand : CommandBase
{
    private readonly PipeSettingsViewModel _viewModel;

    public CreatePipeCommand(PipeSettingsViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute()
    {
        var pipeData = _viewModel.InputPipeDataView;
        var pipeType = pipeData.PipeType;

        var pipe = new Pipe()
        {
            Material = pipeType,
            Radius = pipeData.Radius,
            InitialTemperature = pipeData.MaterialTemperature,
            CoolantTemperature = pipeData.CoolantTemperature,
            Thickness = pipeData.Thickness
        };

        pipe.Insulation.UpdateCollection(_viewModel.PipeInsulationCollection);

        WeakReferenceMessenger.Default.Send(new CreatePipeMessage(pipe));

        Reset();
    }

    public void Reset()
    {
        _viewModel.InputPipeDataView.CoolantTemperature = default;
        _viewModel.InputPipeDataView.MaterialTemperature = default;
        _viewModel.InputPipeDataView.PipeType = default;
        _viewModel.InputPipeDataView.Radius = default;
        _viewModel.InputPipeDataView.Thickness = default;
    }
}
