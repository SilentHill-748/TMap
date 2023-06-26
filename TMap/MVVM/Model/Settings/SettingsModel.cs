namespace TMap.MVVM.Model.Settings;

/// <summary>
///     Represents global settings for map.
/// </summary>
public class SettingsModel
{
    public SettingsModel(
        MapSettingsModel mapSettings,
        RoadSettingsModel roadSettings, 
        PipelineSettingsModel pipelineSettings)
    {
        MapSettings = mapSettings;
        RoadSettings = roadSettings;
        PipelineSettings = pipelineSettings;
    }

    /// <summary>
    ///     Настройки карты.
    /// </summary>
    public MapSettingsModel MapSettings { get; }

    /// <summary>
    ///     Настройки дорожной одежды.
    /// </summary>
    public RoadSettingsModel RoadSettings { get; }

    /// <summary>
    ///     Настройки трубопровода.
    /// </summary>
    public PipelineSettingsModel PipelineSettings { get; }

    public bool IsComplete { get; set; }

    /// <summary>
    ///     Создает или перезаписывает файл настроек карты.
    /// </summary>
    public void ToJson()
    {

    }
}
