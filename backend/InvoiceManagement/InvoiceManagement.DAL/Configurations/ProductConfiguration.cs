using InvoiceManagement.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.DAL.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Configuration des propriétés de base
            builder.HasIndex(i => i.ProductName).IsUnique(); // Index unique sur Name
            builder.Property(i => i.ProductName)
                .IsRequired() // Obligatoire
                .HasMaxLength(100); // Longueur maximale

            builder.Property(i => i.Description)
                .HasMaxLength(500); // Longueur maximale de Description
            
        }
    }
}