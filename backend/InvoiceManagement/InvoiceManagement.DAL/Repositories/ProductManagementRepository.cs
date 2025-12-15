using InvoiceManagement.DL.Entities;
using InvoiceManagement.DAL.Repositories.Interfaces;
using InvoiceManagement.ToolBox.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.DAL.Repositories
{
    public class ProductManagementRepository : CrudRepository<Product>, IProductManagementRepository
    {
        public ProductManagementRepository(DbContext dbContext) : base(dbContext){ }
    }
}