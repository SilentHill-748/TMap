using System.Collections.ObjectModel;
using System.Linq;

namespace TMap.MVVM.Model.Pipeline;

public class Pipe
{
    public Pipe()
    {
        Insulation = new ObservableCollection<RadialInsulation>();

        Insulation.CollectionChanged += Insulation_CollectionChanged;
    }

    public int Radius { get; set; }

    public int InsulationThickness { get; private set; }

    public int TotalThichness { get; private set; }

    public int Thickness { get; set; }

    public MaterialModel Material { get; set; } = new MaterialModel();

    public double Temperature { get; set; }

    public ObservableCollection<RadialInsulation> Insulation { get; set; }

    private void Insulation_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        InsulationThickness = Insulation.Sum(insulation => insulation.Thickness);
        TotalThichness = Radius + InsulationThickness;
    }
}
