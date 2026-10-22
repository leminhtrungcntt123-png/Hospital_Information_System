using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class DepartmentHeadConfiguration : IEntityTypeConfiguration<DepartmentHead>
{
    public void Configure(EntityTypeBuilder<DepartmentHead> builder)
    {
        builder.ToTable("department_heads");
        builder.HasKey(dh => dh.Id);
        builder.Property(dh => dh.FullName).HasMaxLength(100).IsRequired();

        builder.HasOne(dh => dh.Account)
            .WithOne(a => a.DepartmentHead)
            .HasForeignKey<DepartmentHead>(dh => dh.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(dh => dh.AccountId).IsUnique();

        builder.HasOne(dh => dh.Department)
            .WithOne(d => d.DepartmentHead)
            .HasForeignKey<DepartmentHead>(dh => dh.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(dh => dh.DepartmentId).IsUnique();
    }
}