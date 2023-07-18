namespace TMap.WPFCore.Commands.Modeling;

public class CreateModelCommand : CommandBase
{
    private readonly MapViewModel _viewModel;
    private readonly MaterialStore _materialStore;

    public CreateModelCommand(MapViewModel viewModel, MaterialStore materialStore)
    {
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));

        _viewModel = viewModel;
        _materialStore = materialStore;
    }

    protected override void Execute()
    {
        var settings = _viewModel.Settings;
        var map = _viewModel.MapBitmap!;

        _viewModel.MathModel = new MathModel(settings, GetMaterialMap(_viewModel.Settings), map);
        _viewModel.MathModel.ModelStopped += MathModel_ModelStopped;
    }

    public override bool CanExecute()
    {
        var map = _viewModel.MapBitmap;

        return map is { } && 
            map.PixelWidth > 0 && 
            map.PixelHeight > 0;
    }

    private void MathModel_ModelStopped()
    {
        _viewModel.TemperatureSource = _viewModel.MathModel?.GetTemperatureMap();
    }

    private Dictionary<Color, MaterialModel> GetMaterialMap(SettingsModel settings)
    {
        var environmentDefaultMaterial = _materialStore.GetMaterial("Воздух");

        var materials = new List<MaterialModel>() { environmentDefaultMaterial, settings.PipelineSettings.Channel.Material };
        var map = new Dictionary<Color, MaterialModel>();

        materials.AddRange(settings.MapSettings.MapSoilLayers.Select(x => x.Material));
        materials.AddRange(settings.RoadSettings.Layers.Select(x => x.Material));
        materials.AddRange(settings.PipelineSettings.Channel.Pipes.Select(x => x.Material));
        materials.AddRange(settings.PipelineSettings.Channel.InsulationLayers.Select(x => x.Material));

        foreach (Pipe pipe in settings.PipelineSettings.Channel.Pipes)
            materials.AddRange(pipe.Insulation.Select(x => x.Material));

        foreach (MaterialModel material in materials)
        {
            var color = (Color)ColorConverter.ConvertFromString(material.ColorHexCode);

            _ = map.TryAdd(color, material);
        }

        return map;
    }
}
