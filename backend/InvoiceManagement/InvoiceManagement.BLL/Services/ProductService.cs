using InvoiceManagement.BLL.Services.Interfaces;
using InvoiceManagement.DAL.Repositories.Interfaces;
using InvoiceManagement.DL.Entities;

namespace InvoiceManagement.BLL.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductManagementRepository _productManagementRepository;

        public ProductService(IProductManagementRepository productManagementRepository)
        {
            _productManagementRepository = productManagementRepository;
        }

        public void Delete(Guid id)
        {
            Product? existingProduct = _productManagementRepository.FindOne(id);
            if(existingProduct == null)
            {
                throw new Exception($"Product with id {id} doesn't exist");
            }
            _productManagementRepository.Delete(existingProduct);
        }

        public IEnumerable<Product> FindAll()
        {
            return _productManagementRepository.FindMany();
        }

        public Product FindById(Guid id)
        {
            Product? product = _productManagementRepository.FindOne(id);
            if(product == null)
            {
                throw new Exception($"Invoice with id {id} doesn't exist");
            }
            return product;
        }

        public Product Save(Product product)
        {
            if(_productManagementRepository.Any(i => i.ProductName == product.ProductName))
            {
                throw new Exception($"Product with id {product.ProductName} already exist");
            }
            return _productManagementRepository.Save(product);
        }

        public void Update(Guid id, Product product)
        {
            Product? existingProduct = _productManagementRepository.FindOne(id);
            if(existingProduct == null)
            {
                throw new Exception($"Invoice with id {id} doesn't exist");
            }
            existingProduct.ProductName = product.ProductName;
            existingProduct.Description = product.Description;
            existingProduct.Category = product.Category;
            existingProduct.Price = product.Price;
            existingProduct.Specification = product.Specification;

            _productManagementRepository.Update(existingProduct);
        }
    }
}
