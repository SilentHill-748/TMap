namespace TMap.MVVM.Model.Drawing;

public struct PolygonSize
{
    private Point _min;
    private Point _max;
    private int _width;
    private int _height;

    public PolygonSize(Polygon polygon)
    {
        var minX = polygon.Points.Min(point => point.X);
        var maxX = polygon.Points.Max(point => point.X);
        var minY = polygon.Points.Min(point => point.Y);
        var maxY = polygon.Points.Max(point => point.Y);

        _min = new Point(minX, minY);
        _max = new Point(maxX, maxY);

        _width = maxX - minX;
        _height = maxY - minY;
    }

    public readonly Point Min => _min;
    public readonly Point Max => _max;
    public readonly int Width => _width;
    public readonly int Height => _height;

    public void MeasureSize(Point newPoint)
    {
        if (_min < newPoint)
            _min = newPoint;
        
        if (_max > newPoint)
            _max = newPoint;

        _width = _max.X - _min.X;
        _height = _max.Y - _min.Y;
    }
}
