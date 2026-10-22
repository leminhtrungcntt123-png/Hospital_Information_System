using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class InsurancePlanConfiguration : IEntityTypeConfiguration<InsurancePlan>
{
    public void Configure(EntityTypeBuilder<InsurancePlan> builder)
    {
        builder.ToTable("insurance_plans", t =>
        {
            t.HasCheckConstraint("ck_insurance_plans_referral_type", "referral_type IS NULL OR referral_type IN ('IN_NETWORK','OUT_OF_NETWORK')");
        });

        builder.HasKey(ip => ip.Id);
        builder.Property(ip => ip.Name).HasMaxLength(100).IsRequired();
        builder.Property(ip => ip.CoveragePct).HasPrecision(5, 2).IsRequired();
        builder.Property(ip => ip.ReferralType).HasMaxLength(20);
    }
}