namespace TMap.Domain.Entities.Drawing;

public class Polygon
{
    public Polygon(IEnumerable<PixelPoint> points)
    {
        Points = points;
    }

    public IEnumerable<PixelPoint> Points { get; }
    public IEnumerable<Polygon>? SubPolygons { get; }

    public PolygonSize GetSize()
    {
        int minX = 0;
        int minY = 0;
        int maxX = 0;
        int maxY = 0;

        foreach (var point in Points)
        {
            minX = minX > point.X ? point.X : minX;
            minY = minY > point.Y ? point.Y : minY;
            maxX = maxX > point.X ? point.X : maxX;
            maxY = maxY > point.Y ? point.Y : maxY;
        }

        var startPoint = new PixelPoint(minX, minY);
        var width = maxX - minX;
        var height = maxY - minY;

        return new PolygonSize(startPoint, width, height);
    }
}
