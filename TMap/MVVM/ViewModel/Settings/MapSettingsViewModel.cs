using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace TMap.MVVM.ViewModel.Settings;

public class MapSettingsViewModel : ViewModelBase
{
    #region Private fields
    private readonly NavigationService _navigationService;
    private readonly ObservableCollection<MaterialModel> _materials;
    private readonly MapSettingsModel _settings;

    private readonly CreateLayerViewModel _createLayerViewModel;
    private readonly InputMapSettingsViewModel _inputMapSettingsViewModel;

    private int _titleFontSize;
    #endregion

    public MapSettingsViewModel(
        MapSettingsModel settings, 
        MaterialHelper materialHelper,
        NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(materialHelper, nameof(materialHelper));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _materials = materialHelper.GetSoilMaterials();
        _settings = settings;
        _navigationService = navigationService;
        _createLayerViewModel = new CreateLayerViewModel(_materials);
        _inputMapSettingsViewModel = new InputMapSettingsViewModel(settings);

        WindowTitle = "Настройка карты геологического среза";
        TitleFontSize = 22;

        _settings.MapSoilLayers.CollectionChanged += MapSoilLayers_CollectionChanged;
        _createLayerViewModel.LayerCreated += CreateLayerViewModel_LayerCreated;
        _inputMapSettingsViewModel.IsValidChanged += InputMapSettingsViewModel_IsValidChanged;

        InitCommands();
    }

    #region Public properties
    public ObservableCollection<MaterialModel> Materials => _materials;
    public MapSettingsModel Settings => _settings;
    public CreateLayerViewModel CreateLayerView => _createLayerViewModel;
    public InputMapSettingsViewModel InputMapSettingsView => _inputMapSettingsViewModel;
    #endregion

    #region Notify properties
    public bool IsInvalidMapHeight => _settings.MapHeight < 100;
    public string ViewTitle => WindowTitle;
    public int TitleFontSize
    {
        get => _titleFontSize;
        set => Set(ref _titleFontSize, value, nameof(_titleFontSize));
    }
    public bool CanNext => ValidateKeyProperties();
    #endregion

    #region Commands
    public ICommand? RemoveLayerCommand { get; private set; }
    public ICommand? NavigateNextCommand { get; private set; }
    #endregion

    #region Private methods
    private void CreateLayerViewModel_LayerCreated(Layer layer)
    {
        Settings.MapSoilLayers.Add(layer);
    }

    private void MapSoilLayers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsInvalidMapHeight));
        OnPropertyChanged(nameof(CanNext));
    }

    private void InputMapSettingsViewModel_IsValidChanged()
    {
        OnPropertyChanged(nameof(IsInvalidMapHeight));
        OnPropertyChanged(nameof(CanNext));
    }

    private void InitCommands()
    {
        NavigateNextCommand = new NavigateCommand<RoadSettingsViewModel>(_navigationService);
        RemoveLayerCommand = new RemoveMapLayerCommand(this);
    }

    private bool ValidateKeyProperties()
    {
        var isValidInput = InputMapSettingsView.IsValid;

        return (!IsInvalidMapHeight) && (isValidInput);
    }
    #endregion
}