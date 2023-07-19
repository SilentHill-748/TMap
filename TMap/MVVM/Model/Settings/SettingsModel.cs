namespace TMap.MVVM.Model.Settings;

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

    public MapSettingsModel MapSettings { get; }

    public RoadSettingsModel RoadSettings { get; }

    public PipelineSettingsModel PipelineSettings { get; }

    public bool IsCompleted { get; set; }
}
