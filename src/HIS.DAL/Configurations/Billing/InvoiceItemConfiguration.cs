using HIS.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HIS.DAL.Configurations;

public class InvoiceItemConfiguration : IEntityTypeConfiguration<InvoiceItem>
{
    public void Configure(EntityTypeBuilder<InvoiceItem> builder)
    {
        builder.ToTable("invoice_items", t =>
        {
            t.HasCheckConstraint("ck_invoice_items_category", "category IN ('EXAMINATION','MEDICATION','SERVICE')");
        });

        builder.HasKey(ii => ii.Id);
        builder.Property(ii => ii.Category).HasMaxLength(20).IsRequired();
        builder.Property(ii => ii.Description).HasMaxLength(255).IsRequired();
        builder.Property(ii => ii.Amount).HasPrecision(12, 2).IsRequired();

        builder.HasOne(ii => ii.Invoice)
            .WithMany(i => i.InvoiceItems)
            .HasForeignKey(ii => ii.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}