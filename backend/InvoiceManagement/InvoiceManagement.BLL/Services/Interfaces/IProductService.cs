using InvoiceManagement.DL.Entities;

namespace InvoiceManagement.BLL.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> FindAll();
        Product FindById(Guid id);
        Product Save(Product product);
        void Update(Guid id, Product product);
        void Delete(Guid id);
    }
}