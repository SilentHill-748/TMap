﻿using AutoMapper;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

using TMap.Domain.DAO.Material;
using TMap.Domain.Entities.Material;
using TMap.Persistence.Exceptions;

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

    public async Task SeedAsync(string rootPath)
    {
        var databaseExists = _dbContext.Database.EnsureCreated();

        if (!databaseExists) return;

        try
        {
            var jsonPath = Path.Combine(rootPath, "Materials.json");
            var json = File.ReadAllText(jsonPath);
            var stringEnumConverter = new StringEnumConverter(new CamelCaseNamingStrategy());

            var materialDAOs = JsonConvert.DeserializeObject<List<MaterialDAO>>(json, stringEnumConverter) ??
                throw new MaterialsNotLoadedException(jsonPath, true);

            var materials = materialDAOs.Select(_mapper.Map<MaterialDAO, Material>);

            await _dbContext.Materials.AddRangeAsync(materials);

            _ = await _dbContext.SaveChangesAsync();
        }
        catch
        {
            await _dbContext.Database.EnsureDeletedAsync();
            throw;
        }
    }
}