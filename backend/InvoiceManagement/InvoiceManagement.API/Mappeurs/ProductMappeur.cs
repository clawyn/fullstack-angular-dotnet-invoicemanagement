using InvoiceManagement.DL.Entities;
using InvoiceManagement.API.Dtos.Product;

namespace InvoiceManagement.API.Mappeurs
{
    public static class ProductMappeur
    {
        public static ProductShortDto ToProductShortDto(this Product p)
        {
            return new ProductShortDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Category = p.Category,
                Price = p.Price,
            };
        }

        public static ProductDetailsDto ToProductDetailsDto(this Product p)
        {
            return new ProductDetailsDto
            {
                Id = p.Id,
                ProductName = p.ProductName,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                Specification = p.Specification,
                
            };
        }

        public static Product ToProduct(this ProductFormDto p)
        {
            return new Product
            {
                ProductName = p.ProductName,
                Description = p.Description,
                Category = p.Category,
                Price = p.Price,
                Specification = p.Specification,
            };
        }
    } 
}

