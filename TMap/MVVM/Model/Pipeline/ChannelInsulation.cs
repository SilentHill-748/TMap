using System;

namespace TMap.MVVM.Model.Pipeline;

public class ChannelInsulation
{
    public ChannelInsulation(MaterialModel insulationMaterial)
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
    public MaterialModel Material { get; set; }
}
