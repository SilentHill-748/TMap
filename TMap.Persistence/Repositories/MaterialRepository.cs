using System.Linq.Expressions;

using AutoMapper;

using Microsoft.EntityFrameworkCore;

using TMap.Domain.Abstractions.Repositories;
using TMap.Domain.DTO.Material;
using TMap.Domain.Entities.Material;
using TMap.Domain.Exceptions;

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

    public IEnumerable<MaterialDTO> GetMaterials(Expression<Func<Material, bool>>? condition = null)
    {
        IQueryable<Material> materials = _materials;

        if (condition is not null)
            materials = materials.Where(condition);

        return materials.Select(_mapper.Map<Material, MaterialDTO>);
    }

    public async Task CreateMaterialAsync(MaterialDTO materialDTO)
    {
        var material = _mapper.Map<MaterialDTO, Material>(materialDTO);

        _ = await _materials.AddAsync(material);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task CreateMaterialsAsync(IEnumerable<MaterialDTO> materialDTOs)
    {
        var materials = materialDTOs.Select(_mapper.Map<MaterialDTO, Material>);

        await _materials.AddRangeAsync(materials);

        _ = _dbContext.SaveChangesAsync();
    }

    public async Task UpdateMaterialAsync(MaterialDTO materialDTO)
    {
        var material = _mapper.Map<MaterialDTO, Material>(materialDTO);

        _ = _materials.Update(material);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteMaterialAsync(MaterialDTO materialDTO)
    {
        var material = _mapper.Map<MaterialDTO, Material>(materialDTO);

        _ = _materials.Remove(material);
        _ = await _dbContext.SaveChangesAsync();
    }

    public async Task<MaterialDTO> GetMaterialByNameAsync(string materialName)
    {
        var material = await _materials.FirstOrDefaultAsync(material => material.Name.Equals(materialName))
            ?? throw new MaterialNotFoundByNameException(materialName);

        return _mapper.Map<Material, MaterialDTO>(material);
    }
}