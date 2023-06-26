using System;

namespace TMap.MVVM.Model.Modeling;

public class MapLayer 
{
    public MapLayer(Material material)
    {
        ArgumentNullException.ThrowIfNull(material, nameof(material));

        Material = material;
    }

    /// <summary>
    ///     Толщина слоя в сантиметрах.
    /// </summary>
    public int Thickness { get; set; }

    /// <summary>
    ///     Содержит информацию по материалу слоя.
    /// </summary>
    public Material Material { get; set; }
}