using System;

namespace TMap.MVVM.Model.Settings;

public class PipelineSettingsModel
{
    public PipelineSettingsModel(MaterialModel channelMaterial)
    {
        ArgumentNullException.ThrowIfNull(channelMaterial, nameof(channelMaterial));

        Channel = new PipelineChannel(channelMaterial);
    }

    public bool IsSkiped { get; set; }

    public PipelineChannel Channel { get; set; }
}
