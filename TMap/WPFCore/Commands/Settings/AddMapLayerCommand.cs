using System;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Settings;

public class AddMapLayerCommand : CommandBase
{
    private readonly CreateLayerViewModel _viewModel;
    private readonly Action<Layer>? _callback;

    public AddMapLayerCommand(CreateLayerViewModel viewModel, Action<Layer>? callback)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
        _callback = callback;
    }

    protected override void Execute()
    {
        var material = _viewModel.Material;

        if (material is not { })
            throw new Exception("Материал не установлен!");

        material.Humidity = _viewModel.Humidity;
        material.InitialTemperature = _viewModel.InitTemperature;
        material.LayerThickness = _viewModel.Thickness;

        var layer = new Layer(material)
        {
            Thickness = _viewModel.Thickness
        };

        _callback?.Invoke(layer);
    }
}
