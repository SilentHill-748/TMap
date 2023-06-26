namespace TMap.Domain.Entities.Drawing;

public struct PixelPoint
{
    public int X { get; set; }
    public int Y { get; set; }

    public PixelPoint(int x, int y)
    {
        X = x;
        Y = y;
    }
}
