namespace TMap.MVVM.Model;

public class ImageModel
{
    public int PixelWidth { get; set; }
    public int PixelHeight { get; set; }
    public int Dpi { get; set; } = 96;

    public WriteableBitmap CreateWriteableBitmap()
    {
        var bmp = new WriteableBitmap(PixelWidth + 1, PixelHeight + 1, Dpi, Dpi, PixelFormats.Bgra32, null);

        return bmp;
    }
}
