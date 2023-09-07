using TMap.Domain.Entities.Material;

namespace TMap.Domain.DTO.Material;

public class MaterialDTO
{
    public MaterialDTO()
    {
        Name = string.Empty;
        ColorHexCode = "#ffffffff";
    }

    public MaterialDTO(Entities.Material.Material material)
    {
        MaterialId = material.MaterialId;
        Name = material.Name;
        ThermalConductivity = material.ThermalConductivity;
        Density = material.Density;
        Humidity = material.Humidity;
        Type = material.Type;
        ColorHexCode = material.ColorHexCode;
    }

    public int MaterialId { get; set; }
    public string Name { get; set; }
    public double ThermalConductivity { get; set; }
    public double SpecificHeat { get; set; }
    public double Density { get; set; }
    public double Humidity { get; set; }
    public MaterialType Type { get; set; }
    public string ColorHexCode { get; set; }
}