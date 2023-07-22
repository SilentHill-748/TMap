namespace TMap.MVVM.Model.Settings;

public class PipelineChannelInputDataModel
{
    private readonly ChannelInputDataViewModel _viewModel;
    private readonly RoadSettingsModel _roadSettings;
    private readonly PipelineSettingsModel _pipelineSettings;

    public PipelineChannelInputDataModel(SettingsModel settings, ChannelInputDataViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(viewModel, nameof(viewModel));

        _viewModel = viewModel;
        _roadSettings = settings.RoadSettings;
        _pipelineSettings = settings.PipelineSettings;
    }

    public int ChannelInsulationThickness { get; set; }

    public int TotalThickness { get; set; }

    public int MinChannelHeightLayout { get; set; }

    public int MaxChannelHeightLayout { get; set; }

    public int MinCenterlinePosition { get; set; }

    public int MaxCenterlinePosition { get; set; }

    public string? ChannelHeightPlaceholder { get; set; }

    public string? ChannelDepthPlaceholder { get; set; }

    public string? PipeCenterlinePlaceholder { get; set; }

    public void SetProperties()
    {
        _viewModel.ChannelDepth = _viewModel.ChannelDepth > _roadSettings.MaxDepth ? _viewModel.ChannelDepth : _roadSettings.MaxDepth;
        _viewModel.ChannelHeight = MinChannelHeightLayout;
    }

    public void UpdateProperties()
    {
        ChannelInsulationThickness = _pipelineSettings.Channel.InsulationThickness;
        TotalThickness = _viewModel.Thickness + ChannelInsulationThickness;

        SetChannelHeightPositions();

        SetPipeCenterlinePositions();
        
        SetPlaceholders();
    }

    private void SetChannelHeightPositions()
    {
        var maxDiameterPipe = _pipelineSettings.Channel.Pipes.MaxBy(x => x.Radius + x.InsulationThickness);

        if (maxDiameterPipe is not { })
            return;

        var pipeFullDiameter = (maxDiameterPipe.Radius + maxDiameterPipe.InsulationThickness) * 2;

        MinChannelHeightLayout = 2 * TotalThickness + pipeFullDiameter + _viewModel.InteraxalWidth * 2;
        MaxChannelHeightLayout = 3000 - MinChannelHeightLayout;
    }

    private void SetPipeCenterlinePositions()
    {
        var lowPosition = _viewModel.ChannelDepth + MinChannelHeightLayout / 2;
        var highPosition = _viewModel.ChannelHeight < MinChannelHeightLayout ? MinChannelHeightLayout : _viewModel.ChannelHeight - MinChannelHeightLayout;

        MinCenterlinePosition = lowPosition;
        MaxCenterlinePosition = lowPosition + highPosition;
    }

    private void SetPlaceholders()
    {
        ChannelDepthPlaceholder = $"от {_roadSettings.MaxDepth} см";
        ChannelHeightPlaceholder = $"от {MinChannelHeightLayout} до {3000 - MinChannelHeightLayout} см";
        PipeCenterlinePlaceholder = $"от {MinCenterlinePosition} до {MaxCenterlinePosition} см";
    }
}