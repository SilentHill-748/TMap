using System.Collections.ObjectModel;
using System.Linq;

namespace TMap.MVVM.Model.Settings;

public class RoadSettingsModel
{
    public RoadSettingsModel()
    {
        Layers = new ObservableCollection<Layer>();

        Layers.CollectionChanged += Layers_CollectionChanged;
    }

    public int RoadWidth { get; set; }

    public int TotalRoadWidth => 2 * ((HasMound ? MoundWidth : 0) + RoadsideWidth + EdgeWidth) + RoadWidth;

    public int MoundWidth { get; set; }

    public int MoundHeight { get; set; }

    public bool HasMound { get; set; }

    public int RoadsideWidth { get; set; }

    public int EdgeWidth { get; set; }

    public int MaxDepth { get; private set; }

    public ObservableCollection<Layer> Layers { get; set; }

    private void Layers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        MaxDepth = Layers.Sum(layer => layer.Thickness);
    }
}