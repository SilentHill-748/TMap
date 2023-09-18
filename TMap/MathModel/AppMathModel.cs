namespace TMap.MathModel;

/// <summary>
///     Выполняет расчет тепловых характеристик моделируемой области.
/// </summary>
public class AppMathModel
{
    private readonly Dictionary<Color, MaterialModel> _materialMap;
    private readonly SettingsModel _settingsModel;
    private readonly WriteableBitmap _map;
    private readonly Cell[,] _modelCells;
    private readonly MaterialModel _defaultMaterial;
    private readonly TemperatureColorService _temperatureService;

    private readonly double _minInterval;
    private readonly double _maxIntervalX;
    private readonly double _maxIntervalY;

    public AppMathModel(SettingsModel settingsModel, Dictionary<Color, MaterialModel> materialMap, WriteableBitmap map)
    {
        ArgumentNullException.ThrowIfNull(settingsModel, nameof(settingsModel));
        ArgumentNullException.ThrowIfNull(materialMap, nameof(materialMap));
        ArgumentNullException.ThrowIfNull(map, nameof(map));

        _temperatureService = new TemperatureColorService("Resources/Assets/TemperatureGradient.png");
        _settingsModel = settingsModel;
        _materialMap = materialMap;
        _map = map;
        _modelCells = new Cell[_map.PixelWidth - 2, _map.PixelHeight - 2];
        _minInterval = 0;
        _maxIntervalX = _map.PixelWidth;
        _maxIntervalY = _map.PixelHeight;
        _defaultMaterial = materialMap[Colors.White];

        InitializeModel();
    }

    public event Action? ModelStopped;

    public Cell[,] Cells => _modelCells;

    // TODO: Delete stub.
    public void Run()
    {
        StepOne();

        ModelStopped?.Invoke();
    }
    public WriteableBitmap GetTemperatureMap()
    {
        var temperatureImage = new WriteableBitmap(_map.PixelWidth, _map.PixelHeight, _map.DpiX, _map.DpiY, _map.Format, null);

        temperatureImage.Clear(Colors.White);

        var width = Cells.GetLength(0);
        var height = Cells.GetLength(1);

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var temperature = Cells[i, j].Temperature;

                if (temperature > 170.0) temperature = 170.0;

                if (temperature < -70.0) temperature = -70.0;

                if (temperature is double.NaN) continue;

                temperatureImage.SetPixel(i, j, _temperatureService.GetColor(temperature));
            }
        }

        return temperatureImage;
    }

    // Запуск расчета от окружающей среды.
    private void StepOne()
    {
        var rows = Cells.GetLength(0);
        var columns = Cells.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Cells[i, j].Temperature = CalculateTemperature(i, j);
            }
        }

        //for (int j = 0; j < columns; j++)
        //{
        //    Cells[center, j].Temperature = CalculateTemperature(center, j);

        //    while (right < rows || left > 0)
        //    {
        //        if (right != rows)
        //            Cells[right, j].Temperature = CalculateTemperature(right++, j);

        //        if (left != 0)
        //            Cells[left, j].Temperature = CalculateTemperature(left--, j);
        //    }

        //    left = center - 1;
        //    right = center + 1;
        //}
    }

    private double CalculateTemperature(int x, int y)
    {
        return 0;
    }

    private void InitializeModel()
    {
        var width = _map.PixelWidth;
        var height = _map.PixelHeight;

        for (int i = 1; i < width - 1; i++)
            for (int j = 1; j < height - 1; j++)
            {
                var pixelColor = _map.GetPixel(i, j);
                var material = _materialMap[pixelColor];

                // TODO: Delete stub temperature.
                var cell = new Cell() { Material = material, Temperature = 0 };

                Cells[i - 1, j - 1] = cell;
            }
    }
}
