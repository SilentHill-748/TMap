using Microsoft.EntityFrameworkCore;

using TMap.Domain.Abstractions.Repositories;
using TMap.Domain.DTO.Material;
using TMap.Domain.Entities.Material;

namespace TMap.Persistence.Repositories;

public class MaterialRepository : IMaterialRepository
{
    private readonly DbSet<Material> _materials;
    private readonly TMapDbContext _dbContext;

    public MaterialRepository(TMapDbContext dbContext)
    {
        _dbContext = dbContext;
        _materials = dbContext.Materials;
    }

    public IEnumerable<MaterialDTO> GetAllMaterialsByType(MaterialType type)
    {
        var materials = _materials.Where(material => material.Type.Equals(type));

        return materials.Select(material => 
            new MaterialDTO(
                material.MaterialId,
                material.Name,
                material.Density, 
                material.Humidity,
                material.Type, 
                material.ColorHexCode));
    }

    public async Task CreateMaterial(MaterialDTO materialDTO)
    {
        var material = await CreateNonContainedMaterialAsync(materialDTO);

        _ = await _materials.AddAsync(material);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task CreateMaterials(IEnumerable<MaterialDTO> materialDTOs)
    {
        foreach (MaterialDTO dto in materialDTOs)
        {
            _ = await _materials.AddAsync(await CreateNonContainedMaterialAsync(dto));
        }

        _ = _dbContext.SaveChangesAsync();
    }

    public async Task UpdateMaterial(MaterialDTO materialDTO)
    {
        var material = await GetMaterialByDTOAsync(materialDTO);

        _ = _materials.Update(material);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMaterial(MaterialDTO materialDTO)
    {
        var material = await GetMaterialByDTOAsync(materialDTO);

        _ = _materials.Remove(material);
        _ = await _dbContext.SaveChangesAsync();
    }

    private async Task<Material> GetMaterialByDTOAsync(MaterialDTO materialDTO)
    {
        Material? material = await _materials.FirstOrDefaultAsync(material => material.MaterialId == materialDTO.MaterialId);

        //TODO: Написать свои классы Exception.
        if (material is null)
            throw new Exception("Не удалось найти материал в базе данных!");

        return material;
    }

    private async Task<Material> CreateNonContainedMaterialAsync(MaterialDTO dto)
    {
        Material? containedMaterial = await _materials.FindAsync(dto.MaterialId);

        if (containedMaterial is not null)
            throw new Exception("Такой материал уже есть в базе данных!");

        return new Material(dto);
    }
}