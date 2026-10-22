using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class LabResultConfiguration : IEntityTypeConfiguration<LabResult>
{
    public void Configure(EntityTypeBuilder<LabResult> builder)
    {
        builder.ToTable("lab_results");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Result).HasColumnType("text");
        builder.Property(r => r.RecordedAt).HasDefaultValueSql("now()");

        builder.HasOne(r => r.Order)
            .WithOne(o => o.Result)
            .HasForeignKey<LabResult>(r => r.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(r => r.OrderId).IsUnique();

        builder.HasOne(r => r.Performer)
            .WithMany(a => a.PerformedLabResults)
            .HasForeignKey(r => r.PerformedBy)
            .OnDelete(DeleteBehavior.SetNull);
    }
}