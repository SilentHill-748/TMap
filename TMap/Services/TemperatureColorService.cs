namespace TMap.Services;

public class TemperatureColorService
{
    private const double MinTemperature = -70.00;
    private const double MaxTemperature = 400.00;

    private readonly Dictionary<double, Color> _colorMap;

    public TemperatureColorService(string filename)
    {
        ArgumentException.ThrowIfNullOrEmpty(filename, nameof(filename));

        if (!File.Exists(filename))
            throw new FileNotFoundException(filename);

        _colorMap = CreateColorMap(BitmapFactory.FromStream(File.Open(filename, FileMode.Open)));
    }

    public Color GetColor(double temperature)
    {
        return _colorMap[Math.Round(temperature, 2)];
    }

    private Dictionary<double, Color> CreateColorMap(WriteableBitmap gradient)
    {
        var map = new Dictionary<double, Color>();
        var colors = GetColors(gradient, 47000);

        var step = 0.01;
        var value = MinTemperature;
        var i = 47000 - 1;

        while (value < MaxTemperature)
        {
            map.Add(value, colors[i--]);
            value = Math.Round(value + step, 2);
        }

        while (i > 0)
        {
            map.Add(400.0, colors[i + 1]);
            i--;
        }

        return map;
    }

    private static List<Color> GetColors(WriteableBitmap gradient, int count)
    {
        var step = count / gradient.PixelWidth;
        var colors = new List<Color>();

        for (int i = 0; i < gradient.PixelWidth; i++)
        {
            for (int j = 0; j < step; j++)
            {
                colors.Add(gradient.GetPixel(i, 10));
            }

            if (i == gradient.PixelWidth - 1)
            {
                while (colors.Count != count)
                    colors.Add(gradient.GetPixel(i, 10));
            }
        }

        return colors;
    }
}