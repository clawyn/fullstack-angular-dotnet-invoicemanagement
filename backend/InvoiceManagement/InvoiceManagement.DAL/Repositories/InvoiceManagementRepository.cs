using InvoiceManagement.DL.Entities;
using InvoiceManagement.DAL.Repositories.Interfaces;
using InvoiceManagement.ToolBox.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.DAL.Repositories
{
    public class InvoiceManagementRepository : CrudRepository<Invoice>, IInvoiceManagementRepository
    {
        public InvoiceManagementRepository(DbContext dbContext) : base(dbContext){ }
        public Invoice? FindOneWithProducts(Guid id)
        {
            return _entities
                .Include(i => i.InvoiceProducts)
                .ThenInclude(ip => ip.Product)
                .FirstOrDefault(i => i.Id == id);
        }
    }
}

