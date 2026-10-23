using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
{
    public void Configure(EntityTypeBuilder<Appointment> builder)
    {
        builder.ToTable("appointments", t =>
        {
            t.HasCheckConstraint("ck_appointments_booking_channel", "booking_channel IN ('WEB','ON_SITE')");
            t.HasCheckConstraint("ck_appointments_visit_type", "visit_type IN ('SCHEDULED','WALK_IN')");
            t.HasCheckConstraint("ck_appointments_priority_level", "priority_level BETWEEN 0 AND 3");
        });

        builder.HasKey(a => a.Id);
        builder.Property(a => a.AppointmentCode).HasMaxLength(30).IsRequired();
        builder.HasIndex(a => a.AppointmentCode).IsUnique();

        builder.Property(a => a.StatusId).IsRequired();
        builder.Property(a => a.BookingChannel).HasMaxLength(15).HasDefaultValue("WEB").IsRequired();
        builder.Property(a => a.VisitType).HasMaxLength(15).HasDefaultValue("SCHEDULED").IsRequired();
        builder.Property(a => a.PriorityLevel).HasDefaultValue(0);
        builder.Property(a => a.QueueNumber).HasMaxLength(10);
        builder.Property(a => a.Reason).HasMaxLength(255);
        builder.Property(a => a.CreatedAt).HasDefaultValueSql("now()");
        builder.Property(a => a.UpdatedAt).HasDefaultValueSql("now()");

        builder.HasOne(a => a.Status)
            .WithMany(s => s.Appointments)
            .HasForeignKey(a => a.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Slot)
            .WithMany(s => s.Appointments)
            .HasForeignKey(a => a.SlotId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.Room)
            .WithMany(r => r.Appointments)
            .HasForeignKey(a => a.RoomId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(a => a.ParentAppointment)
            .WithMany(pa => pa.ChildAppointments)
            .HasForeignKey(a => a.ParentAppointmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(a => a.PatientId).HasDatabaseName("idx_appointments_patient");
        builder.HasIndex(a => a.StatusId).HasDatabaseName("idx_appointments_status_id");
    }
}