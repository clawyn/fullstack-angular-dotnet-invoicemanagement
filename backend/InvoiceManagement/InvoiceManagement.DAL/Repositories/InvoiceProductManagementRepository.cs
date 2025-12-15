using InvoiceManagement.DL.Entities;
using InvoiceManagement.DAL.Repositories.Interfaces;
using InvoiceManagement.ToolBox.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.DAL.Repositories
{
    public class InvoiceProductManagementRepository : CrudRepository<InvoiceProduct>, IInvoiceProductManagementRepository
    {
        public InvoiceProductManagementRepository(DbContext dbContext) : base(dbContext){ }
    }
}

