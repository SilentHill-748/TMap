namespace TMap.MVVM.Model.Settings;

public class PipelineSettingsModel
{
    public PipelineSettingsModel()
    {
        Channel = new PipelineChannel();
    }

    public bool IsSkiped { get; set; }

    public PipelineChannel Channel { get; set; }
}