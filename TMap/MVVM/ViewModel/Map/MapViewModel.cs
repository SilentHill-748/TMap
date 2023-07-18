using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using TMap.Configurations.Extentions;
using TMap.MVVM.Stores;
using TMap.WPFCore.Commands.Modeling;

namespace TMap.MVVM.ViewModel.Map;

public class MapViewModel : ViewModelBase
{
    #region Private fields
    private readonly NavigationService _navigationService;
    private WriteableBitmap? _mapBitmap;
    private WriteableBitmap? _temperatureSource;

    private readonly ImageModel _imageModel = new();
    private readonly SettingsModel _settings;
    private readonly MaterialStore _materialStore;
    #endregion

    public MapViewModel(MaterialStore materialStore, SettingsModel settings, NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(materialStore, nameof(materialStore));
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _settings = settings;
        _navigationService = navigationService;
        _materialStore = materialStore;

        MaterialList = new ObservableCollection<MaterialModel>(_materialStore.Materials);

        _materialStore.StoreChanged += MaterialStore_StoreChanged;

        InitializeCommands();
    }

    #region Models
    public ImageModel ImageModel => _imageModel;
    public SettingsModel Settings => _settings;
    public MathModel? MathModel { get; set; }
    public WriteableBitmap? TemperatureSource
    {
        get => _temperatureSource;
        set => Set(ref _temperatureSource, value, nameof(TemperatureSource));
    }

    public ObservableCollection<MaterialModel> MaterialList { get; }
    #endregion

    #region Observable properties
    public WriteableBitmap? MapBitmap
    {
        get => _mapBitmap;
        set => Set(ref _mapBitmap, value, nameof(MapBitmap));
    }
    #endregion

    #region Commands
    public ICommand? DrawMapCommand { get; private set; }
    public ICommand? RunModelCommand { get; private set; }
    public ICommand? CreateModelCommand { get; private set; }
    public ICommand? NavigateToSettingsCommand { get; private set; }
    #endregion

    #region Private methods
    private void InitializeCommands()
    {
        DrawMapCommand = new DrawMapCommand(this, _materialStore);
        CreateModelCommand = new CreateModelCommand(this, _materialStore);
        RunModelCommand = new RunModelCommand(this);
        NavigateToSettingsCommand = new NavigateCommand<MapSettingsViewModel>(_navigationService);
    }

    private void MaterialStore_StoreChanged()
    {
        MaterialList.UpdateCollection(_materialStore.Materials);
    }
    #endregion
}
