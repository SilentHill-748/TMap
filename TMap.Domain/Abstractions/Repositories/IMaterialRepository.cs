using TMap.Domain.DTO.Material;
using TMap.Domain.Entities.Material;

namespace TMap.Domain.Abstractions.Repositories;

/// <summary>
///     Represents a material repository object.
/// </summary>
public interface IMaterialRepository
{
    /// <summary>
    ///     Get all materials by concrete type.
    /// </summary>
    /// <param name="type">Type of material.</param>
    /// <returns>The <see cref="MaterialDTO"/> collection.</returns>
    IEnumerable<MaterialDTO> GetAllMaterialsByType(MaterialType type);

    /// <summary>
    ///     Get finded material by specified name.
    /// </summary>
    /// <param name="materialName">The material name.</param>
    /// <returns>Finded material data as <see cref="MaterialDTO"/>.</returns>
    MaterialDTO GetMaterialByName(string materialName);

    /// <summary>
    ///     Create a new material.
    /// </summary>
    /// <param name="materialDTO">The material data.</param>
    Task CreateMaterialAsync(MaterialDTO materialDTO);

    /// <summary>
    ///     Create collection of materials.
    /// </summary>
    /// <param name="materialDTOs">The <see cref="MaterialDTO"/> collection.</param>
    Task CreateMaterialsAsync(IEnumerable<MaterialDTO> materialDTOs);

    /// <summary>
    ///     Delete a chosen material.
    /// </summary>
    /// <param name="materialDTO">The material data.</param>
    Task DeleteMaterialAsync(MaterialDTO materialDTO);

    /// <summary>
    ///     Update data of a chosen material.
    /// </summary>
    /// <param name="materialDTO">The material data.</param>
    Task UpdateMaterialAsync(MaterialDTO materialDTO);
}
