using InvoiceManagement.DL.Entities;
using InvoiceManagement.ToolBox.Repositories;

namespace InvoiceManagement.DAL.Repositories.Interfaces
{

    public interface ICustomerManagementRepository : ICrudRepository<Customer>
    {
    }
}