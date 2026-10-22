using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class PrescriptionItemConfiguration : IEntityTypeConfiguration<PrescriptionItem>
{
    public void Configure(EntityTypeBuilder<PrescriptionItem> builder)
    {
        builder.ToTable("prescription_items");
        builder.HasKey(pi => pi.Id);
        builder.Property(pi => pi.Dosage).HasMaxLength(100);
        builder.Property(pi => pi.UsageInstructions).HasMaxLength(255);

        builder.HasOne(pi => pi.Prescription)
            .WithMany(p => p.PrescriptionItems)
            .HasForeignKey(pi => pi.PrescriptionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pi => pi.Medication)
            .WithMany(m => m.PrescriptionItems)
            .HasForeignKey(pi => pi.MedicationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}