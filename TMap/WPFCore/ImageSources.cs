using System.IO;

namespace TMap.WPFCore
{
    /// <summary>
    ///     Texture path constants
    /// </summary>
    public static class ImageSources
    {
        private readonly static string TextureFolder =
            Path.Combine(Directory.GetCurrentDirectory(), "Resources", "Assets", "Blocks");

        public readonly static string Sand = Path.Combine(TextureFolder, "sand.png");
        public readonly static string Loam = Path.Combine(TextureFolder, "loam.png");
        public readonly static string Grass = Path.Combine(TextureFolder, "grass2.png");
        public readonly static string Empty = Path.Combine(TextureFolder, "air.png");

        public readonly static string TestTexture = Path.Combine(TextureFolder, "test.jpg");
    }
}
