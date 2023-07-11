namespace TMap.MVVM.Model.Map;

public class Layer
{
    public int Thickness { get; set; }

    public int Width { get; set; }

    public MaterialModel Material { get; set; } = new MaterialModel();
}