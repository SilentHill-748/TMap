namespace TMap.WPFCore.Commands.Settings.Map;

public class AddMapLayerCommand : CommandBase
{
    private readonly CreateMapLayerViewModel _viewModel;

    public AddMapLayerCommand(CreateMapLayerViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute()
    {
        var material = _viewModel.Material;

        material.Humidity = _viewModel.Humidity;

        var layer = new Layer()
        {
            InitialTemperature = _viewModel.InitTemperature,
            Material = material,
            Thickness = _viewModel.Thickness
        };

        WeakReferenceMessenger.Default.Send(new MapLayerCreateMessage(layer));
        Reset();
    }

    public override bool CanExecute()
    {
        return _viewModel.IsValid;
    }

    private void Reset()
    {
        _viewModel.Material = default;
        _viewModel.Thickness = default;
        _viewModel.Humidity = default;
        _viewModel.InitTemperature = default;
    }
}
