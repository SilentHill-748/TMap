using System.Collections.ObjectModel;
using System.Linq;

namespace TMap.MVVM.Model.Settings;

public class RoadSettingsModel
{
    public RoadSettingsModel()
    {
        Layers = new ObservableCollection<RoadLayer>();
    }

    /// <summary>
    ///     Ширина проезжей части.
    /// </summary>
    public int RoadWidth { get; set; } // min 700 cm

    public int TotalRoadWidth => 2 * ((HasMound ? MoundWidth : 0) + RoadsideWidth + EdgeWidth) + RoadWidth;

    /// <summary>
    ///     Ширина насыпи, если тип дороги негородской.
    /// </summary>
    public int MoundWidth { get; set; } // min 50 cm

    /// <summary>
    ///     Высота насыпи, если тип дороги негородской.
    /// </summary>
    public int MoundHeight { get; set; } // min 20 cm

    /// <summary>
    ///     Флаг, указывающий, есть ли насыпь.
    /// </summary>
    public bool HasMound { get; set; }

    /// <summary>
    ///     Ширина обочины.
    /// </summary>
    public int RoadsideWidth { get; set; } // min 50 cm

    /// <summary>
    ///     Ширина края дороги.
    /// </summary>
    public int EdgeWidth { get; set; } // 'min = (mapWidth - 900) / 2', if contains mound, then 'min = (mapWidth - 800) / 2'.

    public int MaxDepth => Layers.Sum(x => x.Thickness);

    /// <summary>
    ///     Коллеция слоев дорожной одежды.
    /// </summary>
    public ObservableCollection<RoadLayer> Layers { get; set; }
}