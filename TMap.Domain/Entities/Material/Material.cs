using TMap.Domain.DTO.Material;

namespace TMap.Domain.Entities.Material;

public class Material
{
    public Material()
    {
        Name = string.Empty;
        ColorHexCode = "#ffffffff";
    }

    public Material(MaterialDTO materialDTO)
    {
        MaterialId = materialDTO.MaterialId;
        Name = materialDTO.Name;
        Density = materialDTO.Density;
        Humidity = materialDTO.Humidity;
        Type = materialDTO.Type;
        ColorHexCode = materialDTO.ColorHexCode;
    }

    public int MaterialId { get; set; }
    public string Name { get; set; }
    public double Density { get; set; }
    public double Humidity { get; set; }
    public MaterialType Type { get; set; }
    public string ColorHexCode { get; set; }

    public MaterialDTO MapToDTO()
    {
        return new MaterialDTO(MaterialId, Name, Density, Humidity, Type, ColorHexCode);
    }
}
