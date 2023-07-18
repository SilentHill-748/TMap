using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;

namespace TMap.MVVM.Model.Pipeline;

public class PipelineChannel
{
    public PipelineChannel()
    {
        Pipes = new ObservableCollection<Pipe>();
        InsulationLayers = new ObservableCollection<ChannelInsulation>();

        InsulationLayers.CollectionChanged += InsulationLayers_CollectionChanged;
    }

    public int Width => MeasureWidth();

    public int InsulationThickness { get; private set; }

    public int Height { get; set; }

    public int Thickness { get; set; }

    public int PipesCenterline{ get; set; }

    public int InteraxalWidth { get; set; }

    public int ChannelDepth { get; set; }

    public MaterialModel Material { get; set; } = new MaterialModel();

    public ObservableCollection<ChannelInsulation> InsulationLayers { get; set; }

    public ObservableCollection<Pipe> Pipes { get; set; }

    public void Clear()
    {
        Height = default;
        Thickness = default;
        ChannelDepth = default;
        PipesCenterline = default;

        Pipes.Clear();
        InsulationLayers.Clear();
    }

    private void InsulationLayers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        InsulationThickness = InsulationLayers.Sum(insulation => insulation.Thickness);
    }

    private int MeasureWidth()
    {
        var pipesWidth = GetPipesRadiusWithInsulationThickness();
        var space = (Pipes.Count - 1) * InteraxalWidth;
        var totalThickness = (Thickness + InsulationThickness) * 2;

        return totalThickness + pipesWidth + space + InteraxalWidth * 2;
    }

    private int GetPipesRadiusWithInsulationThickness()
    {
        int width = 0;

        foreach (Pipe pipe in Pipes)
        {
            width += (pipe.Radius + pipe.InsulationThickness) * 2;
        }

        return width;
    }
}
