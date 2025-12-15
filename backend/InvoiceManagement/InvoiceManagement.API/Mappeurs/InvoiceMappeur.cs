using InvoiceManagement.API.Dtos.Invoice;
using InvoiceManagement.API.Dtos.Product;
using InvoiceManagement.API.Dtos.Invoice_Product;
using InvoiceManagement.DL.Entities;

namespace InvoiceManagement.API.Mappeurs
{
    public static class InvoiceMappeur
    {
        public static InvoiceShortDto ToInvoiceShortDto(this Invoice i)
        {
            return new InvoiceShortDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                
            };
        }

        public static InvoiceDetailsDto ToInvoiceDetailsDto(this Invoice i)
        {
            return new InvoiceDetailsDto
            {
                Id = i.Id,
                Name = i.Name,
                Description = i.Description,
                WorkForce = i.WorkForce,
                
                InvoiceProducts = i.InvoiceProducts.Select(ip => new InvoiceProductDto
                {
                    ProductId = ip.ProductId,
                    Quantity = ip.Quantity,
                    Amount = ip.Amount,
                    
                    Product = new ProductDetailsDto
                    {
                        Id = ip.Product.Id,
                        ProductName = ip.Product.ProductName,
                        Category = ip.Product.Category,
                        Price = ip.Product.Price
                    }
                }).ToList()
            };
            
        }

        public static Invoice ToInvoice(this InvoiceFormDto i)
        {
            return new Invoice
            {
                Name = i.Name,
                Description = i.Description,
                WorkForce = i.WorkForce,
                CustomerId = i.CustomerId,
                
                // Mapper les produits
                InvoiceProducts = i.Products.Select(p => new InvoiceProduct
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                }).ToList()
            };
        }
    }
}