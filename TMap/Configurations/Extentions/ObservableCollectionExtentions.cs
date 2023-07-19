namespace TMap.Configurations.Extentions;

public static class ObservableCollectionExtentions
{
    public static void UpdateCollection<T>(this ObservableCollection<T> collection, IEnumerable<T> values)
    {
        collection.Clear();
        
        foreach (T value in values)
            collection.Add(value);
    }
}
