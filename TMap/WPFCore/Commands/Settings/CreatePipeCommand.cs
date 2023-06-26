using System;
using System.Collections.Generic;
using System.Windows;

using CommunityToolkit.Mvvm.Messaging;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Settings;

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

        if (pipeType is not { })
            throw new Exception("Ошибка создания трубы, нет материала!");

        pipeType.InitialTemperature = pipeData.CoolantTemperature;
        pipeType.LayerThickness = 1;

        var pipe = new Pipe(pipeType)
        {
            Radius = pipeData.Radius,
            Temperature = pipeData.CoolantTemperature,
            Thickness = pipeData.Thickness
        };

        AddInsolutionLayers(pipe, _viewModel.PipeInsulationCollection);

        WeakReferenceMessenger.Default.Send(new CreatePipeMessage(pipe));

        Reset();
    }

    private static void AddInsolutionLayers(Pipe pipe, IEnumerable<RadialInsulation> insulationLayers)
    {
        foreach (RadialInsulation insulation in insulationLayers)
            pipe.Insulation.Add(insulation);
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
