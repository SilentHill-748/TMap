using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TMap.Helpers;

public class MaterialHelper
{
    private readonly string _configurationPath
        = Path.Combine(Directory.GetCurrentDirectory(), "TMap", "Configurations");

    public MaterialHelper()
    {
        DefaultMaterial = GetMaterials("Materials.json").First(x => x.Name == "Воздух");

        DefaultMaterial.LayerThickness = 1;
    }

    public Material DefaultMaterial { get; }

    public ObservableCollection<Material> GetSoilMaterials()
    {
        var materials = GetMaterials("Materials.json").Where(x => x.Name != "Воздух");

        return new ObservableCollection<Material>(materials);
    }

    public ObservableCollection<Material> GetChannelInsulationMaterials()
    {
        return GetMaterials("PipelineInsulationChannelMaterials.json");
    }

    public ObservableCollection<Material> GetPipeInsulationMaterials()
    {
        return GetMaterials("PipeInsulationMaterials.json");
    }

    public ObservableCollection<Material> GetPipeMaterials()
    {
        return GetMaterials("PipeMaterials.json");
    }

    private ObservableCollection<Material> GetMaterials(string jsonFilename)
    {
        string filepath = Path.Combine(_configurationPath, jsonFilename);

        // TODO: Реализуй свой эксепшн, если класс MaterialHelper останется.
        var materials = JsonSerializer.Deserialize<Material[]>(File.ReadAllText(filepath)) ??
            throw new System.Exception("Materials doesn't got!");

        return new ObservableCollection<Material>(materials.OrderBy(material => material.Name));
    }
}
