using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace TMap.MVVM.Model.Pipeline;

public class Pipe
{
    public Pipe(MaterialModel pipeMaterial)
    {
        ArgumentNullException.ThrowIfNull(pipeMaterial, nameof(pipeMaterial));

        Insulation = new ObservableCollection<RadialInsulation>();

        Material = pipeMaterial;
    }

    /// <summary>
    ///     Радиус трубы.
    /// </summary>
    public int Radius { get; set; }

    public int InsulationThickness => Insulation.Sum(x => x.Thickness);
    public int TotalThichness => Radius + InsulationThickness;

    /// <summary>
    ///     Толщина трубы.
    /// </summary>
    public int Thickness { get; set; }

    // TODO: Нет материала для трубы.
    /// <summary>
    ///     Материал трубы
    /// </summary>
    public MaterialModel Material { get; set; }

    /// <summary>
    ///     Температура жидкости. Константа.
    /// </summary>
    public double Temperature { get; set; }

    /// <summary>
    ///     Изоляционное покрытие трубы.
    /// </summary>
    public ObservableCollection<RadialInsulation> Insulation { get; set; }
}
