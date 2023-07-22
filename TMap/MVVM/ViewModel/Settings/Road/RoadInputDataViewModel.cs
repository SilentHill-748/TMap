namespace TMap.MVVM.ViewModel.Settings.Road;

public class RoadInputDataViewModel : ViewModelBase
{
    #region Dependencies
    private readonly SettingsModel _settings;
    private readonly RoadInputDataValidator _validator;
    #endregion

    #region Private fields
    private int _width;
    private int _moundWidth;
    private int _moundHeight;
    private bool _hasMound;
    private int _edgeWidth;
    private int _roadsideWidth;
    #endregion

    public RoadInputDataViewModel(SettingsModel roadSettings, RoadInputDataValidator validator)
    {
        ArgumentNullException.ThrowIfNull(roadSettings, nameof(roadSettings));
        ArgumentNullException.ThrowIfNull(validator, nameof(validator));

        _settings = roadSettings;
        _validator = validator;

        Validate(validator, this);

        IsValidChanged += InputRoadSettingsViewModel_IsValidChanged;
        PropertyChanged += RoadInputDataViewModel_PropertyChanged;
    }

    #region Notify properties
    public int Width
    {
        get => _width;
        set => Set(ref _width, value, nameof(Width));
        
    }
    public int MoundWidth
    {
        get => _moundWidth;
        set => Set(ref _moundWidth, value, nameof(MoundWidth));
    }
    public int MoundHeight
    {
        get => _moundHeight;
        set => Set(ref _moundHeight, value, nameof(MoundHeight));
    }
    public int RoadsideWidth
    {
        get => _roadsideWidth;
        set => Set(ref _roadsideWidth, value, nameof(RoadsideWidth));
    }
    public int EdgeWidth
    {
        get => _edgeWidth;
        set => Set(ref _edgeWidth, value, nameof(EdgeWidth));
    }
    public bool HasMound
    {
        get => _hasMound;
        set => Set(ref _hasMound, value, nameof(HasMound));
    }
    #endregion

    #region Event handlers
    private void InputRoadSettingsViewModel_IsValidChanged()
    {
        var roadSettings = _settings.RoadSettings;

        if (IsValid)
        {
            roadSettings.EdgeWidth = EdgeWidth;
            roadSettings.RoadsideWidth = RoadsideWidth;
            roadSettings.RoadWidth = Width;
            roadSettings.HasMound = HasMound;
            roadSettings.MoundWidth = MoundWidth;
            roadSettings.MoundHeight = MoundHeight;

            _settings.MapSettings.MapWidth = roadSettings.TotalRoadWidth;
        }
    }

    private void RoadInputDataViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Validate(_validator, this);
    }
    #endregion
}
