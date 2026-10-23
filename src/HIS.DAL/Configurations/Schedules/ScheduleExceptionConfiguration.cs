using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class ScheduleExceptionConfiguration : IEntityTypeConfiguration<ScheduleException>
{
    public void Configure(EntityTypeBuilder<ScheduleException> builder)
    {
        builder.ToTable("schedule_exceptions", t =>
        {
            t.HasCheckConstraint("ck_schedule_exceptions_shift", "shift IS NULL OR shift IN ('MORNING','AFTERNOON')");
            t.HasCheckConstraint("ck_schedule_exceptions_type", "type IN ('LEAVE','TRAINING','SURGERY','SHIFT_SWAP')");
        });

        builder.HasKey(se => se.Id);
        builder.Property(se => se.Shift).HasMaxLength(10);
        builder.Property(se => se.Type).HasMaxLength(20).IsRequired();
        builder.Property(se => se.Reason).HasMaxLength(255);
        builder.Property(se => se.CreatedAt).HasDefaultValueSql("now()");

        builder.HasOne(se => se.Doctor)
            .WithMany(d => d.ScheduleExceptions)
            .HasForeignKey(se => se.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(se => se.ReplacementDoctor)
            .WithMany(d => d.ReplacementScheduleExceptions)
            .HasForeignKey(se => se.ReplacementDoctorId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(se => se.Approver)
            .WithMany(a => a.ApprovedScheduleExceptions)
            .HasForeignKey(se => se.ApprovedBy)
            .OnDelete(DeleteBehavior.SetNull);
    }
}