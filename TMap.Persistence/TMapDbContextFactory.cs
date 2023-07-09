using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TMap.Persistence;

public class TMapDbContextFactory : IDesignTimeDbContextFactory<TMapDbContext>
{
    public TMapDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder();

        optionsBuilder.UseSqlite("Data Source=tmap.db");

        return new TMapDbContext(optionsBuilder.Options);
    }
}
