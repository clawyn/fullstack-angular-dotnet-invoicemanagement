using InvoiceManagement.DL.Entities;

namespace InvoiceManagement.BLL.Services.Interfaces
{
    public interface IInvoiceService
    {
        IEnumerable<Invoice> FindAll();
        IEnumerable<Invoice> GetInvoicesByCustomerId(Guid customerId);
        Invoice FindById(Guid id);
        Invoice Save(Invoice invoice);
        void Update(Guid id, Invoice invoice);
        void Delete(Guid id);

        Invoice SaveWithProducts(Invoice invoice);
    } 
}

