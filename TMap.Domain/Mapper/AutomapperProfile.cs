using AutoMapper;

using TMap.Domain.DAO.Material;
using TMap.Domain.DTO.Material;
using TMap.Domain.Entities.Material;

namespace TMap.Domain.Mapper;

public class AutomapperProfile : Profile
{
    public AutomapperProfile()
    {
        CreateMap<Material, MaterialDTO>();
        CreateMap<Material, MaterialDAO>();
    }
}