using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class DutyShiftConfiguration : IEntityTypeConfiguration<DutyShift>
{
    public void Configure(EntityTypeBuilder<DutyShift> builder)
    {
        builder.ToTable("duty_shifts", t =>
        {
            t.HasCheckConstraint("ck_duty_time", "end_time > start_time");
        });

        builder.HasKey(d => d.Id);
        builder.Property(d => d.StartTime).IsRequired();
        builder.Property(d => d.EndTime).IsRequired();
        builder.Property(d => d.Note).HasMaxLength(255);
        builder.Property(d => d.CreatedAt).HasDefaultValueSql("now()");

        builder.HasOne(d => d.Account)
            .WithMany(a => a.DutyShifts)
            .HasForeignKey(d => d.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.Department)
            .WithMany(dep => dep.DutyShifts)
            .HasForeignKey(d => d.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(d => d.AssignedByAccount)
            .WithMany(a => a.AssignedDutyShifts)
            .HasForeignKey(d => d.AssignedBy)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(d => new { d.AccountId, d.StartTime })
            .HasDatabaseName("idx_duty_shifts_account");
    }
}
