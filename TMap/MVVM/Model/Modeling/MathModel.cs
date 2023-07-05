using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using MathNet.Numerics.Integration;

using TMap.Services;

namespace TMap.MVVM.Model.Modeling;

/// <summary>
///     Выполняет расчет тепловых характеристик моделируемой области.
/// </summary>
public class MathModel
{
    private readonly Dictionary<Color, Material> _materialMap;
    private readonly SettingsModel _settingsModel;
    private readonly WriteableBitmap _map;
    private readonly Cell[,] _modelCells;
    private readonly Material _defaultMaterial;
    private readonly TemperatureColorService _temperatureService;

    private readonly double _minInterval;
    private readonly double _maxIntervalX;
    private readonly double _maxIntervalY;

    public MathModel(SettingsModel settingsModel, Dictionary<Color, Material> materialMap, WriteableBitmap map)
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

    public event Action? ModelRunning;
    public event Action? ModelRunned;
    public event Action? ModelStepOneSuccessed;
    public event Action? ModelStepTwoSuccessed;
    public event Action? ModelStopped;

    public Cell[,] Cells => _modelCells;

    public void Run()
    {
        StepOne();

        //if (!_settingsModel.PipelineSettings.IsSkiped)
        //{
        //    StepTwo();
        //    ModelStepTwoSuccessed?.Invoke();
        //}

        ModelStopped?.Invoke();
    }
    public WriteableBitmap GetTemperatureMap()
    {
        // Коллекция атласа цветов для температур от -70,00 до +170,00

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
        //var x = rows / 2;
        //var y = columns / 2;

        //bool isLeftDown = false;
        //var counter = 0;
        //var steps = 1;

        //while (counter < rows * columns - 1)
        //{
        //    if (isLeftDown)
        //    {
        //        while (x-- > x - steps && x > 0)
        //            Cells[x, y].Temperature = 170; //CalculateTemperature(x, y);

        //        while (y++ < y + steps && y < columns)
        //            Cells[x, y].Temperature = 170;// CalculateTemperature(x, y);
        //    }
        //    else
        //    {
        //        while (x++ < x + steps && x < rows)
        //            Cells[x, y].Temperature = 170;// CalculateTemperature(x, y);

        //        while (y-- > y - steps && y > 0)
        //            Cells[x, y].Temperature = 170;// CalculateTemperature(x, y);
        //    }

        //    counter += steps * 2;
        //    steps++;
        //    isLeftDown = !isLeftDown;
        //}

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

    // Запуск расчета температур от трубопровода.
    private void StepTwo()
    {

    }

    private double CalculateTemperature(int x, int y)
    {
        var cell = Cells[x, y];
        var lambda = 0.55;
        var channel = _settingsModel.PipelineSettings.Channel;
        var h = channel.PipesCenterline;
        var b = channel.Pipes.Sum(x => x.TotalThichness) + channel.InteraxalWidth;
        var pipeCenterlineMaterial = Cells[_map.PixelWidth / 2, channel.PipesCenterline].Material!;
        var layers = new List<Material>();
        layers.AddRange(_settingsModel.MapSettings.MapSoilLayers.Select(x => x.Material));
        layers.AddRange(_settingsModel.RoadSettings.Layers.Select(x => x.Material));

        //var avgSoilConductivity = layers.Average(x => x.ThermalConductivity);
        var avgSoilTemperature = layers.Average(x => x.InitialTemperature);
        var R0 = 1 / (2 * Math.PI * lambda) * Math.Log(Math.Sqrt(1 + Math.Pow(2 * (h / b), 2)));
        var R1 = GetPipeInsulationR(channel.Pipes[0], pipeCenterlineMaterial) + R0;
        var R2 = GetPipeInsulationR(channel.Pipes[1], pipeCenterlineMaterial) + R0;
        var T0 = pipeCenterlineMaterial.InitialTemperature;
        var T1 = channel.Pipes[0].Temperature;
        var T2 = channel.Pipes[1].Temperature;

        var q1 = ((T1 - T0) * R2 - (T2 - T0) * R0) / R1 * R2 - R0;
        var q2 = ((T2 - T0) * R1 - (T1 - T0) * R0) / R1 * R2 - R0;
       
        var func1 = (Math.Pow(x, 2) + Math.Pow(y + h, 2)) / (Math.Pow(x, 2) + Math.Pow(y - h, 2));
        var func2 = (Math.Pow(x - b, 2) + Math.Pow(y + h, 2)) / (Math.Pow(x - b, 2) + Math.Pow(y - b, 2));
       
        var Ln1 = Math.Log(Math.Sqrt( (Math.Pow(x - b, 2) + Math.Pow(y + h, 2)) / (Math.Pow(x - b, 2) + Math.Pow(y - h, 2)) ));
        var Ln2 = Math.Log(Math.Sqrt( ((x * x) + Math.Pow(y + h, 2)) / ((x * x) + Math.Pow(y - h, 2)) ));

        var Ln = (q1 / lambda * Math.Log(func1)) + (q2 / lambda * Math.Log(func2));
        var r = (cell.Temperature - avgSoilTemperature) / GetR();
        var Hs = (y / (y * lambda)) * r;

        return avgSoilTemperature + (cell.Temperature - avgSoilTemperature) / GetR() * 3.36 + layers.Sum(x => x.LayerThickness / x.ThermalConductivity * r) + (Hs is double.NaN ? 0.0 : Hs);
        //return avgSoilTemperature + (q1 / (2 * Math.PI * lambda)) * Ln1 + q2 / (2 * Math.PI * lambda) * Ln2;
        //var pipes = _settingsModel.PipelineSettings.Channel.Pipes;
        //var target = Cells[x, y];
        //var Tg = target.Material!.InitialTemperature;
        //var Tv = target.Material.InitialTemperature;
        //var pipeSoil = Cells[_map.PixelWidth / 2, _settingsModel.PipelineSettings.Channel.PipesCenterline].Material;
        //var RpipeSoil = pipeSoil!.LayerThickness / pipeSoil.ThermalConductivity;
        //var H = y;
        //var h = _settingsModel.PipelineSettings.Channel.PipesCenterline;
        //var mainLambda = 0.02;
        //var lambdaG = 0.55;
        //var R = GetR();
        //var Lr = (Tg - Tv) / R;
        //double q1, q2;

        //var layers = new List<Material>();
        //layers.AddRange(_settingsModel.MapSettings.MapSoilLayers.Select(x => x.Material));
        //layers.AddRange(_settingsModel.RoadSettings.Layers.Select(x => x.Material));

        //var num = layers.Sum(x => (x.LayerThickness / x.ThermalConductivity) * Lr);

        //double cellTemp = Tv + Lr * (1 / (2 * Math.PI * lambdaG) * Math.Log(Math.Sqrt((x * x + Math.Pow(y + h, 2)) / (x * x + Math.Pow(y - h, 2)))));  /*Tv + () * 3.36 + num;*/

        ////if (H != 0)
        ////{
        ////    cellTemp += (H / (H * mainLambda)) * Lr;
        ////}

        //if (pipes.Count == 2)
        //{
        //    var b = pipes.Sum(x => x.TotalThichness) + _settingsModel.PipelineSettings.Channel.InteraxalWidth;
        //    var R0 = 1 / (2 * Math.PI * lambdaG) * Math.Log(Math.Sqrt(1 + Math.Pow(2 * (pipeSoil.LayerThickness / b), 2)));
        //    var R1 = GetPipeInsulationR(pipes[0], pipeSoil);
        //    var R2 = GetPipeInsulationR(pipes[1], pipeSoil);

        //    q1 = ((pipes[0].Temperature - Cells[x, H].Material!.InitialTemperature) * R2 + (pipes[1].Temperature - Cells[x, H].Material!.InitialTemperature) * R0) / (R1 * R2 - Math.Pow(R0, 2));
        //    q2 = ((pipes[1].Temperature - Cells[x, H].Material!.InitialTemperature) * R1 - (pipes[0].Temperature - Cells[x, H].Material!.InitialTemperature) * R0) / (R1 * R2 - Math.Pow(R0, 2));

        //    var q = Math.Pow(x, 2) + Math.Pow(y + h, 2);
        //    var w = Math.Pow(x, 2) + Math.Pow(y - h, 2);
        //    var z = Math.Pow(x - b, 2) + Math.Pow(y + h, 2);
        //    var c = Math.Pow(x - b, 2) + Math.Pow(y - b, 2);

        //    //return Tv + q1 / (2 * Math.PI * lambdaG) * Math.Log(Math.Sqrt(q / w)) + q2 / (2 * Math.PI * lambdaG) * Math.Log(Math.Sqrt(z / c));
        //}

        //return cellTemp;
        //var airCell = new Cell() { Material = _defaultMaterial, Temperature = _defaultMaterial.InitialTemperature };

        //var cell = Cells[x, y];
        //var left = x == 0 ? airCell : Cells[x - 1, y];
        //var right = x == _map.PixelWidth - 1 ? airCell : Cells[x + 1, y];
        //var up = y == 0 ? airCell : Cells[x, y - 1];
        //var down = y == _map.PixelHeight - 1 ? airCell : Cells[x, y + 1];

        //var Q = CalculateQ;
        //var R = CalculateR;

        //var dx = (double x) => Q(left, cell, R(left)) - Q(cell, right, R(right));
        //var dy = (double x) => Q(up, cell, R(up)) - Q(cell, down, R(down));

        //var Tx = SimpsonRule.IntegrateComposite(dx, _minInterval, _maxIntervalX, 50);
        //var Ty = SimpsonRule.IntegrateComposite(dy, _minInterval, _maxIntervalY, 50);

        //return Tx + Ty;
    }

    private double GetR()
    {
        var Rp = 3.36;

        var mapLayers = _settingsModel.MapSettings.MapSoilLayers;
        var roadLayers = _settingsModel.RoadSettings.Layers;

        var mapLayersR = mapLayers.Sum(x => x.Thickness / x.Material.ThermalConductivity);
        var roadLayersR = roadLayers.Sum(x => x.Thickness / x.Material.ThermalConductivity);

        return Rp + mapLayersR + roadLayersR;
    }

    //private double CalculateQ(Cell cell1, Cell cell2, double R)
    //{
    //    return (cell1.Temperature - cell2.Temperature) / R;
    //}

    //private double CalculateR(Cell cell)
    //{
    //    return cell.Material!.LayerThickness / cell.Material!.ThermalConductivity;
    //}

    private void InitializeModel()
    {
        var width = _map.PixelWidth;
        var height = _map.PixelHeight;

        for (int i = 1; i < width - 1; i++)
            for (int j = 1; j < height - 1; j++)
            {
                var pixelColor = _map.GetPixel(i, j);
                var material = _materialMap[pixelColor];

                var cell = new Cell() { Material = material, Temperature = material.InitialTemperature };

                Cells[i - 1, j - 1] = cell;
            }
    }

    private double GetPipeInsulationR(Pipe pipe, Material material)
    {
        return pipe.Insulation.Sum(x => x.Thickness / x.Material.ThermalConductivity) + (material.LayerThickness / material.ThermalConductivity);
    }
}
