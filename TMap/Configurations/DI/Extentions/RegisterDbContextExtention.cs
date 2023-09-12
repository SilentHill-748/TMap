using Container = SimpleInjector.Container;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterDbContextExtention
{
    public static Container RegisterDbContext(this Container container)
    {
        string dbFolder = GetDbFolder();
        string dbFilename = Path.Combine(dbFolder, "tmap.db");

        var options = new DbContextOptionsBuilder()
            .UseSqlite($"Data source={dbFilename};")
            .Options;

        container.RegisterSingleton(() => new TMapDbContext(options));

        return container;
    }

    private static string GetDbFolder()
    {
        string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string databaseFolder = Path.Combine(myDocumentsFolder, "TMap");

        if (!Directory.Exists(databaseFolder))
            Directory.CreateDirectory(databaseFolder);

        return databaseFolder;
    }
}
