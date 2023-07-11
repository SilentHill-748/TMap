using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using AutoMapper;

using TMap.Domain.Abstractions.Services.Material;
using TMap.Domain.Entities.Material;
using TMap.WPFCore.Commands.Modeling;

namespace TMap.MVVM.ViewModel.Map;

//TODO: Внедрить рисование температурной карты. Нужны данные с температурами.
public class MapViewModel : ViewModelBase
{
    #region Private fields
    private readonly NavigationService _navigationService;
    private WriteableBitmap? _mapBitmap;
    private WriteableBitmap? _temperatureSource;

    private readonly ImageModel _imageModel = new();
    private readonly SettingsModel _settings;
    private readonly IMaterialService _materialService;
    private readonly IMapper _mapper;
    #endregion

    public MapViewModel(IMaterialService materialService, IMapper mapper, SettingsModel settings, NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _settings = settings;
        _navigationService = navigationService;
        _materialService = materialService;
        _mapper = mapper;

        MaterialList = new ObservableCollection<MaterialModel>(
            materialService
                .GetMaterialsByType(MaterialType.Soil)
                .Select(mapper.Map<MaterialModel>));

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

    private void InitializeCommands()
    {
        DrawMapCommand = new DrawMapCommand(this, _materialService, _mapper);
        CreateModelCommand = new CreateModelCommand(this, _materialService, _mapper);
        RunModelCommand = new RunModelCommand(this);
        NavigateToSettingsCommand = new NavigateCommand<MapSettingsViewModel>(_navigationService);
    }
}
