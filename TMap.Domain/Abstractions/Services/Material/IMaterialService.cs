using TMap.Domain.DTO.Material;
using TMap.Domain.Abstractions.Repositories;

namespace TMap.Domain.Abstractions.Services.Material;

public interface IMaterialService
{
    /// <inheritdoc cref="IMaterialRepository.GetMaterialByName(string)"/>
    MaterialDTO GetMaterialByName(string name);

    /// <summary>
    ///     Get collection of soil materials for geological map.
    /// </summary>
    /// <returns>The collection of <see cref="MaterialDTO"/>.</returns>
    IEnumerable<MaterialDTO> GetSoilMaterials();

    /// <summary>
    ///     Get collection of soil and road materials for geological road map.
    /// </summary>
    /// <returns>The collection of <see cref="MaterialDTO"/>.</returns>
    IEnumerable<MaterialDTO> GetRoadMaterials();

    /// <summary>
    ///     Get collection of insulation materials for pipeline channel.
    /// </summary>
    /// <returns>The collection of <see cref="MaterialDTO"/>.</returns>
    IEnumerable<MaterialDTO> GetPipelineChannelInsulationMaterials();

    /// <summary>
    ///     Get collection of insulation materials for pipes.
    /// </summary>
    /// <returns>The collection of <see cref="MaterialDTO"/>.</returns>
    IEnumerable<MaterialDTO> GetPipelineInsulationMaterials();

    /// <summary>
    ///     Get collection of pipe materials for pipeline.
    /// </summary>
    /// <returns>The collection of <see cref="MaterialDTO"/>.</returns>
    IEnumerable<MaterialDTO> GetPipeMaterials();

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
