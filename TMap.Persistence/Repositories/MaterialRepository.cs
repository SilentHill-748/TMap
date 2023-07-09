using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TMap.Domain.Abstractions.Repositories;
using TMap.Domain.DTO.Material;
using TMap.Domain.Entities.Material;

namespace TMap.Persistence.Repositories;

public class MaterialRepository : IMaterialRepository
{
    private readonly DbSet<Material> _materials;
    private readonly TMapDbContext _dbContext;
    private readonly IMapper _mapper;

    public MaterialRepository(TMapDbContext dbContext, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        _dbContext = dbContext;
        _materials = dbContext.Materials;
        _mapper = mapper;
    }

    public IEnumerable<MaterialDTO> GetAllMaterialsByType(MaterialType type)
    {
        var materials = _materials.Where(material => material.Type.Equals(type));

        return materials.Select(material => _mapper.Map<Material, MaterialDTO>(material));
    }

    public async Task CreateMaterial(MaterialDTO materialDTO)
    {
        var material = _mapper.Map<MaterialDTO, Material>(materialDTO);

        _ = await _materials.AddAsync(material);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task CreateMaterials(IEnumerable<MaterialDTO> materialDTOs)
    {
        var materials = materialDTOs.Select(_mapper.Map<MaterialDTO, Material>);

        await _materials.AddRangeAsync(materials);

        _ = _dbContext.SaveChangesAsync();
    }

    public async Task UpdateMaterial(MaterialDTO materialDTO)
    {
        var material = _mapper.Map<MaterialDTO, Material>(materialDTO);

        _ = _materials.Update(material);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMaterial(MaterialDTO materialDTO)
    {
        var material = _mapper.Map<MaterialDTO, Material>(materialDTO);

        _ = _materials.Remove(material);
        _ = await _dbContext.SaveChangesAsync();
    }
}