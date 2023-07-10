using TMap.Domain.DTO.Material;
using TMap.Domain.Entities.Material;
using TMap.Domain.Abstractions.Repositories;

namespace TMap.Domain.Abstractions.Services.Material;

public interface IMaterialService
{
    /// <summary>
    ///     Get all materials from database by specified material type.
    /// </summary>
    /// <param name="type">The type of materials.</param>
    /// <returns><see cref="MaterialDTO"/> collection.</returns>
    IEnumerable<MaterialDTO> GetMaterialsByType(MaterialType type);

    /// <inheritdoc cref="IMaterialRepository.GetMaterialByName(string)"/>
    MaterialDTO GetMaterialByName(string name);

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
