using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("payments", t =>
        {
            t.HasCheckConstraint("ck_payments_amount", "amount > 0");
            t.HasCheckConstraint("ck_payments_payment_type", "payment_type IN ('DEPOSIT','PAYMENT','REFUND')");
            t.HasCheckConstraint("ck_payments_payment_method", "payment_method IN ('CASH','BANK_TRANSFER')");
        });

        builder.HasKey(p => p.Id);
        builder.Property(p => p.Amount).HasPrecision(12, 2).IsRequired();
        builder.Property(p => p.PaymentType).HasMaxLength(15).HasDefaultValue("PAYMENT").IsRequired();
        builder.Property(p => p.PaymentMethod).HasMaxLength(20).HasDefaultValue("CASH").IsRequired();
        builder.Property(p => p.Note).HasMaxLength(255);
        builder.Property(p => p.PaidAt).HasDefaultValueSql("now()");

        builder.HasOne(p => p.Invoice)
            .WithMany(i => i.Payments)
            .HasForeignKey(p => p.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(p => p.Cashier)
            .WithMany(a => a.Payments)
            .HasForeignKey(p => p.CashierId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasIndex(p => p.InvoiceId)
            .HasDatabaseName("idx_payments_invoice");
    }
}
