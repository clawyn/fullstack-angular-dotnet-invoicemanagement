using InvoiceManagement.DL.Entities;

namespace InvoiceManagement.BLL.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> FindAll();
        Customer FindById(Guid id);
        Customer Save(Customer customer);
        void Update(Guid id, Customer customer);
        void Delete(Guid id);
    } 
}

