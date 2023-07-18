namespace TMap.MVVM.Model.Settings;

public class MapSettingsModel
{
    public MapSettingsModel()
    {
        MapSoilLayers = new ObservableCollection<Layer>();

        MapSoilLayers.CollectionChanged += MapSoilLayers_CollectionChanged;
    }

    public int MapWidth { get; set; }

    public int MapHeight { get; private set; }

    public double EnvironmentTemperature { get; set; }

    public bool IsFrontView { get; set; }

    public ObservableCollection<Layer> MapSoilLayers { get; set; }

    private void MapSoilLayers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        MapHeight = MapSoilLayers.Sum(x => x.Thickness);
    }
}
