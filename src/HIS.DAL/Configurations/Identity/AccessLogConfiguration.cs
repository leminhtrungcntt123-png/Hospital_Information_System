using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class AccessLogConfiguration : IEntityTypeConfiguration<AccessLog>
{
    public void Configure(EntityTypeBuilder<AccessLog> builder)
    {
        builder.ToTable("access_logs");
        builder.HasKey(al => al.Id);
        builder.Property(al => al.Action).HasMaxLength(50).IsRequired();
        builder.Property(al => al.OccurredAt).HasDefaultValueSql("now()");
        builder.Property(al => al.IpAddress).HasMaxLength(45);

        builder.HasOne(al => al.Account)
            .WithMany(a => a.AccessLogs)
            .HasForeignKey(al => al.AccountId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(al => al.MedicalRecord)
            .WithMany(m => m.AccessLogs)
            .HasForeignKey(al => al.MedicalRecordId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(al => al.AccountId).HasDatabaseName("idx_access_logs_account");
    }
}