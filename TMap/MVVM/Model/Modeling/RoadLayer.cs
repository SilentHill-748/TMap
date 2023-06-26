using System;

namespace TMap.MVVM.Model.Modeling;

public class RoadLayer
{
    public RoadLayer(Material material)
    {
        ArgumentNullException.ThrowIfNull(material, nameof(Material));

        Material = material;
    }

    public int Width { get; set; }
    public int Thickness { get; set; }
    public Material Material { get; set; }
}
