using TMap.Domain.Entities.Material;

namespace TMap.Domain.DTO.Material;

public record class MaterialDTO(string Name, double Density, double Humidity, MaterialType Type, string ColorHexCode);