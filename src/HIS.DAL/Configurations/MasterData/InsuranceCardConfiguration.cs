using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class InsuranceCardConfiguration : IEntityTypeConfiguration<InsuranceCard>
{
    public void Configure(EntityTypeBuilder<InsuranceCard> builder)
    {
        builder.ToTable("insurance_cards", t =>
        {
            t.HasCheckConstraint("ck_insurance_dates", "valid_to >= valid_from");
        });

        builder.HasKey(c => c.Id);
        builder.Property(c => c.CardNumber).HasMaxLength(20).IsRequired();
        builder.HasIndex(c => c.CardNumber).IsUnique();
        builder.Property(c => c.RegisteredFacility).HasMaxLength(150);
        builder.Property(c => c.IsRegisteredHere).HasDefaultValue(false);
        builder.Property(c => c.ValidFrom).IsRequired();
        builder.Property(c => c.ValidTo).IsRequired();
        builder.Property(c => c.IsActive).HasDefaultValue(true);
        builder.Property(c => c.CreatedAt).HasDefaultValueSql("now()");

        builder.HasOne(c => c.Patient)
            .WithMany(p => p.InsuranceCards)
            .HasForeignKey(c => c.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.InsurancePlan)
            .WithMany(ip => ip.InsuranceCards)
            .HasForeignKey(c => c.InsurancePlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => new { c.PatientId, c.ValidTo })
            .HasDatabaseName("idx_insurance_cards_patient");
    }
}
