using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Modeling;

public class CreateModelCommand : CommandBase
{
    private readonly MapViewModel _viewModel;
    private readonly MaterialHelper _materialHelper;

    public CreateModelCommand(MapViewModel viewModel, MaterialHelper materialHelper)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));
        ArgumentNullException.ThrowIfNull(materialHelper, nameof(materialHelper));

        _viewModel = viewModel;
        _materialHelper = materialHelper;
    }

    protected override void Execute()
    {
        var settings = _viewModel.Settings;
        var map = _viewModel.MapBitmap!;

        _viewModel.MathModel = new MathModel(settings, GetMaterialMap(_viewModel.Settings), map);
        _viewModel.MathModel.ModelStopped += MathModel_ModelStopped;
    }

    private void MathModel_ModelStopped()
    {
        _viewModel.TemperatureSource = _viewModel.MathModel?.GetTemperatureMap();
    }

    public override bool CanExecute()
    {
        var map = _viewModel.MapBitmap;

        return map is { } && 
            map.PixelWidth > 0 && 
            map.PixelHeight > 0;
    }

    private Dictionary<Color, Material> GetMaterialMap(SettingsModel settings)
    {
        var materials = new List<Material>() { _materialHelper.DefaultMaterial, settings.PipelineSettings.Channel.Material };
        var map = new Dictionary<Color, Material>();

        materials.AddRange(settings.MapSettings.MapSoilLayers.Select(x => x.Material));
        materials.AddRange(settings.RoadSettings.Layers.Select(x => x.Material));
        materials.AddRange(settings.PipelineSettings.Channel.Pipes.Select(x => x.Material));
        materials.AddRange(settings.PipelineSettings.Channel.InsulationLayers.Select(x => x.Material));

        foreach (Pipe pipe in settings.PipelineSettings.Channel.Pipes)
            materials.AddRange(pipe.Insulation.Select(x => x.Material));

        foreach (Material material in materials)
        {
            var color = (Color)ColorConverter.ConvertFromString(material.Color);

            _ = map.TryAdd(color, material);
        }

        return map;
    }
}
