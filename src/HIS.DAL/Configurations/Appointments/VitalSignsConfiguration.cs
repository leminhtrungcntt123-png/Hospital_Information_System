using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class VitalSignsConfiguration : IEntityTypeConfiguration<VitalSigns>
{
    public void Configure(EntityTypeBuilder<VitalSigns> builder)
    {
        builder.ToTable("vital_signs");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Temperature).HasPrecision(4, 1);
        builder.Property(v => v.Weight).HasPrecision(5, 2);
        builder.Property(v => v.Height).HasPrecision(5, 2);
        builder.Property(v => v.RecordedAt).HasDefaultValueSql("now()");

        builder.HasOne(v => v.Appointment)
            .WithOne(a => a.VitalSigns)
            .HasForeignKey<VitalSigns>(v => v.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(v => v.AppointmentId).IsUnique();

        builder.HasOne(v => v.Recorder)
            .WithMany(n => n.RecordedVitalSigns)
            .HasForeignKey(v => v.RecordedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}