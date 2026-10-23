using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class ClinicRoomConfiguration : IEntityTypeConfiguration<ClinicRoom>
{
    public void Configure(EntityTypeBuilder<ClinicRoom> builder)
    {
        builder.ToTable("clinic_rooms");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.RoomName).HasMaxLength(50).IsRequired();
        builder.Property(r => r.RoomNumber).HasMaxLength(20).IsRequired();

        builder.HasOne(r => r.Department)
            .WithMany(d => d.ClinicRooms)
            .HasForeignKey(r => r.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}