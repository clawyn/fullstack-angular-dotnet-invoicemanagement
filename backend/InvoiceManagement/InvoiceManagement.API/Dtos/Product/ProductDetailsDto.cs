using InvoiceManagement.API.Dtos.Invoice_Product;

namespace InvoiceManagement.API.Dtos.Product
{

    public class ProductDetailsDto
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public String Description { get; set; }
        public string Category { get; set; }
        public Decimal Price { get; set; }
        public string Specification { get; set; }
        
        public ICollection<InvoiceProductDto> InvoiceProducts { get; set; } = new List<InvoiceProductDto>();
    }
}