using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class LabOrderConfiguration : IEntityTypeConfiguration<LabOrder>
{
    public void Configure(EntityTypeBuilder<LabOrder> builder)
    {
        builder.ToTable("lab_orders", t =>
        {
            t.HasCheckConstraint("ck_lab_orders_status", "status IN ('PENDING','COMPLETED')");
        });

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Note).HasMaxLength(255);
        builder.Property(o => o.Status).HasMaxLength(20).HasDefaultValue("PENDING").IsRequired();
        builder.Property(o => o.CreatedAt).HasDefaultValueSql("now()");

        builder.HasOne(o => o.MedicalRecord)
            .WithMany(m => m.LabOrders)
            .HasForeignKey(o => o.MedicalRecordId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(o => o.Service)
            .WithMany(s => s.LabOrders)
            .HasForeignKey(o => o.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}