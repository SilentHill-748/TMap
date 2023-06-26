using System;

namespace TMap.MVVM.Model.Pipeline;

public class ChannelInsulation
{
    public ChannelInsulation(Material insulationMaterial)
    {
        ArgumentNullException.ThrowIfNull(insulationMaterial, nameof(insulationMaterial));

        Material = insulationMaterial;
    }

    /// <summary>
    ///     Толщина слоя изоляции в сантиметрах.
    /// </summary>
    public int Thickness { get; set; }

    /// <summary>
    ///     Материал изоляции.
    /// </summary>
    public Material Material { get; set; }
}
