using TMap.Domain.DTO.Material;
using TMap.Domain.Abstractions.Repositories;
using TMap.Domain.Entities.Material;

namespace TMap.Domain.Abstractions.Services.Material;

public interface IMaterialService
{
    /// <summary>
    ///     Get all materials.
    /// </summary>
    /// <returns>Collection of <see cref="MaterialDTO"/>.</returns>
    IEnumerable<MaterialDTO> GetMaterials();

    /// <inheritdoc cref="IMaterialRepository.GetMaterialByNameAsync(string)"/>
    Task<MaterialDTO> GetMaterialByNameAsync(string name);

    /// <summary>
    ///     Get materials by specified type.
    /// </summary>
    /// <param name="types">Material types.</param>
    /// <returns>The collection of <see cref="MaterialDTO"/>.</returns>
    IEnumerable<MaterialDTO> GetMaterialsByType(MaterialType types);

    /// <summary>
    ///     Add new material to database.
    /// </summary>
    /// <param name="materialDTO">The material data.</param>
    /// <returns><see cref="Task"/></returns>
    Task CreateMaterialAsync(MaterialDTO materialDTO);

    /// <summary>
    ///     Delete material from database.
    /// </summary>
    /// <param name="materialDTO">The material data.</param>
    /// <returns><see cref="Task"/></returns>
    Task DeleteMaterialAsync(MaterialDTO materialDTO);

    /// <summary>
    ///     Edit material data.
    /// </summary>
    /// <param name="materialDTO">The material data.</param>
    /// <returns><see cref="Task"/></returns>
    Task UpdateMaterialAsync(MaterialDTO materialDTO);
}
