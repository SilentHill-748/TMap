﻿namespace TMap.Domain.Entities.Material;

public class Material
{
    public int MaterialId { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Density { get; set; }
    public double Humidity { get; set; }
    public MaterialType Type { get; set; }
    public string ColorHexCode { get; set; } = "#ffffffff";
}
