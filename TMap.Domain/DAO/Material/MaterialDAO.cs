﻿using Newtonsoft.Json;

using TMap.Domain.Entities.Material;

namespace TMap.Domain.DAO.Material;

public class MaterialDAO
{
    [JsonProperty("Name")]
    public string? Name { get; set; }

    [JsonProperty("ThermalConductivity")]
    public double ThermalConductivity { get; set; }
    [JsonProperty("SpecificHeat")]
    public double SpecificHeat { get; set; }

    [JsonProperty("Density")]
    public double Density { get; set; }

    [JsonProperty("Humidity")]
    public double Humidity { get; set; }

    [JsonProperty("Type")]
    public MaterialType Type { get; set; }

    [JsonProperty("Color")]
    public string? ColorHexCode { get; set; }
}
