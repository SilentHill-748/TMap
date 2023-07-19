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

    public IEnumerable<MaterialDTO> GetMaterials()
    {
        return _materialRepository.GetMaterials();
    }

    public IEnumerable<MaterialDTO> GetMaterialsByType(MaterialType types)
    {
        return _materialRepository.GetMaterials(material => material.Type.HasFlag(types));
    }

    public async Task<MaterialDTO> GetMaterialByNameAsync(string name)
    {
        return await _materialRepository.GetMaterialByNameAsync(name);
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
