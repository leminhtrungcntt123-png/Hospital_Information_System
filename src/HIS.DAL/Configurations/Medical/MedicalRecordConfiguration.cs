using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class MedicalRecordConfiguration : IEntityTypeConfiguration<MedicalRecord>
{
    public void Configure(EntityTypeBuilder<MedicalRecord> builder)
    {
        builder.ToTable("medical_records");
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Symptoms).HasColumnType("text");
        builder.Property(m => m.ClinicalExamination).HasColumnType("text");
        builder.Property(m => m.PrimaryDiagnosisCode).HasMaxLength(20);
        builder.Property(m => m.PrimaryDiagnosisDesc).HasMaxLength(255);
        builder.Property(m => m.SecondaryDiagnosis).HasColumnType("text");
        builder.Property(m => m.Conclusion).HasColumnType("text");
        builder.Property(m => m.CreatedAt).HasDefaultValueSql("now()");
        builder.Property(m => m.UpdatedAt).HasDefaultValueSql("now()");

        builder.HasOne(m => m.Appointment)
            .WithOne(a => a.MedicalRecord)
            .HasForeignKey<MedicalRecord>(m => m.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(m => m.AppointmentId).IsUnique().HasDatabaseName("idx_records_appointment");

        builder.HasOne(m => m.Doctor)
            .WithMany(d => d.MedicalRecords)
            .HasForeignKey(m => m.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}