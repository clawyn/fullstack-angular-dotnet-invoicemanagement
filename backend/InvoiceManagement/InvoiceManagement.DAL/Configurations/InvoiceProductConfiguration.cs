using InvoiceManagement.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.DAL.Configurations
{
    public class InvoiceProductConfiguration : IEntityTypeConfiguration<InvoiceProduct>
    {
        public void Configure(EntityTypeBuilder<InvoiceProduct> builder)
        {
            builder.HasKey(ip => new { ip.InvoiceId, ip.ProductId });

            builder.Property(ip => ip.Quantity)
                .IsRequired();

            builder.Property(ip => ip.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.HasOne(ip => ip.Invoice)
                .WithMany(i => i.InvoiceProducts)
                .HasForeignKey(ip => ip.InvoiceId);

            builder.HasOne(ip => ip.Product)
                .WithMany(p => p.InvoiceProducts)
                .HasForeignKey(ip => ip.ProductId);

            builder.ToTable("InvoiceProducts");
        }
    }
}