using InvoiceManagement.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceManagement.DAL.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(c => c.Name)
                .IsRequired()                
                .HasMaxLength(200);          
            
            builder.Property(c => c.Email)
                .IsRequired() 
                .HasMaxLength(150);           

            
            builder.Property(c => c.Phone)
                .HasMaxLength(15);          

            
            builder.HasMany(c => c.Invoices)  
                .WithOne(i => i.Customer)     
                .HasForeignKey(i => i.CustomerId); 
            
        }
    }
}