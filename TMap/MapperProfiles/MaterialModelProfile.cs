using AutoMapper;

using TMap.Domain.Entities.Material;

namespace TMap.MapperProfiles;

public class MaterialModelProfile : Profile
{
    public MaterialModelProfile()
    {
        CreateMap<Material, MaterialModel>();
        CreateMap<MaterialModel, Material>();
    }
}
