using InvoiceManagement.API.Dtos.Invoice_Product;

namespace InvoiceManagement.API.Dtos.Invoice
{
    public class InvoiceDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = null!;
        public decimal WorkForce { get; set; }
        
        public Guid CustomerId { get; set; }
        
        public ICollection<InvoiceProductDto> InvoiceProducts { get; set; } = new List<InvoiceProductDto>();
    }
}

