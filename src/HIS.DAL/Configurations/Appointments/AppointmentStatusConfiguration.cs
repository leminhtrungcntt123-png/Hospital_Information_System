using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class AppointmentStatusConfiguration : IEntityTypeConfiguration<AppointmentStatus>
{
    public void Configure(EntityTypeBuilder<AppointmentStatus> builder)
    {
        builder.ToTable("appointment_statuses");
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();
        builder.Property(s => s.Code).HasMaxLength(20).IsRequired();
        builder.HasIndex(s => s.Code).IsUnique();
        builder.Property(s => s.Name).HasMaxLength(50).IsRequired();
        builder.Property(s => s.SortOrder).IsRequired();
        builder.Property(s => s.IsTerminal).HasDefaultValue(false);

        builder.HasData(
            new AppointmentStatus { Id = (int)AppointmentStatusId.Pending, Code = "PENDING", Name = "Chờ xác nhận", SortOrder = 1, IsTerminal = false },
            new AppointmentStatus { Id = (int)AppointmentStatusId.Confirmed, Code = "CONFIRMED", Name = "Đã xác nhận", SortOrder = 2, IsTerminal = false },
            new AppointmentStatus { Id = (int)AppointmentStatusId.CheckedIn, Code = "CHECKED_IN", Name = "Đã check-in", SortOrder = 3, IsTerminal = false },
            new AppointmentStatus { Id = (int)AppointmentStatusId.ReadyForDoctor, Code = "READY_FOR_DOCTOR", Name = "Chờ bác sĩ", SortOrder = 4, IsTerminal = false },
            new AppointmentStatus { Id = (int)AppointmentStatusId.InProgress, Code = "IN_PROGRESS", Name = "Đang khám", SortOrder = 5, IsTerminal = false },
            new AppointmentStatus { Id = (int)AppointmentStatusId.Completed, Code = "COMPLETED", Name = "Hoàn thành", SortOrder = 6, IsTerminal = true },
            new AppointmentStatus { Id = (int)AppointmentStatusId.Cancelled, Code = "CANCELLED", Name = "Đã hủy", SortOrder = 7, IsTerminal = true },
            new AppointmentStatus { Id = (int)AppointmentStatusId.NoShow, Code = "NO_SHOW", Name = "Không đến", SortOrder = 8, IsTerminal = true },
            new AppointmentStatus { Id = (int)AppointmentStatusId.Expired, Code = "EXPIRED", Name = "Hết hạn giữ chỗ", SortOrder = 9, IsTerminal = true }
        );
    }
}