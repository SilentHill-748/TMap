namespace TMap.Domain.Entities.Drawing;

public readonly struct PolygonSize
{
    public PolygonSize(PixelPoint start, int width, int height)
    {
        this.Width = width;
        this.Height = height;
        this.StartPoint = start;
    }

    public PixelPoint StartPoint { get; }
    public int Width { get; }
    public int Height { get; }
}
