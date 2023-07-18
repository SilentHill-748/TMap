namespace TMap.MVVM.ViewModel.Settings.Road;

public class InputRoadSettingsViewModel : ViewModelBase
{
    private const string RoadWidthError = ValidationErrors.RoadSettingsErrors.InputRoadSettingsErrors.RoadWidthError;
    private const string MoundWidthError = ValidationErrors.RoadSettingsErrors.InputRoadSettingsErrors.MoundWidthError;
    private const string MoundHeightError = ValidationErrors.RoadSettingsErrors.InputRoadSettingsErrors.MoundHeightError;
    private const string RoadsideWidthError = ValidationErrors.RoadSettingsErrors.InputRoadSettingsErrors.RoadsideWidthError;
    private const string EdgeWidthError = ValidationErrors.RoadSettingsErrors.InputRoadSettingsErrors.EdgeWidthError;

    private readonly SettingsModel _settings;

    private int _width;
    private int _moundWidth;
    private int _moundHeight;
    private bool _hasMound;
    private int _edgeWidth;
    private int _roadsideWidth;

    public InputRoadSettingsViewModel(SettingsModel roadSettings)
    {
        InitialValidation();

        _settings = roadSettings;

        IsValidChanged += InputRoadSettingsViewModel_IsValidChanged;
    }

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

    #region Notify properties
    public int Width
    {
        get => _width;
        set
        {
            Set(ref _width, value, nameof(Width));
            ValidateRoadWidth();
        }
    }
    public int MoundWidth
    {
        get => _moundWidth;
        set
        {
            Set(ref _moundWidth, value, nameof(MoundWidth));
            if (HasMound) ValidateMoundWidth();
        }
    }
    public int MoundHeight
    {
        get => _moundHeight;
        set
        {
            Set(ref _moundHeight, value, nameof(MoundHeight));
            if (HasMound) ValidateMoundHeight();
        }
    }
    public int RoadsideWidth
    {
        get => _roadsideWidth;
        set
        {
            Set(ref _roadsideWidth, value, nameof(RoadsideWidth));
            ValidateRoadsideWidth();
        }
    }
    public int EdgeWidth
    {
        get => _edgeWidth;
        set
        {
            Set(ref _edgeWidth, value, nameof(EdgeWidth));
            ValidateEdgeWidth();
        }
    }
    public bool HasMound
    {
        get => _hasMound;
        set
        {
            Set(ref _hasMound, value, nameof(HasMound));

            ClearErrors(nameof(MoundWidth));
            ClearErrors(nameof(MoundHeight));

            if (value)
            {
                ValidateMoundWidth();
                ValidateMoundHeight();
            }
        }
    }
    #endregion

    private void InitialValidation()
    {
        ValidateRoadWidth();
        ValidateRoadsideWidth();
        ValidateEdgeWidth();
    }

    private void ValidateRoadWidth()
        => ValidateProperty(() => Width < 700, nameof(Width), RoadWidthError);
    private void ValidateMoundWidth()
        => ValidateProperty(() => Between(50, 100, MoundWidth), nameof(MoundWidth), MoundWidthError);
    private void ValidateMoundHeight()
        => ValidateProperty(() => Between(20, 100, MoundHeight), nameof(MoundHeight), MoundHeightError);
    private void ValidateRoadsideWidth()
        => ValidateProperty(() => Between(25, 50, RoadsideWidth), nameof(RoadsideWidth), RoadsideWidthError);
    private void ValidateEdgeWidth()
        => ValidateProperty(() => EdgeWidth < 50, nameof(EdgeWidth), EdgeWidthError);

    private static bool Between(int left, int right, int value)
        => value < left || value > right;

}
