using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.ToTable("services");

        builder.HasKey(s => s.Id);
        builder.Property(s => s.Name).HasMaxLength(150).IsRequired();
        builder.Property(s => s.Type).HasMaxLength(20).IsRequired();
        builder.Property(s => s.Price).HasPrecision(12, 2).HasDefaultValue(0m);
        builder.Property(s => s.IsActive).HasDefaultValue(true);
    }
}