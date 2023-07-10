using System.Windows.Media;

namespace TMap.MVVM.Model.Modeling;

public class MaterialModel
{
    /// <summary>
    ///     Теплопроводимость материала.
    /// </summary>
    public double ThermalConductivity { get; set; }

    public double InitialTemperature { get; set; }

    /// <summary>
    ///     Плотность материала.
    /// </summary>
    public double Density { get; set; }

    /// <summary>
    ///     Влажность материала.
    /// </summary>
    public double Humidity { get; set; }

    /// <summary>
    ///     Название материала.
    /// </summary>
    public string Name { get; set; } = "None";

    public string ColorHexCode { get; set; } = "#FFFFFFFF";

    public int LayerThickness { get; set; }

    public Color GetColor()
    {
        return (Color)ColorConverter.ConvertFromString(ColorHexCode);
    }
}