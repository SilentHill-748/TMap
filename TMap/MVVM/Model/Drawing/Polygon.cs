namespace TMap.MVVM.Model.Drawing;

public struct Polygon
{
    public Polygon(IList<Point> points)
    {
        Points = points;
        Size = new PolygonSize(this);
    }

    public IList<Point> Points { get; private set; }
    public PolygonSize Size { get; private set; }
    public Color Fill { get; set; }

    public readonly void AddPoint(Point point)
    {
        Points.Add(point);

        Size.MeasureSize(point);
    }

    public readonly int[] ToInt32Vector()
    {
        int[] vector = new int[Points.Count * 2];

        for (int i = 0, j = 0; i < Points.Count; i++)
        {
            vector[j++] = Points[i].X;
            vector[j++] = Points[i].Y;
        }

        return vector;
    }
}
