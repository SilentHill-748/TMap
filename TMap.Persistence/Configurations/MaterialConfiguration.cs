using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using TMap.Domain.Entities.Material;

namespace TMap.Persistence.Configurations;

internal class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable("materials");

        builder
            .Property(x => x.MaterialId)
            .HasColumnName("material_id");

        builder
            .Property(x => x.Name)
            .HasMaxLength(100)
            .HasColumnName("name");

        builder
            .Property(x => x.ThermalConductivity)
            .HasColumnName("thermal_conductivity");

        builder
            .Property(x => x.Humidity)
            .HasColumnName("humidity");

        builder
            .Property(x => x.Density)
            .HasColumnName("density");

        builder
            .Property(x => x.SpecificHeat)
            .HasColumnName("specific_heat");

        builder
            .Property(x => x.Type)
            .HasMaxLength(30)
            .HasColumnName("type")
            .HasConversion(new EnumToStringConverter<MaterialType>());

        builder
            .Property(x => x.ColorHexCode)
            .HasMaxLength(9)
            .HasColumnName("color");
    }
}
