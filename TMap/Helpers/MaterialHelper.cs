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

    public MaterialModel DefaultMaterial { get; }

    public ObservableCollection<MaterialModel> GetSoilMaterials()
    {
        var materials = GetMaterials("Materials.json").Where(x => x.Name != "Воздух");

        return new ObservableCollection<MaterialModel>(materials);
    }

    public ObservableCollection<MaterialModel> GetChannelInsulationMaterials()
    {
        return GetMaterials("PipelineInsulationChannelMaterials.json");
    }

    public ObservableCollection<MaterialModel> GetPipeInsulationMaterials()
    {
        return GetMaterials("PipeInsulationMaterials.json");
    }

    public ObservableCollection<MaterialModel> GetPipeMaterials()
    {
        return GetMaterials("PipeMaterials.json");
    }

    private ObservableCollection<MaterialModel> GetMaterials(string jsonFilename)
    {
        string filepath = Path.Combine(_configurationPath, jsonFilename);

        // TODO: Реализуй свой эксепшн, если класс MaterialHelper останется.
        var materials = JsonSerializer.Deserialize<MaterialModel[]>(File.ReadAllText(filepath)) ??
            throw new System.Exception("Materials doesn't got!");

        return new ObservableCollection<MaterialModel>(materials.OrderBy(material => material.Name));
    }
}
