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
    void CreateMaterial(MaterialDTO material);

    /// <summary>
    ///     Create collection of materials.
    /// </summary>
    /// <param name="materials">The material collection.</param>
    void CreateMaterials(IEnumerable<MaterialDTO> materials);

    /// <summary>
    ///     Delete a chosen material.
    /// </summary>
    /// <param name="material">The material.</param>
    void DeleteMaterial(MaterialDTO material);

    /// <summary>
    ///     Update data of a chosen material.
    /// </summary>
    /// <param name="material">The material.</param>
    void UpdateMaterial(MaterialDTO material);
}
