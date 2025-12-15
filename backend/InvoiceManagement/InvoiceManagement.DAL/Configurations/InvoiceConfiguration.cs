using InvoiceManagement.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceManagement.DAL.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            
            builder.HasKey(c => c.Id);
            
            builder.HasIndex(i => i.Name).IsUnique(); 
            builder.Property(i => i.Name)
                .IsRequired() 
                .HasMaxLength(100); 
            

            builder.Property(i => i.Description)
                .HasMaxLength(500); 

           
            builder.HasOne(i => i.Customer) 
                .WithMany(c => c.Invoices) 
                .HasForeignKey(i => i.CustomerId); 
            

        }
    }
}

