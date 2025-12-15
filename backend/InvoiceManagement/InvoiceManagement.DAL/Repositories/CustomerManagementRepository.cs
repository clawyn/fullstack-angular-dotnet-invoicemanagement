using InvoiceManagement.DL.Entities;
using InvoiceManagement.DAL.Repositories.Interfaces;
using InvoiceManagement.ToolBox.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.DAL.Repositories
{
    public class CustomerManagementRepository : CrudRepository<Customer>, ICustomerManagementRepository
    {
        public CustomerManagementRepository(DbContext dbContext) : base(dbContext){ }
    } 
}

