using System.Threading.Tasks;

using AutoMapper;

using SimpleInjector;

using TMap.Domain.Abstractions.Services.Material;

namespace TMap.Configurations.DI.Extentions;

public static class RegisterModelsExtention
{
    public static Container RegisterModels(this Container container)
    {
        container.RegisterSingleton<MapSettingsModel>();
        container.RegisterSingleton<RoadSettingsModel>();
        container.RegisterSingleton(async () => await GetPipelineSettingsModel(container));
        container.RegisterSingleton<SettingsModel>();

        return container;
    }

    private static async Task<PipelineSettingsModel> GetPipelineSettingsModel(Container container)
    {
        var materialService = container.GetInstance<IMaterialService>();
        var mapper = container.GetInstance<IMapper>();

        var channelMaterial = await materialService.GetMaterialByNameAsync("Железобетон");

        return new PipelineSettingsModel(mapper.Map<MaterialModel>(channelMaterial));
    }
}
