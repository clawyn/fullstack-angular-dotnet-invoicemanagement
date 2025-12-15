using InvoiceManagement.DAL.Configurations;
using InvoiceManagement.DL.Entities;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace InvoiceManagement.DAL.Context
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<Favorite> Favorites => Set<Favorite>();
        
        public DbSet<Customer> Customers => Set<Customer>();
        
        public DbSet<Product> Products => Set<Product>();
        
        public DbSet<InvoiceProduct> InvoiceProducts => Set<InvoiceProduct>();        
        public LibraryDbContext() { }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=InvoiceDb;Trusted_Connection=True;TrustServerCertificate=True;");
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=invoiceDb;Username=postgres;Password=postgres");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new InvoiceConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new FavoriteConfiguration());
            builder.ApplyConfiguration(new CustomerConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new InvoiceProductConfiguration());
            
            builder.Entity<Invoice>()
                .HasOne(i => i.Customer) 
                .WithMany(c => c.Invoices)
                .HasForeignKey(i => i.CustomerId);
            
            builder.Entity<InvoiceProduct>()
                .HasOne(ip => ip.Product) 
                .WithMany(p => p.InvoiceProducts)
                .HasForeignKey(ip => ip.ProductId);
            
            builder.Entity<InvoiceProduct>()
                .HasOne(ip => ip.Invoice) 
                .WithMany(i => i.InvoiceProducts)
                .HasForeignKey(ip => ip.InvoiceId);
            
            builder.Entity<InvoiceProduct>()
                .HasKey(ip => new { ip.InvoiceId, ip.ProductId });
        }
    }
}
