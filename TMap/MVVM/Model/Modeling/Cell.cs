using System;

namespace TMap.MVVM.Model.Modeling;

/// <summary>
///     Represents a cell on map with size 1x1 cm.
/// </summary>
public class Cell
{
    /// <summary>
    ///     Температура точки на графике карты.
    /// </summary>
    public double Temperature { get; set; }
    /// <summary>
    ///     Тип грунта или материал трубопровода/дороги точки. 
    /// </summary>
    public Material? Material { get; set; }
}
