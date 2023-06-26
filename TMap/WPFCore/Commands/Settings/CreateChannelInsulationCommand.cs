using System;

using CommunityToolkit.Mvvm.Messaging;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Settings;

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
            throw new Exception("Не удалось создать изоляционный слой коллектора!");

        channelInsulationMaterial.InitialTemperature = _viewModel.InitialTemperature;
        channelInsulationMaterial.LayerThickness = 1; // Все, что внутри коллектора и его стенки - толщина слоя будет 1 см.

        var insulation = new ChannelInsulation(channelInsulationMaterial) { Thickness = _viewModel.Thickness };

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
