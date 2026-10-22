using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class DoctorScheduleConfiguration : IEntityTypeConfiguration<DoctorSchedule>
{
    public void Configure(EntityTypeBuilder<DoctorSchedule> builder)
    {
        builder.ToTable("doctor_schedules", t =>
        {
            t.HasCheckConstraint("ck_doctor_schedules_day_of_week", "day_of_week IS NULL OR (day_of_week BETWEEN 1 AND 7)");
            t.HasCheckConstraint("ck_doctor_schedules_shift", "shift IN ('MORNING','AFTERNOON')");
        });

        builder.HasKey(ds => ds.Id);
        builder.Property(ds => ds.Shift).HasMaxLength(10).IsRequired();
        builder.Property(ds => ds.SlotDurationMinutes).HasDefaultValue(15);
        builder.Property(ds => ds.IsActive).HasDefaultValue(true);

        builder.HasOne(ds => ds.Doctor)
            .WithMany(d => d.DoctorSchedules)
            .HasForeignKey(ds => ds.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(ds => ds.Room)
            .WithMany(r => r.DoctorSchedules)
            .HasForeignKey(ds => ds.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}