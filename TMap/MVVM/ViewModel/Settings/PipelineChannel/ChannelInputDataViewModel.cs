namespace TMap.MVVM.ViewModel.Settings.PipelineChannel;

public class ChannelInputDataViewModel : ViewModelBase
{
    #region Dependencies
    private readonly ChannelInputDataValidator _validator;
    private readonly PipelineSettingsModel _pipelineSettings;
    #endregion

    #region Private fields
    private int _thickness;
    private int _channelHeight;
    private int _channelDepth;
    private int _pipeCenterline;
    private int _interaxalWidth;
    #endregion

    public ChannelInputDataViewModel(SettingsModel settings, ChannelInputDataValidator validator)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        Settings = settings;
        _validator = validator;
        _pipelineSettings = settings.PipelineSettings;

        Data = new PipelineChannelInputDataModel(settings, this);

        _pipelineSettings.Channel.InsulationLayers.CollectionChanged += InsulationLayers_CollectionChanged;
        _pipelineSettings.Channel.Pipes.CollectionChanged += Pipes_CollectionChanged;
        IsValidChanged += InputChannelDataViewModel_IsValidChanged;
        PropertyChanged += ChannelInputDataViewModel_PropertyChanged;

        Validate(validator, this);
    }

    #region Public properties
    public SettingsModel Settings { get; }
    public PipelineChannelInputDataModel Data { get; }
    #endregion

    #region Notify properties
    public int Thickness
    {
        get => _thickness;
        set
        {
            Set(ref _thickness, value, nameof(Thickness));
            UpdateData();
        }
    }
    public int ChannelHeight
    {
        get => _channelHeight;
        set
        {
            Set(ref _channelHeight, value, nameof(ChannelHeight));
            UpdateData();
        }
    }
    public int ChannelDepth
    {
        get => _channelDepth;
        set
        {
            Set(ref _channelDepth, value, nameof(ChannelDepth));
            UpdateData();
        }
    }
    public int PipeCenterline
    {
        get => _pipeCenterline;
        set
        {
            Set(ref _pipeCenterline, value, nameof(PipeCenterline));
            UpdateData();
        }
    }
    public int InteraxalWidth
    {
        get => _interaxalWidth;
        set
        {
            Set(ref _interaxalWidth, value, nameof(InteraxalWidth));
            UpdateData();
        }
    }
    #endregion

    #region Event handlers
    private void ChannelInputDataViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Validate(_validator, this);
    }

    private void Pipes_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        Data.SetProperties();

        UpdateData();
    }

    private void InsulationLayers_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        if (e.Action is NotifyCollectionChangedAction.Reset)
            return;

        UpdateData();
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
    #endregion

    #region Private methods
    private void UpdateData()
    {
        Data.UpdateProperties();

        OnPropertyChanged(nameof(Data));
    }
    #endregion
}