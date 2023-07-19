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
        var colorMaterialMap = GetMaterialColorsMap();

        _viewModel.MathModel = new MathModel(settings, colorMaterialMap, map);

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

    private Dictionary<Color, MaterialModel> GetMaterialColorsMap()
    {
        var materials = _materialStore.Materials;
        var map = new Dictionary<Color, MaterialModel>();

        foreach (MaterialModel material in materials)
        {
            var color = material.GetColor();

            _ = map.TryAdd(color, material);
        }

        return map;
    }
}