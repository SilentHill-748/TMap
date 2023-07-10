using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TMap.Services.Drawing;

public class DrawingService
{
    private readonly SettingsModel _settings;
    private readonly WriteableBitmap _map;
    private readonly MaterialModel _defaultMaterial;

    public DrawingService(SettingsModel settingsModel, WriteableBitmap writeableBitmap, MaterialModel defaultMaterial)
    {
        ArgumentNullException.ThrowIfNull(settingsModel, nameof(settingsModel));
        ArgumentNullException.ThrowIfNull(writeableBitmap, nameof(writeableBitmap));
        ArgumentNullException.ThrowIfNull(defaultMaterial, nameof(defaultMaterial));

        _settings = settingsModel;
        _map = writeableBitmap;
        _defaultMaterial = defaultMaterial;
    }

    public int MapCenterLine => _map.PixelWidth / 2;

    public void DrawMainMap()
    {
        var mapSettings = _settings.MapSettings;
        var layers = mapSettings.MapSoilLayers;
        var start = new Point(0, _settings.RoadSettings.MoundHeight + layers[0].Thickness);
        var polygons = new List<Polygon>()
        {
            CreateMainMapHeadPolygon(layers[0].Material)
        };

        for (int i = 1; i < layers.Count; i++)
        {
            polygons.Add(CreateRectPolygon(start, _map.PixelWidth, layers[i].Thickness, layers[i].Material));
            start.Y += layers[i].Thickness;
            start.X = 0;
        }

        DrawPolygons(polygons);
    }

    public void DrawRoadMap()
    {
        var polygons = new List<Polygon>();
        var roadSettings = _settings.RoadSettings;
        var roadLayers = roadSettings.Layers;
        var startPoint = new Point();

        foreach (RoadLayer layer in roadLayers)
        {
            startPoint.X = (MapCenterLine - layer.Width / 2);
            polygons.Add(CreateRectPolygon(startPoint, layer.Width, layer.Thickness, layer.Material));
            startPoint.Y += layer.Thickness;
        }

        DrawPolygons(polygons);
    }

    public void DrawPipelineMap()
    {
        if (_settings.PipelineSettings.IsSkiped)
            return;

        DrawPipelineChannel();
        DrawPipes();
    }

    private void DrawPipelineChannel()
    {
        var polygons = new List<Polygon>();
        var channel = _settings.PipelineSettings.Channel;
        var width = _settings.PipelineSettings.Channel.Width;
        var height = _settings.PipelineSettings.Channel.Height;
        var insulations = _settings.PipelineSettings.Channel.InsulationLayers;

        var mapCenter = _settings.MapSettings.MapWidth / 2;
        var startPoint = new Point(mapCenter - (channel.Width / 2), channel.ChannelDepth);

        polygons.Add(CreateRectPolygon(startPoint, width, height, channel.Material));

        startPoint.X += channel.Thickness;
        startPoint.Y += channel.Thickness;
        width -= 2 * channel.Thickness;
        height -= 2 * channel.Thickness;

        foreach (ChannelInsulation insulation in insulations)
        {
            insulation.Material.ColorHexCode = "#7d818a";
            polygons.Add(CreateRectPolygon(startPoint, width, height, insulation.Material));

            var thickness = insulation.Thickness;
            startPoint.X += thickness;
            startPoint.Y += thickness;

            width -= 2 * thickness;
            height -= 2 * thickness;
        }

        polygons.Add(CreateRectPolygon(startPoint, width, height, _defaultMaterial));

        DrawPolygons(polygons);
    }

    private void DrawPipes()
    {
        var channel = _settings.PipelineSettings.Channel;
        var pipes = channel.Pipes.Sum(x => x.Radius + x.InsulationThickness) * 2;
        var pipesWidth = pipes + ((channel.Pipes.Count - 1) * channel.InteraxalWidth);

        var center = new Point(MapCenterLine - pipesWidth / 2, channel.PipesCenterline);

        foreach (Pipe pipe in channel.Pipes)
        {
            var pipeThickness = pipe.Radius + pipe.InsulationThickness;

            center.X += pipeThickness;
            DrawPipe(center, pipe);
            center.X += pipeThickness + channel.InteraxalWidth;
        }
    }

    private void DrawPipe(Point center, Pipe pipe)
    {
        var radius = pipe.Radius + pipe.InsulationThickness;

        foreach (RadialInsulation insulation in pipe.Insulation)
        {
            insulation.Material.ColorHexCode = "#7d818a";
            DrawCircle(center, radius, insulation.Material.GetColor());
            radius -= insulation.Thickness;
        }

        pipe.Material.ColorHexCode = "#adadad";
        DrawCircle(center, radius, pipe.Material.GetColor());
    }

    private void DrawCircle(Point center, int radius, Color fill)
    {
        _map.FillEllipseCentered(center.X, center.Y, radius, radius, fill);
        //_map.DrawEllipseCentered(center.X, center.Y, radius, radius, Colors.Black);
    }

    private Polygon CreateRectPolygon(Point start, int width, int height, MaterialModel material)
    {
        var points = new List<Point>() { start };

        points.Add(new Point(start.X + width, points[^1].Y));
        points.Add(new Point(points[^1].X, points[^1].Y + height));
        points.Add(new Point(start.X, points[^1].Y));
        points.Add(start);

        var polygon = new Polygon(points) { Fill = material.GetColor() };

        return polygon;
    }

    private Polygon CreateMainMapHeadPolygon(MaterialModel material)
    {
        var points = new List<Point>();
        var roadSettings = _settings.RoadSettings;
        var last = new Index(1, fromEnd: true);

        var roadWidth = (roadSettings.RoadWidth + 2 * roadSettings.RoadsideWidth);

        points.Add(new Point(0, roadSettings.MoundHeight));
        points.Add(new Point(roadSettings.EdgeWidth, points[last].Y));
        points.Add(new Point(points[last].X + roadSettings.MoundWidth, points[last].Y - roadSettings.MoundHeight));
        points.Add(new Point(points[last].X + roadWidth, points[last].Y));
        points.Add(new Point(points[last].X + roadSettings.MoundWidth, points[last].Y + roadSettings.MoundHeight));
        points.Add(new Point(roadSettings.TotalRoadWidth, points[last].Y));
        points.Add(new Point(roadSettings.TotalRoadWidth, points[last].Y + _settings.MapSettings.MapSoilLayers[0].Thickness));
        points.Add(new Point(0, points[last].Y));
        points.Add(points[0]);

        return new Polygon(points) { Fill = material.GetColor() };
    }

    private void DrawPolygons(IList<Polygon> polygons)
    {
        for (int i = 0; i < polygons.Count; i++)
        {
            DrawPolygon(polygons[i]);
        }
    }

    private void DrawPolygon(Polygon polygon)
    {
        if (_map.PixelHeight >= polygon.Size.Height &&
            _map.PixelWidth >= polygon.Size.Width)
        {
            var vector = polygon.ToInt32Vector();

            _map.FillPolygon(vector, polygon.Fill);
            //_map.DrawPolyline(vector, color);
        }
    }
}