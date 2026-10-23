using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.ToTable("doctors");
        builder.HasKey(d => d.Id);
        builder.Property(d => d.FullName).HasMaxLength(100).IsRequired();
        builder.Property(d => d.Specialty).HasMaxLength(100);
        builder.Property(d => d.LicenseNumber).HasMaxLength(30);
        builder.Property(d => d.AvgConsultationMinutes).HasDefaultValue(15);

        builder.HasOne(d => d.Account)
            .WithOne(a => a.Doctor)
            .HasForeignKey<Doctor>(d => d.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(d => d.AccountId).IsUnique();

        builder.HasOne(d => d.Department)
            .WithMany(dep => dep.Doctors)
            .HasForeignKey(d => d.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}