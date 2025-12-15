using InvoiceManagement.DL.Entities;
using InvoiceManagement.ToolBox.Repositories;

namespace InvoiceManagement.DAL.Repositories.Interfaces
{
    public interface IInvoiceManagementRepository : ICrudRepository<Invoice>
    {
        Invoice? FindOneWithProducts(Guid id);
    }
}

