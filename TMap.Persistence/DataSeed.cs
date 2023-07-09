using System.Text.Json;

using TMap.Domain.Entities.Material;

namespace TMap.Persistence;

public class DataSeed
{
    private readonly TMapDbContext _dbContext;

    public DataSeed(TMapDbContext dbContext)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));

        _dbContext = dbContext;
    }

    public async Task SeedAsync()
    {
        var json = Path.Combine(Directory.GetCurrentDirectory(), "Materials.json");

        var materials = JsonSerializer.Deserialize<List<Material>>(File.Open(json, FileMode.Open));

        if (materials is null)
            throw new Exception("Не удалось загрузить данные по базовым материалам!");

        await _dbContext.Materials.AddRangeAsync(materials);
        
        _ = await _dbContext.SaveChangesAsync();
    }
}
