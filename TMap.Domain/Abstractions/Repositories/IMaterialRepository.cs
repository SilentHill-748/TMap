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
    /// <returns>The material collection.</returns>
    IEnumerable<MaterialDTO> GetAllMaterialsByType(MaterialType type);

    /// <summary>
    ///     Create a new material.
    /// </summary>
    /// <param name="material">The material.</param>
    Task CreateMaterial(MaterialDTO materialDTO);

    /// <summary>
    ///     Create collection of materials.
    /// </summary>
    /// <param name="materials">The material collection.</param>
    Task CreateMaterials(IEnumerable<MaterialDTO> materialDTOs);

    /// <summary>
    ///     Delete a chosen material.
    /// </summary>
    /// <param name="material">The material.</param>
    Task DeleteMaterial(MaterialDTO materialDTO);

    /// <summary>
    ///     Update data of a chosen material.
    /// </summary>
    /// <param name="material">The material.</param>
    Task UpdateMaterial(MaterialDTO materialDTO);
}
