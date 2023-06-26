using System;

using CommunityToolkit.Mvvm.Messaging;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Settings;

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
        if (_viewModel.Material is not { }) return;

        var material = _viewModel.Material;
       
        material.InitialTemperature = _viewModel.InitialTemperature;
        material.Humidity = _viewModel.Humidity;
        material.LayerThickness = _viewModel.Thickness;

        var layer = new RoadLayer(material)
        {
            Thickness = _viewModel.Thickness,
            Width = _viewModel.Width
        };

        //TODO: Не весь код переписан под использование шины сообщенй!
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
