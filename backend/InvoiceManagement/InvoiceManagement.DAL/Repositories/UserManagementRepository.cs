using InvoiceManagement.DAL.Repositories.Interfaces;
using InvoiceManagement.DL.Entities;
using InvoiceManagement.ToolBox.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.DAL.Repositories
{
    public class UserManagementRepository : CrudRepository<User>, IUserManagementRepository
    {
        public UserManagementRepository(DbContext dbContext) : base(dbContext) { }
    }
}