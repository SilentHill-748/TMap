using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TMap.MVVM.Model.Pipeline;

public class PipelineChannel
{
    public PipelineChannel(Material channelMaterial)
    {
        ArgumentNullException.ThrowIfNull(channelMaterial, nameof(channelMaterial));

        Pipes = new ObservableCollection<Pipe>();
        InsulationLayers = new ObservableCollection<ChannelInsulation>();

        Material = channelMaterial;
    }

    /// <summary>
    ///     Ширина канала в миллиметрах.
    /// </summary>
    public int Width => MeasureWidth();

    public int InsulationThickness => InsulationLayers.Sum(x => x.Thickness);

    /// <summary>
    ///     Высота канала в миллиметрах
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    ///     Толщина стенок канала в миллиметрах.
    /// </summary>
    public int Thickness { get; set; }

    /// <summary>
    ///     Расположение осевой линии труб, отностельно коллектора в миллиметрах.
    /// </summary>
    public int PipesCenterline{ get; set; }

    /// <summary>
    ///     Межосевое расстояние меду трубами в миллиметрах
    /// </summary>
    public int InteraxalWidth { get; set; }

    /// <summary>
    ///     Глубина заложения коллектора в миллиметрах.
    /// </summary>
    public int ChannelDepth { get; set; }

    /// <summary>
    ///     Материал, из которого состоит канал.
    /// </summary>
    public Material Material { get; } // Железобетон. Всегда.

    /// <summary>
    ///     Изоляция коллектора.
    /// </summary>
    public ObservableCollection<ChannelInsulation> InsulationLayers { get; set; }

    /// <summary>
    ///     Трубы, содержащиеся в канале.
    /// </summary>
    public ObservableCollection<Pipe> Pipes { get; set; }

    public void Clear()
    {
        //Width = default;
        Height = default;
        Thickness = default;
        ChannelDepth = default;
        PipesCenterline = default;

        Pipes.Clear();
        InsulationLayers.Clear();
    }

    private int MeasureWidth()
    {
        var pipesWidth = GetPipesRadiusWithInsulationThickness();
        var space = (Pipes.Count - 1) * InteraxalWidth;
        var totalThickness = (Thickness + InsulationThickness) * 2;

        return totalThickness + pipesWidth + space + InteraxalWidth * 2;
    }

    private int PipeWidth(Pipe pipe)
    {
        return (pipe.Radius * 2) + (pipe.InsulationThickness * 2);
    }

    private int GetPipesRadiusWithInsulationThickness()
    {
        int width = 0;

        foreach (Pipe pipe in Pipes)
        {
            width += PipeWidth(pipe);
        }

        return width;
    }
}
