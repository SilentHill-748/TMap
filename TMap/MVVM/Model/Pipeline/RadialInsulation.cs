namespace TMap.MVVM.Model.Pipeline;

public class RadialInsulation
{
    public int Thickness { get; set; }

    public double InitialTemperature { get; set; }

    public MaterialModel Material { get; set; } = new MaterialModel();
}
