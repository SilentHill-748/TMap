using System.Collections.ObjectModel;
using System.Linq;

using SimpleInjector;

namespace TMap.Configurations.DI;

public static class ConfigureAppServices
{
    private static ObservableCollection<Material> Materials = new MaterialHelper().GetSoilMaterials();

    public static Container RegisterServices(this Container container)
    {
        var materailHelper = new MaterialHelper();
        var settings = CreateStubSettings(materailHelper);

        //settings.MapSettings.MapWidth = settings.RoadSettings.TotalRoadWidth;

        container.RegisterSingleton<MaterialHelper>();

        //container.RegisterSingleton<DrawingService>();

        // Models
        container.RegisterSingleton<MapSettingsModel>();
        container.RegisterSingleton<RoadSettingsModel>();
        container.RegisterSingleton<PipelineSettingsModel>();
        container.RegisterSingleton<SettingsModel>();
        //container.RegisterSingleton(() => settings);

        // View models
        container.RegisterSingleton<MapViewModel>();
        container.RegisterSingleton<MapSettingsViewModel>();
        container.RegisterSingleton<RoadSettingsViewModel>();
        container.RegisterSingleton<PipeSettingsViewModel>();
        container.RegisterSingleton<PipelineChannelSettingsViewModel>();
        container.Register<MainViewModel>();

        // Services
        container.RegisterSingleton(() => new NavigationService(container));

        return container;
    }

    private static SettingsModel CreateStubSettings(MaterialHelper materialHelper)
    {
        var mapSettings = new MapSettingsModel()
        {
            EnvironmentTemperature = 25.36,
            IsFrontView = true,
            MapWidth = 0,
            MapSoilLayers = GetMapLayers()
        };

        var roadSettings = new RoadSettingsModel()
        {
            EdgeWidth = 100,
            HasMound = true,
            MoundHeight = 50,
            MoundWidth = 100,
            RoadsideWidth = 30,
            RoadWidth = 700,
            Layers = GetRoadLayers()
        };

        var pipelineSettings = new PipelineSettingsModel(materialHelper)
        {
            Channel = GetChannel()
        };

        return new SettingsModel(mapSettings, roadSettings, pipelineSettings);
    }

    private static ObservableCollection<MapLayer> GetMapLayers()
    {
        var material = new Material() { Name = "Тестовый слой." };

        return new ObservableCollection<MapLayer>()
        {
            new MapLayer(Materials.First(x => x.Name == "Травосмесь")) { Thickness = 60 },
            new MapLayer(Materials.First(x => x.Name == "Суглинок легкий пылеватый")) { Thickness = 25 },
            new MapLayer(Materials.First(x => x.Name == "Суглинок тяжелый пылеватый")) { Thickness = 30 },
            new MapLayer(Materials.First(x => x.Name == "Глина")) { Thickness = 20 },
            new MapLayer(Materials.First(x => x.Name == "Песок мелкий")) { Thickness = 25 },
            new MapLayer(Materials.First(x => x.Name == "Песок крупный")) { Thickness = 100 },
            new MapLayer(Materials.First(x => x.Name == "Каменные материалы")) { Thickness = 75 },
            new MapLayer(Materials.First(x => x.Name == "Песок мелкий")) { Thickness = 15 },
            new MapLayer(Materials.First(x => x.Name == "Песок мелкий")) { Thickness = 10 },
            new MapLayer(Materials.First(x => x.Name == "Песок крупный")) { Thickness = 20 } //689 mm
        };
    }

    private static ObservableCollection<RoadLayer> GetRoadLayers()
    {
        var material = new Material() { Name = "Тестовый слой дороги." };

        return new ObservableCollection<RoadLayer>()
        {
            new RoadLayer(Materials.First(x => x.Name == "Асфальтобетон песчаный")) { Thickness = 2, Width = 700 },
            new RoadLayer(Materials.First(x => x.Name == "Асфальтобетон мелкозернистый")) { Thickness = 4, Width = 700 },
            new RoadLayer(Materials.First(x => x.Name == "Асфальтобетон крупнозернистый")) { Thickness = 4, Width = 700 },
            new RoadLayer(Materials.First(x => x.Name == "Щебеночные (гравийные) материалы обработанные органическим вяжущим")) { Thickness = 5, Width = 750 },
            new RoadLayer(Materials.First(x => x.Name == "Щебень, обработанный органическим вяжущим по способу пропитки")) { Thickness = 5, Width = 750 },
            new RoadLayer(Materials.First(x => x.Name == "Щебеночные и гравийные материалы, не обработанные вяжущим")) { Thickness = 6, Width = 800 },
            new RoadLayer(Materials.First(x => x.Name == "Геотекстиль")) { Thickness = 1, Width = 800 },
            new RoadLayer(Materials.First(x => x.Name == "Песок пылеватый")) { Thickness = 10, Width = 800 },
            new RoadLayer(Materials.First(x => x.Name == "Песок мелкий")) { Thickness = 15, Width = 900 },
            new RoadLayer(Materials.First(x => x.Name == "Песок крупный")) { Thickness = 20, Width = 900 } //w: 1000 mm | t: 228 mm
        };
    }

    private static PipelineChannel GetChannel()
    {
        Material material = Materials.First(x => x.Name == "Железобетон");
        Material pipeMaterial = new () { Name = "Тестовый материал трубы", Color = "#adadad" };

        // 60 70 80 mm, 3 6 9 mm | Insulation: 20, 40, 30 mm
        var pipes = new ObservableCollection<Pipe>()
        {
            new Pipe(pipeMaterial) { Radius = 5, Temperature = 1, Thickness = 1 },
            new Pipe(pipeMaterial) { Radius = 10, Temperature = 1, Thickness = 1 },
            new Pipe(pipeMaterial) { Radius = 15, Temperature = 1, Thickness = 1 }
        };
        
        SetPipeInsulations(pipes);

        return new PipelineChannel(material)
        {
            ChannelDepth = 250,
            Height = 150,
            PipesCenterline = 325,
            InteraxalWidth = 10,
            InsulationLayers = GetChannelInsulations(),
            Thickness = 10,
            Pipes = pipes
        };
    }

    private static ObservableCollection<ChannelInsulation> GetChannelInsulations()
    {
        Material material = new() { Name = "Тестовый материал изоляции коллектора", Color = "#7d818a" };

        return new ObservableCollection<ChannelInsulation>()
        {
            new ChannelInsulation(material) { Thickness = 5 },
            new ChannelInsulation(material) { Thickness = 5 },
            new ChannelInsulation(material) { Thickness = 5 } // T: 80 mm
        };
    }

    private static void SetPipeInsulations(ObservableCollection<Pipe> pipes)
    {
        pipes[0].Insulation = SetPipeInsulation(3, 3, 5);      // T: 20 mm
        pipes[1].Insulation = SetPipeInsulation(5, 3, 2);    // T: 40 mm
        pipes[2].Insulation = SetPipeInsulation(5, 3, 2);      // T: 30 mm
    }

    private static ObservableCollection<RadialInsulation> SetPipeInsulation(int t1, int t2, int t3)
    {
        Material material = new() { Name = "Тестовый материал изоляции труб", Color = "#7d818a" };

        return new ObservableCollection<RadialInsulation>()
        {
            new RadialInsulation(material) { Thickness = t1 },
            new RadialInsulation(material) { Thickness = t2 },
            new RadialInsulation(material) { Thickness = t3 }
        };
    }
}
