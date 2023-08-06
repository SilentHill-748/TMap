namespace TMap.WPFCore.Commands.Settings.PipelineChannel;

public class CreateChannelInsulationCommand : CommandBase
{
    private readonly CreateChannelInsulationViewModel _viewModel;

    public CreateChannelInsulationCommand(CreateChannelInsulationViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute()
    {
        var channelInsulationMaterial = _viewModel.Material;

        if (channelInsulationMaterial is not { })
            throw new MaterialException("Не удалось создать изоляционный слой коллектора! Не выбран материал изоляционного слоя!");

        var insulation = new ChannelInsulation()
        {
            Thickness = _viewModel.Thickness,
            InitialTemperature = _viewModel.InitialTemperature,
            Material = channelInsulationMaterial
        };

        WeakReferenceMessenger.Default.Send(new CreateChannelInsulationMessage(insulation));
        Reset();
    }

    public override bool CanExecute()
    {
        return _viewModel.ChannelInsulationCollection.Count < 3 && _viewModel.IsValid;
    }

    private void Reset()
    {
        _viewModel.Material = default;
        _viewModel.InitialTemperature = default;
        _viewModel.Thickness = default;
    }
}
