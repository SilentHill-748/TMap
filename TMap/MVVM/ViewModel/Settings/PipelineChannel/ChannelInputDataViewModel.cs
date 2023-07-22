namespace TMap.MVVM.ViewModel.Settings.PipelineChannel;

public class ChannelInputDataViewModel : ViewModelBase
{
    #region Dependencies
    private readonly ChannelInputDataValidator _validator;
    private readonly RoadSettingsModel _roadSettings;
    private readonly PipelineSettingsModel _pipelineSettings;
    #endregion

    #region Private fields
    private int _thickness;
    private int _channelHeight;
    private int _channelDepth;
    private int _pipeCenterline;
    private int _interaxalWidth;

    private int _insulationThickness;
    private int _totalThickness;
    private int _minChannelHeightLayout;
    private int _maxChannelHeightLayout;
    private int _minCenterlinePosition;
    private int _maxCenterlinePosition;

    private string? _channelHeightPlaceholder;
    private string? _channelDepthPlaceholder;
    private string? _pipeCenterlinePlaceholder;
    #endregion

    public ChannelInputDataViewModel(SettingsModel settings, ChannelInputDataValidator validator)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        Settings = settings;
        _validator = validator;
        _roadSettings = settings.RoadSettings;
        _pipelineSettings = settings.PipelineSettings;

        _pipelineSettings.Channel.InsulationLayers.CollectionChanged += InsulationLayers_CollectionChanged;
        _pipelineSettings.Channel.Pipes.CollectionChanged += Pipes_CollectionChanged;
        IsValidChanged += InputChannelDataViewModel_IsValidChanged;
        PropertyChanged += ChannelInputDataViewModel_PropertyChanged;

        Validate(validator, this);
    }

    #region Public properties
    public SettingsModel Settings { get; }
    #endregion

    #region Notify second properties
    public int ChannelInsulationThickness
    {
        get => _insulationThickness;
        set => Set(ref _insulationThickness, value, nameof(ChannelInsulationThickness));
    }
    public int TotalThickness
    {
        get => _totalThickness;
        set => Set(ref _totalThickness, value, nameof(TotalThickness));
    }
    public int MinChannelHeightLayout
    {
        get => _minChannelHeightLayout;
        set => Set(ref _minChannelHeightLayout, value, nameof(MinChannelHeightLayout));
    }
    public int MaxChannelHeightLayout
    {
        get => _maxChannelHeightLayout;
        set => Set(ref _maxChannelHeightLayout, value, nameof(MaxChannelHeightLayout));
    }
    public int MinCenterlinePosition
    {
        get => _minCenterlinePosition;
        set => Set(ref _minCenterlinePosition, value, nameof(MinCenterlinePosition));
    }
    public int MaxCenterlinePosition
    {
        get => _maxCenterlinePosition;
        set => Set(ref _maxCenterlinePosition, value, nameof(MaxCenterlinePosition));
    }
    public string? ChannelHeightPlaceholder
    {
        get => _channelHeightPlaceholder;
        set => Set(ref _channelHeightPlaceholder, value, nameof(ChannelHeightPlaceholder));
    }
    public string? ChannelDepthPlaceholder
    {
        get => _channelDepthPlaceholder;
        set => Set(ref _channelDepthPlaceholder, value, nameof(ChannelDepthPlaceholder));
    }
    public string? PipeCenterlinePlaceholder
    {
        get => _pipeCenterlinePlaceholder;
        set => Set(ref _pipeCenterlinePlaceholder, value, nameof(PipeCenterlinePlaceholder));
    }
    #endregion

    #region Notify properties
    public int Thickness
    {
        get => _thickness;
        set
        {
            Set(ref _thickness, value, nameof(Thickness));
            UpdateProperties();
        }
    }
    public int ChannelHeight
    {
        get => _channelHeight;
        set
        {
            Set(ref _channelHeight, value, nameof(ChannelHeight));
            UpdateProperties();
        }
    }
    public int ChannelDepth
    {
        get => _channelDepth;
        set
        {
            Set(ref _channelDepth, value, nameof(ChannelDepth));
            UpdateProperties();
        }
    }
    public int PipeCenterline
    {
        get => _pipeCenterline;
        set => Set(ref _pipeCenterline, value, nameof(PipeCenterline));
    }
    public int InteraxalWidth
    {
        get => _interaxalWidth;
        set => Set(ref _interaxalWidth, value, nameof(InteraxalWidth));
    }
    #endregion

    #region Event handlers
    private void ChannelInputDataViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Validate(_validator, this);
    }

    private void InsulationLayers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.Action is System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            return;

        SetProperties();
    }

    private void InputChannelDataViewModel_IsValidChanged()
    {
        var channel = _pipelineSettings.Channel;

        if (IsValid)
        {
            channel.ChannelDepth = ChannelDepth;
            channel.Thickness = Thickness;
            channel.Height = ChannelHeight;
            channel.PipesCenterline = PipeCenterline;
            channel.InteraxalWidth = InteraxalWidth;
        }
    }

    private void Pipes_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        SetProperties();
        Validate(_validator, this);
    }
    #endregion

    #region Private methods
    private void SetProperties()
    {
        ChannelDepth = ChannelDepth > _roadSettings.MaxDepth ? ChannelDepth : _roadSettings.MaxDepth;
        UpdateProperties();
        ChannelHeight = MinChannelHeightLayout;
    }

    private void UpdateProperties()
    {
        ChannelInsulationThickness = _pipelineSettings.Channel.InsulationThickness;
        TotalThickness = Thickness + ChannelInsulationThickness;

        var maxDiameterPipe = _pipelineSettings.Channel.Pipes.MaxBy(x => x.Radius + x.InsulationThickness);

        if (maxDiameterPipe is not { })
            return;

        var pipeFullDiameter = (maxDiameterPipe.Radius + maxDiameterPipe.InsulationThickness) * 2;

        MinChannelHeightLayout = 2 * TotalThickness + pipeFullDiameter + InteraxalWidth * 2;
        MaxChannelHeightLayout = 3000 - MinChannelHeightLayout;

        MinCenterlinePosition = ChannelDepth + MinChannelHeightLayout / 2;
        MaxCenterlinePosition = ChannelDepth + MinChannelHeightLayout / 2 + (ChannelHeight < MinChannelHeightLayout ? MinChannelHeightLayout : ChannelHeight - MinChannelHeightLayout);

        ChannelDepthPlaceholder = $"от {_roadSettings.MaxDepth} см";
        ChannelHeightPlaceholder = $"от {MinChannelHeightLayout} до {3000 - MinChannelHeightLayout} см";
        PipeCenterlinePlaceholder = $"от {MinCenterlinePosition} до {MaxCenterlinePosition} см";
    }
    #endregion
}