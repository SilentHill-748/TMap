using System;
using System.Text.Json.Serialization;
using System.Windows.Media;

namespace TMap.MVVM.Model.Modeling;

[Serializable]
public class Material
{
    [JsonPropertyName("ThermalConductivity")]
    /// <summary>
    ///     Теплопроводимость материала.
    /// </summary>
    public double ThermalConductivity { get; set; }

    [JsonIgnore]
    public double InitialTemperature { get; set; }

    [JsonPropertyName("Density")]
    /// <summary>
    ///     Плотность материала.
    /// </summary>
    public double Density { get; set; }

    [JsonPropertyName("Humidity")]
    /// <summary>
    ///     Влажность материала.
    /// </summary>
    public double Humidity { get; set; }

    [JsonPropertyName("Name")]
    /// <summary>
    ///     Название материала.
    /// </summary>
    public string Name { get; set; } = "None";

    [JsonIgnore]
    /// <summary>
    ///     Состояние материала.
    /// </summary>
    public bool IsFrozen { get; set; }

    [JsonPropertyName("Color")]
    public string Color { get; set; } = "#FFFFFFFF";
    public int LayerThickness { get; set; }

    public bool IsPipeCoolantMaterial { get; set; }

    public Color GetColor()
    {
        return (Color)ColorConverter.ConvertFromString(Color);
    }
}