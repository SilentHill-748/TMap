using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using CommunityToolkit.Mvvm.Messaging;

using TMap.MVVM.Messages;
using TMap.WPFCore.Commands.Modeling;

namespace TMap.MVVM.ViewModel.Map;

//TODO: Внедрить рисование температурной карты. Нужны данные с температурами.
public class MapViewModel : ViewModelBase
{
    #region Private fields
    private readonly NavigationService _navigationService;
    private WriteableBitmap? _mapBitmap;
    private WriteableBitmap? _temperatureSource;

    private readonly MathModel _mathModel;
    private readonly ImageModel _imageModel = new();
    private readonly SettingsModel _settings;
    private readonly MaterialHelper _materialHelper;
    #endregion

    public MapViewModel(MaterialHelper materialHelper, SettingsModel settings, NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _settings = settings;
        _navigationService = navigationService;
        _materialHelper = materialHelper;

        MaterialList = materialHelper.GetSoilMaterials();

        InitializeCommands();

        WeakReferenceMessenger.Default.Register<SettingsDoneMessage>(this, OnSettingsCompleted);
    }

    private void OnSettingsCompleted(object recipient, SettingsDoneMessage message)
    {
        _materialHelper.DefaultMaterial.InitialTemperature = Settings.MapSettings.EnvironmentTemperature;
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

    private void InitializeCommands()
    {
        DrawMapCommand = new DrawMapCommand(this, _materialHelper.DefaultMaterial);
        CreateModelCommand = new CreateModelCommand(this, _materialHelper);
        RunModelCommand = new RunModelCommand(this);
        NavigateToSettingsCommand = new NavigateCommand<MapSettingsViewModel>(_navigationService);
    }

    //private Dictionary<Color, Material> GetMaterialMap(MaterialHelper materialHelper)
    //{
    //    var materials = new List<Material>();
    //    var map = new Dictionary<Color, Material>();

    //    materials.AddRange(materialHelper.GetSoilMaterials());
    //    materials.AddRange(materialHelper.GetPipeInsulationMaterials());
    //    materials.AddRange(materialHelper.GetPipeMaterials());
    //    materials.AddRange(materialHelper.GetChannelInsulationMaterials());

    //    foreach (Material material in materials)
    //    {
    //        var color = (Color)ColorConverter.ConvertFromString(material.Color);
            
    //        map.Add(color, material);
    //    }

    //    return map;
    //}
}
