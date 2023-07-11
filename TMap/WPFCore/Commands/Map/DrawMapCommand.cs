using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using AutoMapper;

using TMap.Domain.Abstractions.Services.Material;
using TMap.WPFCore.Commands.Base;
using TMap.Domain.Entities.Material;
using System.Linq;

namespace TMap.WPFCore.Commands.Map;

public class DrawMapCommand : CommandBase
{
    private readonly MapViewModel _viewModel;
    private readonly IMaterialService _materialService;
    private readonly IMapper _mapper;

    public DrawMapCommand(MapViewModel mapViewModel, IMaterialService materialService, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(mapViewModel, nameof(mapViewModel));
        ArgumentNullException.ThrowIfNull(materialService, nameof(materialService));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        _viewModel = mapViewModel;
        _materialService = materialService;
        _mapper = mapper;
    }

    protected override void Execute()
    {
        var width = _viewModel.Settings.MapSettings.MapWidth;
        var height = _viewModel.Settings.MapSettings.MapHeight + _viewModel.Settings.RoadSettings.MoundHeight;

        _viewModel.MapBitmap = new WriteableBitmap(width + 2, height + 2, 96, 96, PixelFormats.Bgra32, null);
        _viewModel.MapBitmap.Clear(Colors.White);

        var material = _materialService.GetMaterialsByType(MaterialType.Environment).First();

        var drawingService = new DrawingService(_viewModel.Settings, _viewModel.MapBitmap, _mapper.Map<MaterialModel>(material));

        drawingService.DrawMainMap();
        drawingService.DrawRoadMap();
        drawingService.DrawPipelineMap();
    }

    public override bool CanExecute()
    {
        return _viewModel.Settings.IsCompleted;
    }
}
