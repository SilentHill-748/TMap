namespace TMap.MVVM.Model.Map;

public class MaterialModel
{
    public double ThermalConductivity { get; set; }

    public double Density { get; set; }

    public double Humidity { get; set; }

    public string Name { get; set; } = "None";

    public MaterialType Type { get; set; }

    public string ColorHexCode { get; set; } = "#FFFFFFFF";

    public Color GetColor()
    {
        return (Color)ColorConverter.ConvertFromString(ColorHexCode);
    }
}