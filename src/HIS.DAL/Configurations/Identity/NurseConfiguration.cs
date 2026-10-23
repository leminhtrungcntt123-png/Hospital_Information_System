using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class NurseConfiguration : IEntityTypeConfiguration<Nurse>
{
    public void Configure(EntityTypeBuilder<Nurse> builder)
    {
        builder.ToTable("nurses");
        builder.HasKey(n => n.Id);
        builder.Property(n => n.FullName).HasMaxLength(100).IsRequired();

        builder.HasOne(n => n.Account)
            .WithOne(a => a.Nurse)
            .HasForeignKey<Nurse>(n => n.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(n => n.AccountId).IsUnique();

        builder.HasOne(n => n.Department)
            .WithMany(d => d.Nurses)
            .HasForeignKey(n => n.DepartmentId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}