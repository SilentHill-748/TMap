using TMap.Domain.Exceptions;

namespace TMap.MVVM.Stores;

public class MaterialStore
{
    private readonly IMaterialService _materialService;
    private readonly IMapper _mapper;
    private readonly List<MaterialModel> _materials;

    public MaterialStore(IMaterialService materialService, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(materialService, nameof(materialService));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        _materials = new List<MaterialModel>();
        _materialService = materialService;
        _mapper = mapper;
    }

    public event Action? StoreChanged;

    public IEnumerable<MaterialModel> Materials => _materials;

    public void Load()
    {
        var materials = _materialService.GetMaterials();
        var materialModels = materials.Select(_mapper.Map<MaterialModel>);

        _materials.Clear();
        _materials.AddRange(materialModels);

        StoreChanged?.Invoke();
    }

    public MaterialModel GetMaterial(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));

        return Materials.FirstOrDefault(material => material.Name == name) 
            ?? throw new MaterialNotFoundByNameException(name);
    }

    public IEnumerable<MaterialModel> GetMapMaterials()
        => GetMaterialsByType(MaterialType.Soil | MaterialType.RoadAndSoil);

    public IEnumerable<MaterialModel> GetRoadMaterials()
        => GetMaterialsByType(MaterialType.Soil | MaterialType.RoadAndSoil | MaterialType.Road);

    public IEnumerable<MaterialModel> GetPipeMaterials()
        => GetMaterialsByType(MaterialType.Pipeline);

    public IEnumerable<MaterialModel> GetInsulationMaterials()
        => GetMaterialsByType(MaterialType.Insulation);

    public IEnumerable<MaterialModel> GetChannelInsulationMaterials()
        => GetMaterialsByType(MaterialType.Insulation | MaterialType.ChannelInsulation);

    public async Task CreateMaterialAsync(MaterialModel materialModel)
    {
        var dto = _mapper.Map<MaterialDTO>(materialModel);

        await _materialService.CreateMaterialAsync(dto);

        StoreChanged?.Invoke();
    }

    public async Task UpdateMaterialAsync(MaterialModel materialModel)
    {
        var dto = _mapper.Map<MaterialDTO>(materialModel);

        await _materialService.UpdateMaterialAsync(dto);

        StoreChanged?.Invoke();
    }

    public async Task DeleteMaterialAsync(MaterialModel materialModel)
    {
        var dto = _mapper.Map<MaterialDTO>(materialModel);

        await _materialService.DeleteMaterialAsync(dto);

        StoreChanged?.Invoke();
    }

    private IEnumerable<MaterialModel> GetMaterialsByType(MaterialType types)
    {
        return _materials
            .Where(material => types.HasFlag(material.Type))
            .OrderBy(material => material.Name);
    }
}
