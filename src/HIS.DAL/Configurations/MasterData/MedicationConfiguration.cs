using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class MedicationConfiguration : IEntityTypeConfiguration<Medication>
{
    public void Configure(EntityTypeBuilder<Medication> builder)
    {
        builder.ToTable("medications");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Name).HasMaxLength(150).IsRequired();
        builder.Property(m => m.ActiveIngredient).HasMaxLength(150);
        builder.Property(m => m.Strength).HasMaxLength(50);
        builder.Property(m => m.Unit).HasMaxLength(20).IsRequired();
        builder.Property(m => m.Price).HasPrecision(12, 2).HasDefaultValue(0m);
        builder.Property(m => m.IsActive).HasDefaultValue(true);
    }
}