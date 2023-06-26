﻿using System;

using TMap.WPFCore.Commands.Base;

namespace TMap.MVVM.ViewModel.Settings;

public class RemovePipeInsulationLayerCommand : ParameterizedCommandBase<RadialInsulation>
{
    private readonly PipeSettingsViewModel _viewModel;

    public RemovePipeInsulationLayerCommand(PipeSettingsViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
    }

    protected override void Execute(RadialInsulation parameter)
    {
        ArgumentNullException.ThrowIfNull(parameter, nameof(parameter));

        _viewModel.PipeInsulationCollection.Remove(parameter);
    }
}
