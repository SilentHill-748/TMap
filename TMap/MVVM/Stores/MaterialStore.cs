using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TMap.Domain.Entities.Material;
using TMap.MVVM.Facades;

namespace TMap.MVVM.Stores;
public class MaterialStore
{
    private readonly MaterialFacade _materialFacade;

    private ObservableCollection<MaterialModel>? _soilMaterials;
    private ObservableCollection<MaterialModel>? _roadMaterials;
    private ObservableCollection<MaterialModel>? _insulationMaterials;
    private ObservableCollection<MaterialModel>? _channelInsulationMaterials;
    private ObservableCollection<MaterialModel>? _pipeMaterials;

    public MaterialStore(MaterialFacade materialFacade)
    {
        ArgumentNullException.ThrowIfNull(materialFacade, nameof(materialFacade));

        _materialFacade = materialFacade;
    }

    public ObservableCollection<MaterialModel> SoilMaterials
        => _soilMaterials ??= _materialFacade.GetMaterialModels(MaterialType.Soil);

    public ObservableCollection<MaterialModel> RoadMaterials
        => _roadMaterials ??= _materialFacade.GetMaterialModels(MaterialType.Soil | MaterialType.Road | MaterialType.RoadAndSoil);

    public ObservableCollection<MaterialModel> InsulationMaterials
        => _insulationMaterials ??= _materialFacade.GetMaterialModels(MaterialType.Insulation);

    public ObservableCollection<MaterialModel> ChannelInsulationMaterials
        => _channelInsulationMaterials ??= _materialFacade.GetMaterialModels(MaterialType.ChannelInsulation);

    public ObservableCollection<MaterialModel> PipeMaterials
        => _pipeMaterials ??= _materialFacade.GetMaterialModels(MaterialType.Pipeline);


    public MaterialModel GetMaterial(MaterialType type, string name)
        => _materialFacade.GetMaterialModel(type, name);
}
