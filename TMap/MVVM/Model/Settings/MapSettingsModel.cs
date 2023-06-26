using System.Collections.ObjectModel;
using System.Linq;

namespace TMap.MVVM.Model.Settings;

public class MapSettingsModel
{
    public MapSettingsModel()
    {
        MapSoilLayers = new ObservableCollection<MapLayer>();
    }

    /// <summary>
    ///     Ширина карты.
    /// </summary>
    public int MapWidth { get; set; }

    /// <summary>
    ///     Глубина карты.
    /// </summary>
    public int MapHeight => MapSoilLayers.Sum(x => x.Thickness);

    /// <summary>
    ///     Температура внешней среды для моделирования.
    /// </summary>
    public double EnvironmentTemperature { get; set; }

    /// <summary>
    ///     Флаг, указывающий, какая проекция дороги будет использоваться.
    /// </summary>
    public bool IsFrontView { get; set; }

    /// <summary>
    ///     Слои карты.
    /// </summary>
    public ObservableCollection<MapLayer> MapSoilLayers { get; set; }
}
