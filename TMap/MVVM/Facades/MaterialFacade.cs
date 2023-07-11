using System;
using System.Collections.ObjectModel;
using System.Linq;

using AutoMapper;

using TMap.Domain.Abstractions.Services.Material;
using TMap.Domain.Entities.Material;

namespace TMap.MVVM.Facades;

public class MaterialFacade
{
    private readonly IMaterialService _materialService;
    private readonly IMapper _mapper;

    public MaterialFacade(IMaterialService materialService, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(materialService, nameof(materialService));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        _materialService = materialService;
        _mapper = mapper;
    }

    public ObservableCollection<MaterialModel> GetMaterialModels(MaterialType type)
    {
        var materials = _materialService.GetMaterialsByType(type);

        var models = materials.Select(_mapper.Map<MaterialModel>);

        return new ObservableCollection<MaterialModel>(models);
    }

    public MaterialModel GetMaterialModel(MaterialType type, string name)
    {
        var airMaterial = _materialService
            .GetMaterialsByType(type)
            .First(material => material.Name.Equals(name));

        return _mapper.Map<MaterialModel>(airMaterial);
    }
}
