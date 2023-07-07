namespace TMap.Domain.Entities.Material;

public class MaterialColor
{
    public MaterialColor()
    {
        HexCode = "#ffffffff";
    }

    public int ColorId { get; set; }
    public string HexCode { get; set; }
}
