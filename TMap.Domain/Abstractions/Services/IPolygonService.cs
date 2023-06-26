using TMap.Domain.Entities.Drawing;

namespace TMap.Domain.Abstractions.Services;

public interface IPolygonService
{
    /// <summary>
    ///     Create a rectangle poligon.
    /// </summary>
    /// <param name="x">The X xoordinate of start point.</param>
    /// <param name="y">The Y xoordinate of start point.</param>
    /// <param name="width">The width of rectangle.</param>
    /// <param name="height">The height of rectangle.</param>
    /// <returns>A new <see cref="Polygon"/> object.</returns>
    Polygon CreateLayerPolygon(int x, int y, int width, int height);

    /// <summary>
    ///     Create a cicle polygon.
    /// </summary>
    /// <param name="x">Center X coordinate.</param>
    /// <param name="y">Center Y coordinate.</param>
    /// <param name="radius">The radius of cicle.</param>
    /// <returns>A new <see cref="Polygon"/> object.</returns>
    Polygon CreatePipePolygon(int x, int y, int radius);
}
