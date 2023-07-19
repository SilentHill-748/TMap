namespace TMap.WPFCore.Commands.Map;

public class DrawMapCommand : CommandBase
{
    private readonly MapViewModel _viewModel;
    private readonly MaterialStore _materialStore;

    public DrawMapCommand(MapViewModel mapViewModel, MaterialStore materialStore)
    {
        ArgumentNullException.ThrowIfNull(mapViewModel, nameof(mapViewModel));
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));

        _viewModel = mapViewModel;
        _materialStore = materialStore;
    }

    protected override void Execute()
    {
        var width = _viewModel.Settings.MapSettings.MapWidth;
        var height = _viewModel.Settings.MapSettings.MapHeight + _viewModel.Settings.RoadSettings.MoundHeight;

        _viewModel.MapBitmap = new WriteableBitmap(width + 2, height + 2, 96, 96, PixelFormats.Bgra32, null);
        _viewModel.MapBitmap.Clear(Colors.White);

        var environmentDefaultMaterial = _materialStore.GetMaterial("Воздух");

        var drawingService = new DrawingService(_viewModel.Settings, _viewModel.MapBitmap, environmentDefaultMaterial);

        drawingService.DrawMainMap();
        drawingService.DrawRoadMap();
        drawingService.DrawPipelineMap();
    }

    public override bool CanExecute()
    {
        return _viewModel.Settings.IsCompleted;
    }
}
