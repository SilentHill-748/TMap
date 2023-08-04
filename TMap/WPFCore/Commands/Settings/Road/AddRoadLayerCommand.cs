using TMap.Exceptions;

namespace TMap.WPFCore.Commands.Settings.Road;

public class AddRoadLayerCommand : CommandBase
{
    private readonly CreateRoadLayerViewModel _viewModel;

    public AddRoadLayerCommand(CreateRoadLayerViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute()
    {
        if (_viewModel.Material is not { })
            throw new MaterialException("Не создан слой дорожной конструкции! Не выбран материал слоя!");

        var material = _viewModel.Material;

        material.Humidity = _viewModel.Humidity;

        var layer = new Layer()
        {
            Material = material,
            InitialTemperature = _viewModel.InitialTemperature,
            Thickness = _viewModel.Thickness,
            Width = _viewModel.Width
        };

        WeakReferenceMessenger.Default.Send(new CreateRoadLayerMessage(layer));

        ResetInputs();
    }

    private void ResetInputs()
    {
        _viewModel.InitialTemperature = default;
        _viewModel.Humidity = default;
        _viewModel.Width = default;
        _viewModel.Thickness = default;
        _viewModel.Material = default;
    }
}
