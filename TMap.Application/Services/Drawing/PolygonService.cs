using TMap.Domain.Abstractions.Services;
using TMap.Domain.Entities.Drawing;

namespace TMap.Application.Services.Drawing;

public class PolygonService : IPolygonService
{
    public Polygon CreateLayerPolygon(int x, int y, int width, int height)
    {
        int right = x + width;
        int bottom = y + height;

        return new Polygon(new PixelPoint[]
        {
            new PixelPoint(x, y),
            new PixelPoint(right, y),
            new PixelPoint(right, bottom),
            new PixelPoint(x, bottom),
            new PixelPoint(x, y)
        });
    }

    public Polygon CreatePipePolygon(int x, int y, int radius)
    {
        // TODO: Реализуй алгоритм рисования круга в виде полигона точек.
        var points = new List<PixelPoint>();

        var x0 = 0;
        var y0 = radius;
        int gap;
        var delta = 2 - 2 * y0;

        while (y0 >= 0)
        {
            points.Add(new PixelPoint(x + x0, y - y0));
            points.Add(new PixelPoint(x - x0, y - y0));
            points.Add(new PixelPoint(x - x0, y + y0));
            points.Add(new PixelPoint(x + x0, y + y0));
                
            gap = 2 * (delta + y0) - 1;

            if (delta < 0 && gap <= 0)
            {
                x0++;
                delta += 2 * x0 + 1;
                continue;
            }

            if (delta > 0 && gap > 0)
            {
                y0--;
                delta -= 2 * y0 + 1;
                continue;
            }

            x0++;
            delta += 2 * (x0 - y0);
            y0--;
        }

        return new Polygon(points);
    }
}
