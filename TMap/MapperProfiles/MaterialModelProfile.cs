namespace TMap.MapperProfiles;

public class MaterialModelProfile : Profile
{
    public MaterialModelProfile()
    {
        CreateMap<Material, MaterialModel>();
        CreateMap<MaterialModel, Material>();
        CreateMap<MaterialDTO, MaterialModel>();
    }
}
