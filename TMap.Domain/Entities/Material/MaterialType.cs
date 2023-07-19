namespace TMap.Domain.Entities.Material;

[Flags]
public enum MaterialType
{
    Environment = 1,
    Soil = 2,
    Road = 4,
    RoadAndSoil = 8,
    Channel = 16,
    ChannelInsulation = 32,
    Insulation = 64,
    Pipeline = 128
}