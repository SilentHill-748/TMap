namespace TMap.MVVM.ViewModel.Settings.PipelineChannel;

public class ChannelInputDataViewModel : ViewModelBase
{
    #region Private error fields
    private const string ThicknessError = ValidationErrors.PipelineSettingsErrors.PipelineChannelErrors.ThicknessError;
    private const string ChannelDepthError = ValidationErrors.PipelineSettingsErrors.PipelineChannelErrors.ChannelDepthError;
    private const string InteraxalWidthError = ValidationErrors.PipelineSettingsErrors.PipelineChannelErrors.InteraxalWidthError;
    #endregion

    #region Private fields
    private readonly RoadSettingsModel _roadSettings;
    private readonly PipelineSettingsModel _pipelineSettings;

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

    public ChannelInputDataViewModel(SettingsModel settings)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));

        Settings = settings;
        _roadSettings = settings.RoadSettings;
        _pipelineSettings = settings.PipelineSettings;

        _pipelineSettings.Channel.InsulationLayers.CollectionChanged += InsulationLayers_CollectionChanged;
        _pipelineSettings.Channel.Pipes.CollectionChanged += Pipes_CollectionChanged;
        IsValidChanged += InputChannelDataViewModel_IsValidChanged;
    }

    private void InsulationLayers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        if (e.Action is System.Collections.Specialized.NotifyCollectionChangedAction.Reset)
            return;

        SetProperties();
        //ValidateViewModel();
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
        ValidateViewModel();
    }

    public SettingsModel Settings { get; }
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

    public int Thickness
    {
        get => _thickness;
        set
        {
            Set(ref _thickness, value, nameof(Thickness));
            UpdateProperties();
            ValidateProperty(() => Thickness < 5 || Thickness > 25, nameof(Thickness), ThicknessError);
        }
    }
    public int ChannelHeight
    {
        get => _channelHeight;
        set
        {
            Set(ref _channelHeight, value, nameof(ChannelHeight));
            UpdateProperties();
            ValidateProperty(
                () =>
                    ChannelHeight < MinChannelHeightLayout || ChannelHeight > 3000 - MinChannelHeightLayout,
                nameof(ChannelHeight),
                $"Высота коллектора должна быть между {MinChannelHeightLayout} и {3000 - MinChannelHeightLayout} см!");
        }
    }
    public int ChannelDepth
    {
        get => _channelDepth;
        set
        {
            Set(ref _channelDepth, value, nameof(ChannelDepth));
            UpdateProperties();
            ValidateProperty(() => ChannelDepth < _roadSettings.MaxDepth, nameof(ChannelDepth), ChannelDepthError);
        }
    }
    public int PipeCenterline
    {
        get => _pipeCenterline;
        set
        {
            Set(ref _pipeCenterline, value, nameof(PipeCenterline));
            ValidateProperty(
                () =>
                    PipeCenterline < MinCenterlinePosition || PipeCenterline > MaxCenterlinePosition,
                nameof(PipeCenterline),
                $"Осевая линия труб должна быть между {MinCenterlinePosition} и {MaxCenterlinePosition}");
        }
    }
    public int InteraxalWidth
    {
        get => _interaxalWidth;
        set
        {
            Set(ref _interaxalWidth, value, nameof(InteraxalWidth));
            ValidateProperty(() => InteraxalWidth < 3 || InteraxalWidth > 10, nameof(InteraxalWidth), InteraxalWidthError);
        }
    }

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

        ValidateViewModel();
    }

    private void ValidateViewModel()
    {
        ValidateProperty(() => Thickness < 5 || Thickness > 25, nameof(Thickness), ThicknessError);
        ValidateProperty(() => ChannelHeight < MinChannelHeightLayout || ChannelHeight > 3000 - MinChannelHeightLayout, nameof(ChannelHeight), $"Высота коллектора должна быть между {MinChannelHeightLayout} и {3000 - MinChannelHeightLayout} см!");
        ValidateProperty(() => ChannelDepth < _roadSettings.MaxDepth, nameof(ChannelDepth), ChannelDepthError);
        ValidateProperty(() => PipeCenterline < MinCenterlinePosition || PipeCenterline > MaxCenterlinePosition, nameof(PipeCenterline), $"Осевая линия труб должна быть между {MinCenterlinePosition} и {MaxCenterlinePosition}");
        ValidateProperty(() => InteraxalWidth < 3 || InteraxalWidth > 10, nameof(InteraxalWidth), InteraxalWidthError);
    }
}