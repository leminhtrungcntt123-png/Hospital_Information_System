using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.ToTable("invoices", t =>
        {
            t.HasCheckConstraint("ck_invoices_status", "status IN ('UNPAID','PAID')");
            t.HasCheckConstraint("ck_invoices_payment_method", "payment_method IS NULL OR payment_method IN ('CASH','BANK_TRANSFER')");
        });

        builder.HasKey(i => i.Id);
        builder.Property(i => i.TotalAmount).HasPrecision(12, 2).HasDefaultValue(0m);
        builder.Property(i => i.InsuranceCoveredAmount).HasPrecision(12, 2).HasDefaultValue(0m);
        builder.Property(i => i.PatientPaidAmount).HasPrecision(12, 2).HasDefaultValue(0m);
        builder.Property(i => i.Status).HasMaxLength(20).HasDefaultValue("UNPAID").IsRequired();
        builder.Property(i => i.PaymentMethod).HasMaxLength(20);
        builder.Property(i => i.CreatedAt).HasDefaultValueSql("now()");

        builder.HasOne(i => i.Appointment)
            .WithOne(a => a.Invoice)
            .HasForeignKey<Invoice>(i => i.AppointmentId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(i => i.AppointmentId).IsUnique();

        builder.HasOne(i => i.Patient)
            .WithMany(p => p.Invoices)
            .HasForeignKey(i => i.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.InsurancePlan)
            .WithMany(ip => ip.Invoices)
            .HasForeignKey(i => i.InsurancePlanId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(i => i.Cashier)
            .WithMany(a => a.CashierInvoices)
            .HasForeignKey(i => i.CashierId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}