using System;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Settings;

public class RemoveRoadLayerCommand : ParameterizedCommandBase<RoadLayer>
{
    private readonly RoadSettingsViewModel _viewModel;

    public RemoveRoadLayerCommand(RoadSettingsViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute(RoadLayer parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter, nameof(parameter));

        _viewModel.Settings.Layers.Remove(parameter);
    }
}