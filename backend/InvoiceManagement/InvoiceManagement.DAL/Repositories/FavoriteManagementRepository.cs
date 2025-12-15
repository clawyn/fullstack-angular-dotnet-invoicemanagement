using InvoiceManagement.DAL.Repositories.Interfaces;
using InvoiceManagement.DL.Entities;
using InvoiceManagement.ToolBox.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InvoiceManagement.DAL.Repositories
{
    public class FavoriteManagementRepository : CrudRepository<Favorite>, IFavoriteManagementRepository
    {
        public FavoriteManagementRepository(DbContext dbContext) : base(dbContext) { }
    }
}