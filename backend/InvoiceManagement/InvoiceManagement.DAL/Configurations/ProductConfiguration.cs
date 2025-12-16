using InvoiceManagement.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.DAL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
    
            builder.HasIndex(i => i.ProductName).IsUnique();
            builder.Property(i => i.ProductName)
                .IsRequired() 
                .HasMaxLength(100);

            builder.Property(i => i.Description)
                .HasMaxLength(500);
            
        }
    }
}