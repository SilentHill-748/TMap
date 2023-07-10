using TMap.Domain.Abstractions.Repositories;
using TMap.Domain.Abstractions.Services.Material;
using TMap.Domain.DTO.Material;
using TMap.Domain.Entities.Material;

namespace TMap.Application.Services.Material;

public class MaterialService : IMaterialService
{
    private readonly IMaterialRepository _materialRepository;

    public MaterialService(IMaterialRepository materialRepository)
    {
        ArgumentNullException.ThrowIfNull(materialRepository, nameof(materialRepository));

        _materialRepository = materialRepository;
    }

    public MaterialDTO GetMaterialByName(string name)
    {
        return _materialRepository.GetMaterialByName(name);
    }

    public IEnumerable<MaterialDTO> GetSoilMaterials()
    {
        return _materialRepository.GetAllMaterialsByType(MaterialType.Soil);
    }

    public IEnumerable<MaterialDTO> GetRoadMaterials()
    {
        var soilMaterials = _materialRepository.GetAllMaterialsByType(MaterialType.Soil);
        var roadMaterials = _materialRepository.GetAllMaterialsByType(MaterialType.Road);
        var soilAndRoadMaterials = _materialRepository.GetAllMaterialsByType(MaterialType.RoadAndSoil);

        return soilMaterials
            .Union(roadMaterials)
            .Union(soilAndRoadMaterials)
            .OrderBy(material => material.Name);
    }

    public IEnumerable<MaterialDTO> GetPipelineChannelInsulationMaterials()
    {
        return _materialRepository.GetAllMaterialsByType(MaterialType.ChannelInsulation);
    }

    public IEnumerable<MaterialDTO> GetPipelineInsulationMaterials()
    {
        return _materialRepository.GetAllMaterialsByType(MaterialType.Insulation);
    }

    public IEnumerable<MaterialDTO> GetPipeMaterials()
    {
        return _materialRepository.GetAllMaterialsByType(MaterialType.Pipeline);
    }

    public async Task CreateMaterialAsync(MaterialDTO materialDTO)
    {
        await _materialRepository.CreateMaterialAsync(materialDTO);
    }

    public async Task DeleteMaterialAsync(MaterialDTO materialDTO)
    {
        await _materialRepository.DeleteMaterialAsync(materialDTO);
    }

    public async Task UpdateMaterialAsync(MaterialDTO materialDTO)
    {
        await _materialRepository.UpdateMaterialAsync(materialDTO);
    }
}
