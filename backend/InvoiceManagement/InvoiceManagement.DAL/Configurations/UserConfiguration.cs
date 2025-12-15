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
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(u => u.Pseudo).IsUnique();
            builder.Property(u => u.Pseudo).IsRequired();
            builder.Property(u => u.Pseudo).HasMaxLength(50);

            builder.HasIndex(u => u.Email).IsUnique();
            builder.Property(u => u.Email).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(150);

            builder.Property(u => u.Password).IsRequired();
        }
    }
}