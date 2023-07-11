using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using TMap.WPFCore.Commands.Base;

namespace TMap.WPFCore.Commands.Map;

public class DrawMapCommand : CommandBase
{
    private readonly MapViewModel _viewModel;
    private readonly MaterialModel _defaultMaterial;

    public DrawMapCommand(MapViewModel mapViewModel, MaterialModel defaultMaterial)
    {
        ArgumentNullException.ThrowIfNull(mapViewModel, nameof(mapViewModel));
        ArgumentNullException.ThrowIfNull(defaultMaterial, nameof(defaultMaterial));

        _viewModel = mapViewModel;
        _defaultMaterial = defaultMaterial;
    }

    protected override void Execute()
    {
        var width = _viewModel.Settings.MapSettings.MapWidth;
        var height = _viewModel.Settings.MapSettings.MapHeight + _viewModel.Settings.RoadSettings.MoundHeight;

        _viewModel.MapBitmap = new WriteableBitmap(width + 2, height + 2, 96, 96, PixelFormats.Bgra32, null);
        _viewModel.MapBitmap.Clear(Colors.White);

        var drawingService = new DrawingService(_viewModel.Settings, _viewModel.MapBitmap, _defaultMaterial);

        drawingService.DrawMainMap();
        drawingService.DrawRoadMap();
        drawingService.DrawPipelineMap();
    }

    public override bool CanExecute()
    {
        return _viewModel.Settings.IsCompleted;
    }


    // To delete \\

    //private void FillTextureTest(int width, int height, int step, string texturePath)
    //{
    //    var polygon = GetPolygon(new System.Windows.Point(0, height), width, step);

    //    var texture = new WriteableBitmap(new BitmapImage(new Uri(texturePath)));

    //    FillTexture(polygon, _viewModel.MapBitmap, texture);
    //}

    // Можно вынести в сервис работы с полигонами.
    //private System.Windows.Point[] GetPolygon(System.Windows.Point startPoint, int width, int height)
    //{
    //    int pointCounter = 4;

    //    System.Windows.Point[] points = new System.Windows.Point[4 + width];
    //    points[0] = startPoint;
    //    points[1] = new System.Windows.Point(startPoint.X, startPoint.Y - height);
    //    points[2] = new System.Windows.Point(startPoint.X + width, startPoint.Y - height);
    //    points[3] = new System.Windows.Point(startPoint.X + width, startPoint.Y);

    //    for (int i = width - 1; i >= 0; i--)
    //    {
    //        points[pointCounter] = new System.Windows.Point(i, startPoint.Y);
    //        pointCounter++;
    //    }

    //    return points;
    //}

    //private void FillTexture(System.Windows.Point[] polygon, WriteableBitmap result, WriteableBitmap texture)
    //{
    //    var polygonSize = GetPolygonSize(polygon);

    //    for (int i = (int)polygonSize.X; i < polygonSize.Width; i++)
    //    {
    //        if (i > result.PixelWidth) break;

    //        for (int j = (int)polygonSize.Y; j < polygonSize.Height; j++)
    //        {
    //            if (j > result.PixelHeight) break;

    //            if (PointInPolygon(polygon, new System.Windows.Point(i, j)))
    //            {
    //                Color pixelColor = texture.GetPixel(i, j);

    //                result.SetPixel(i, j, pixelColor);
    //            }
    //        }
    //    }
    //}

    // Можно вынести в сервис работы с полигонами.
    //private bool PointInPolygon(System.Windows.Point[] polygon, System.Windows.Point point)
    //{
    //    int intersections = 0;

    //    // Проходимся по всем ребрам полигона
    //    for (int i = 0; i < polygon.Length; i++)
    //    {
    //        System.Windows.Point p1 = polygon[i];
    //        System.Windows.Point p2 = polygon[(i + 1) % polygon.Length];

    //        // Проверяем, пересекается ли ребро с лучом, и увеличиваем количество пересечений при необходимости
    //        if ((p1.Y > point.Y) != (p2.Y > point.Y) && point.X < (p2.X - p1.X) * (point.Y - p1.Y) / (p2.Y - p1.Y) + p1.X)
    //        {
    //            intersections++;
    //        }
    //    }

    //    // Если количество пересечений нечетное, точка находится внутри полигона
    //    return intersections % 2 == 1;
    //}

    // Можно вынести в сервис работы с полигонами.
    // Вернет размеры границ полигона
    //private Rect GetPolygonSize(System.Windows.Point[] polygon)
    //{
    //    var width = (int)polygon.Max(x => x.X);
    //    var height = (int)polygon.Max(x => x.Y);
    //    var minX = (int)polygon.Min(x => x.X);
    //    var minY = (int)polygon.Min(x => x.Y);

    //    return new Rect(minX, minY, width, height);
    //}
}
