using InvoiceManagement.API.Dtos.Invoice;
using InvoiceManagement.API.Dtos.Product;

namespace InvoiceManagement.API.Dtos.Invoice_Product;

public class InvoiceProductDto
{
    public Guid InvoiceId { get; set; }
    public InvoiceDetailsDto Invoice { get; set; }

    public Guid ProductId { get; set; }
    public ProductDetailsDto Product { get; set; }

    public int Quantity { get; set; }
    public decimal Amount { get; set; }
}