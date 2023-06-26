﻿using System;

namespace TMap.MVVM.Model.Pipeline;

/// <summary>
///     Represents a pipe insulation.
/// </summary>
public class RadialInsulation
{
    public RadialInsulation(Material insulationMaterial)
    {
        ArgumentNullException.ThrowIfNull(insulationMaterial, nameof(insulationMaterial));

        Material = insulationMaterial;
    }

    /// <summary>
    ///     Толщина или ширина изоляции трубы.
    /// </summary>
    public int Thickness { get; set; }

    /// <summary>
    ///     Материал изоляции, из которого состоит данное изоляционное покрытие.
    /// </summary>
    public Material Material { get; set; }
}
