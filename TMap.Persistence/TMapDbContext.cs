using System.Reflection;

using Microsoft.EntityFrameworkCore;

using TMap.Domain.Entities.Material;

namespace TMap.Persistence;

public class TMapDbContext : DbContext
{
    public TMapDbContext(DbContextOptions optopns) : base(optopns) { }

    public DbSet<Material> Materials => Set<Material>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
