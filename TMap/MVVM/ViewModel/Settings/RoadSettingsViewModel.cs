using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

using CommunityToolkit.Mvvm.Messaging;

namespace TMap.MVVM.ViewModel.Settings;

public class RoadSettingsViewModel : ViewModelBase
{
    private readonly SettingsModel _settings;
    private readonly CreateRoadLayerViewModel _createRoadLayerViewModel;
    private readonly InputRoadSettingsViewModel _inputRoadSettingsViewModel;

    private int _viewTitleFontSize;

    public RoadSettingsViewModel(
        SettingsModel settings, 
        MaterialHelper materialHelper,
        NavigationService navigationService)
    {
        ArgumentNullException.ThrowIfNull(settings, nameof(settings));
        ArgumentNullException.ThrowIfNull(materialHelper, nameof(materialHelper));
        ArgumentNullException.ThrowIfNull(navigationService, nameof(navigationService));

        _settings = settings;
        Materials = materialHelper.GetSoilMaterials();

        _createRoadLayerViewModel = new CreateRoadLayerViewModel(Materials);
        _inputRoadSettingsViewModel = new InputRoadSettingsViewModel(settings);

        WindowTitle = "Настройка гелогоческого среза дорожной конструкции";
        ViewTitleFontSize = 22;

        NavigateNextCommand = new NavigateCommand<PipeSettingsViewModel>(navigationService);
        NavigateBackCommand = new NavigateCommand<MapSettingsViewModel>(navigationService);
        RemoveRoadLayerCommand = new RemoveRoadLayerCommand(this);

        _inputRoadSettingsViewModel.IsValidChanged += InputRoadSettingsViewModel_IsValidChanged;
        Settings.Layers.CollectionChanged += Layers_CollectionChanged;
        WeakReferenceMessenger.Default.Register<CreateRoadLayerMessage>(this, LayerCreated);
    }

    #region Public properties
    public ObservableCollection<Material> Materials { get; }
    public CreateRoadLayerViewModel CreateRoadLayerView => _createRoadLayerViewModel;
    public InputRoadSettingsViewModel InputRoadSettingsView => _inputRoadSettingsViewModel;
    public RoadSettingsModel Settings => _settings.RoadSettings;
    #endregion

    #region Notify properties
    public string ViewTitle => WindowTitle;
    public int ViewTitleFontSize
    {
        get => _viewTitleFontSize;
        set => Set(ref _viewTitleFontSize, value, nameof(ViewTitleFontSize));
    }
    public bool IsValidLayerCount => Settings.Layers.Count > 0;
    public bool HasNext => IsValidLayerCount && InputRoadSettingsView.IsValid;
    #endregion

    public ICommand NavigateNextCommand { get; }
    public ICommand NavigateBackCommand { get; }
    public ICommand RemoveRoadLayerCommand { get; }

    private void LayerCreated(object recipient, CreateRoadLayerMessage message)
    {
        if (message is not { })
            throw new Exception("Ошибка сохранения созданного слоя дорожной конструкции!");

        Settings.Layers.Add(message.Value);
        OnPropertyChanged(nameof(HasNext));
        OnPropertyChanged(nameof(IsValidLayerCount));
    }

    private void InputRoadSettingsViewModel_IsValidChanged()
    {
        OnPropertyChanged(nameof(IsValidLayerCount));
        OnPropertyChanged(nameof(HasNext));
    }

    private void Layers_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        OnPropertyChanged(nameof(IsValidLayerCount));
        OnPropertyChanged(nameof(HasNext));
    }
}