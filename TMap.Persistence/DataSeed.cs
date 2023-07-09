﻿using AutoMapper;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using TMap.Domain.DAO.Material;
using TMap.Domain.Entities.Material;

namespace TMap.Persistence;

public class DataSeed
{
    private readonly TMapDbContext _dbContext;
    private readonly IMapper _mapper;

    public DataSeed(TMapDbContext dbContext, IMapper mapper)
    {
        ArgumentNullException.ThrowIfNull(dbContext, nameof(dbContext));
        ArgumentNullException.ThrowIfNull(mapper, nameof(mapper));

        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task SeedAsync()
    {
        var jsonPath = Path.Combine(Directory.GetCurrentDirectory(), "TMap.Persistence\\Seed\\Materials.json");
        var json = File.ReadAllText(jsonPath);
        var stringEnumConverter = new StringEnumConverter(new CamelCaseNamingStrategy());

        var materialDAOs = JsonConvert.DeserializeObject<List<MaterialDAO>>(json, stringEnumConverter) ?? 
            throw new Exception("Не удалось загрузить данные по базовым материалам!");

        var materials = materialDAOs.Select(_mapper.Map<MaterialDAO, Material>);

        await _dbContext.Materials.AddRangeAsync(materials);
        
        _ = await _dbContext.SaveChangesAsync();
    }
}
